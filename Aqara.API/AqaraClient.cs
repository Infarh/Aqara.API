using System.ComponentModel;
using System.Globalization;
using System.Net.Http.Json;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Text.Json.Serialization;

using Aqara.API.DTO;
using Aqara.API.Exceptions;
using Aqara.API.Infrastructure;
using Aqara.API.Models;

using Microsoft.Extensions.Options;

using static Aqara.API.Addresses;

namespace Aqara.API;

public class AqaraClient
{
    private static readonly JsonSerializerOptions __SerializerOptions = new()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    private readonly HttpClient _Client;
    private readonly AqaraClientConfig _Configuration;
    private readonly IAccessTokenSource _AccessTokenSource;

    private HttpClient GetClient(string? AccessToken = null) => _Client.AddHeaders(_Configuration, AccessToken);

    public AqaraClient(HttpClient Client, IAccessTokenSource AccessTokenSource, AqaraClientConfig Configuration)
    {
        _Client = Client;
        _Configuration = Configuration;
        _AccessTokenSource = AccessTokenSource;
    }

    private async Task<HttpClient> GetClientWithAccessToken(CancellationToken Cancel)
    {
        if(await _AccessTokenSource.GetAccessToken(Cancel).ConfigureAwait(false) is not { } token) 
            throw new InvalidOperationException("Отсутствует токен доступа");

        if (!token.IsExpire) 
            return GetClient(token.AccessToken).AddAccessToken(token);

        token = await RefreshAccessToken(Cancel).ConfigureAwait(false);
        if(token is null || token.IsExpire)
            throw new InvalidOperationException("Не удалось обновить токен доступа");

        await _AccessTokenSource.SetAccessToken(token, Cancel);

        return GetClient(token.AccessToken).AddAccessToken(token);
    }

    public async Task<string?> RequestAuthorizationKey(
        string Account,
        string AccessTokenValidity = "1h",
        AccountType AccountType = AccountType.Aqara,
        CancellationToken Cancel = default)
    {
        var data = new AuthorizationCodeRequest(Account, AccessTokenValidity, AccountType);

        var result = await GetClient()
           .PostAsJsonAsync("", data, Cancel)
           .GetJsonResult<AuthorizationCodeResponse>(Cancel)
           .ConfigureAwait(false);

        if (result is null)
            throw new RequestAuthorizationCodeException("Не удалось получить ответ от сервера")
            {
                RequestData = data,
            };

        if (result.ErrorCode != ErrorCode.Success)
            throw new RequestAuthorizationCodeException($"Ошибка запроса кода авторизации {result.Message} {result.MessageDetails}")
            {
                RequestData = data,
                ResponseData = result,
            };

        return result.Result!.AuthorizationCode;
    }

    public async Task<AccessTokenInfo> ObtainAccessToken(
        string VerificationCode,
        string? Account = null,
        AccountType AccountType = AccountType.Aqara,
        CancellationToken Cancel = default)
    {
        var data = new AccessTokenRequest(VerificationCode, Account, AccountType);

        var response = await GetClient()
           .PostAsJsonAsync("", data, __SerializerOptions, Cancel)
           .ConfigureAwait(false);

        var result = await response
           .EnsureSuccessStatusCode()
           .Content
           .ReadFromJsonAsync<AccessTokenResponse>(cancellationToken: Cancel);

        if (result is null)
            throw new RequestAccessTokenException("Не удалось получить ответ от сервера")
            {
                RequestData = data
            };

        if (result.ErrorCode != ErrorCode.Success)
            throw new RequestAccessTokenException($"Ошибка получения токена авторизации {result.Message} {result.MessageDetails}")
            {
                RequestData = data,
                ResponseData = result,
            };

        var access_token = result.Result!.AccessToken;
        var refresh_token = result.Result.RefreshToken;
        var expire = int.Parse(result.Result.ExpiresIn);
        var open_id = result.Result.OpenId;

        var token = new AccessTokenInfo(access_token, refresh_token, expire, open_id, DateTime.Now);

        await _AccessTokenSource.SetAccessToken(token, Cancel).ConfigureAwait(false);

        return token;
    }

    public async Task<AccessTokenInfo> RefreshAccessToken(CancellationToken Cancel = default)
    {
        if (await _AccessTokenSource.GetAccessToken(Cancel).ConfigureAwait(false) is not { } token)
            throw new InvalidOperationException("Невозможно обновить токен авторизации потом, что отсутствует информация о старом токене авторизации");

        var data = new RefreshAccessTokenRequest(token.RefreshToken);

        var response = await GetClient()
           .PostAsJsonAsync("", data, __SerializerOptions, Cancel)
           .ConfigureAwait(false);

        var result = await response
           .EnsureSuccessStatusCode()
           .Content
           .ReadFromJsonAsync<RefreshAccessTokenResponse>(cancellationToken: Cancel);

        if (result is null)
            throw new RefreshAccessTokenException("Не удалось получить ответ от сервера")
            {
                RequestData = data
            };

        if (result.ErrorCode != ErrorCode.Success)
            throw new RefreshAccessTokenException($"Ошибка получения токена авторизации {result.Message} {result.MessageDetails}")
            {
                RequestData = data,
                ResponseData = result,
            };

        var access_token = result.Result!.AccessToken;
        var refresh_token = result.Result.RefreshToken;
        var expire = int.Parse(result.Result.ExpiresIn);
        var open_id = result.Result.OpenId;

        var result_token = new AccessTokenInfo(access_token, refresh_token, expire, open_id, DateTime.Now);

        return await _AccessTokenSource.SetAccessToken(result_token, Cancel);
    }

    public async Task<PositionInfo[]> GetPositions(string? ParentPositionId = null, int? Page = 1, int? PageSize = 30, CancellationToken Cancel = default)
    {
        var data = new GetPositionsRequest(ParentPositionId, Page, PageSize);

        var client = await GetClientWithAccessToken(Cancel);

        var response = await client
           .PostAsJsonAsync("", data, __SerializerOptions, Cancel)
           .ConfigureAwait(false);

        var result = await response
           .EnsureSuccessStatusCode()
           .Content
           .ReadFromJsonAsync<GetPositionsResponse>(cancellationToken: Cancel);

        if (result is null)
            throw new GetPositionsException("Не удалось получить ответ от сервера")
            {
                RequestData = data
            };

        if (result.ErrorCode != ErrorCode.Success)
            throw new GetPositionsException($"Ошибка получения токена авторизации {result.Message} {result.MessageDetails}")
            {
                RequestData = data,
                ResponseData = result,
            };

        return result.Result.Data
           .Select(position => new PositionInfo
           {
               PositionId = position.PositionId,
               ParentPositionId = position.ParentPositionId,
               Name = position.Name,
               Description = position.Description,
               CreateTime = DateTime.UnixEpoch.AddTicks(position.CreateTime * 10000)
           })
           .ToArray();
    }

    public async Task<DeviceInfo[]> GetDevicesByPosition(string? PositionId = null, int? Page = 1, int PageSize = 30, CancellationToken Cancel = default)
    {
        var data = new GetDevicesByPositionRequest(PositionId, Page, PageSize);

        var json_request = JsonSerializer.Serialize(data);

        var client = await GetClientWithAccessToken(Cancel);

        var response = await client
           .PostAsJsonAsync("", data, __SerializerOptions, Cancel)
           .ConfigureAwait(false);

        var result = await response
           .EnsureSuccessStatusCode()
           .Content
           .ReadFromJsonAsync<GetDevicesByPositionResponse>(cancellationToken: Cancel);

        if (result is null)
            throw new GetDevicesByPositionException("Не удалось получить ответ от сервера")
            {
                RequestData = data
            };

        if (result.ErrorCode != ErrorCode.Success)
            throw new GetDevicesByPositionException($"Ошибка получения токена авторизации {result.Message} {result.MessageDetails}")
            {
                RequestData = data,
                ResponseData = result,
            };

        return result.Result.Data
           .Select(device => new DeviceInfo
           {
               Id = device.Id,
               ParentId = device.ParentId,
               PositionId = device.PositionId,
               DeviceName = device.DeviceName,
               CreateTime = DateTime.UnixEpoch.AddTicks(device.CreateTime * 10000),
               UpdateTime = DateTime.UnixEpoch.AddTicks(device.UpdateTime * 10000),
               TimeZone = device.TimeZone,
               Model = device.Model,
               ModelType = device.ModelType switch
               {
                   1 => DeviceModelType.GatewayWithChilds,
                   2 => DeviceModelType.GatewayWithoutChilds,
                   3 => DeviceModelType.SubDevice,
                   _ => throw new GetDevicesByPositionException($"Некорректное значение типа модели устройства {device.ModelType}")
                   {
                       RequestData = data,
                       ResponseData = result,
                   }
               },
               OnlineState = device.State switch
               {
                   0 => false,
                   1 => true,
                   _ => throw new GetDevicesByPositionException($"Некорректное значение состояния устройства {device.State}")
                   {
                       RequestData = data,
                       ResponseData = result,
                   }
               },
               FirmwareVersion = device.FirmwareVersion,
           })
           .ToArray();
    }

    public async Task<DeviceFeatureInfo[]> GetDeviceModelFeatures(string Model, string? ResourceId = null, CancellationToken Cancel = default)
    {
        var data = new GetDeviceModelFeaturesRequest(Model, ResourceId);

        var json_request = JsonSerializer.Serialize(data);

        var client = await GetClientWithAccessToken(Cancel);

        var response = await client
           .PostAsJsonAsync("", data, __SerializerOptions, Cancel)
           .ConfigureAwait(false);

        //var result_json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

        var result = await response
           .EnsureSuccessStatusCode()
           .Content
           .ReadFromJsonAsync<GetDeviceModelFeaturesResponse>(cancellationToken: Cancel);

        if (result is null)
            throw new GetDeviceModelFeaturesException("Не удалось получить ответ от сервера")
            {
                RequestData = data
            };

        if (result.ErrorCode != ErrorCode.Success)
            throw new GetDeviceModelFeaturesException($"Ошибка получения токена авторизации {result.Message} {result.MessageDetails}")
            {
                RequestData = data,
                ResponseData = result,
            };

        return result.Result
           .Select(info => new DeviceFeatureInfo
            {
               ResourceId = info.ResourceId,
               Name = info.Name,
               NameEn = info.NameEn,
               Description = info.Description,
               DescriptionEn = info.DescriptionEn,
               MinValue = info.MinValue,
               MaxValue = info.MaxValue,
               DefaultValue = info.DefaultValue,
               Unit = info.Unit,
               Server = info.Server,
               SubjectModel = info.SubjectModel,
               Enums = info.Enums,
               Access = info.Access switch
               {
                   0 => DeviceFeatureAccess.Read,
                   1 => DeviceFeatureAccess.Write,
                   2 => DeviceFeatureAccess.ReadWrite,
                   _=> (DeviceFeatureAccess)info.Access
               },
            })
           .ToArray();
    }

    public async Task<StatisticValueInfo[]> GetDeviceFeatureStatistic(
        string DeviceId, 
        IEnumerable<string> FeatureId,
        FeatureStatisticAggregationType AggregationType,
        DateTime StartTime,
        FeatureStatisticAggregationDimension Dimension = FeatureStatisticAggregationDimension.Interval30m,
        DateTime? EndTime = null,
        int? Size = null,
        CancellationToken Cancel = default)
    {
        var features = FeatureId as IReadOnlyCollection<string> ?? FeatureId.ToArray();

        List<int> aggregation_type;
        if (AggregationType is FeatureStatisticAggregationType.All or FeatureStatisticAggregationType.All2)
            aggregation_type = new List<int> { 0, 1, 2, 3, 4 };
        else
        {
            aggregation_type = new List<int>();

            if((AggregationType & FeatureStatisticAggregationType.Difference) == FeatureStatisticAggregationType.Difference)
                aggregation_type.Add(0);

            if((AggregationType & FeatureStatisticAggregationType.Min) == FeatureStatisticAggregationType.Min)
                aggregation_type.Add(1);

            if((AggregationType & FeatureStatisticAggregationType.Max) == FeatureStatisticAggregationType.Max)
                aggregation_type.Add(2);

            if((AggregationType & FeatureStatisticAggregationType.Average) == FeatureStatisticAggregationType.Average)
                aggregation_type.Add(3);

            if((AggregationType & FeatureStatisticAggregationType.Frequency) == FeatureStatisticAggregationType.Frequency)
                aggregation_type.Add(3);
        }

        var dimension = Dimension switch
        {
            FeatureStatisticAggregationDimension.Interval30m => "30m",
            FeatureStatisticAggregationDimension.Interval1h => "1h",
            FeatureStatisticAggregationDimension.Interval2h => "2h",
            FeatureStatisticAggregationDimension.Interval3h => "3h",
            FeatureStatisticAggregationDimension.Interval4h => "4h",
            FeatureStatisticAggregationDimension.Interval5h => "5h",
            FeatureStatisticAggregationDimension.Interval6h => "6h",
            FeatureStatisticAggregationDimension.Interval12h => "12h",
            FeatureStatisticAggregationDimension.Interval1d => "1d",
            FeatureStatisticAggregationDimension.Interval7d => "7d",
            _ => throw new InvalidEnumArgumentException(nameof(Dimension), (int)Dimension, typeof(FeatureStatisticAggregationDimension))
        };

        var start_time = StartTime.ToUniversalTime();
        var end_time = EndTime?.ToUniversalTime();

        var data = new GetDeviceFeatureStatisticRequest(DeviceId, aggregation_type, features, start_time, end_time, dimension, Size);

        //var json_request = JsonSerializer.Serialize(data, new JsonSerializerOptions(__SerializerOptions) { WriteIndented = true });

        var client = await GetClientWithAccessToken(Cancel);

        var response = await client
           .PostAsJsonAsync("", data, __SerializerOptions, Cancel)
           .ConfigureAwait(false);

        //var result_json = await response.Content.ReadAsStringAsync(Cancel).ConfigureAwait(false);

        var result = await response
           .EnsureSuccessStatusCode()
           .Content
           .ReadFromJsonAsync<GetDeviceFeatureStatisticResponse>(cancellationToken: Cancel);

        if (result is null)
            throw new GetDeviceFeatureStatisticException("Не удалось получить ответ от сервера")
            {
                RequestData = data
            };

        if (result.ErrorCode != ErrorCode.Success)
            throw new GetDeviceFeatureStatisticException($"Ошибка получения токена авторизации {result.Message} {result.MessageDetails}")
            {
                RequestData = data,
                ResponseData = result,
            };

        return result.Result.Data
           .Select(info => new StatisticValueInfo
            {
                DeviceId = info.SubjectId,
                FeatureId = info.ResourceId,
                Value = double.Parse(info.Value, CultureInfo.InvariantCulture),
                Time = info.TimeStamp is { } time_stamp ? TimeEx.UnixTimeFromTicks(time_stamp) : null,
                StartTime = TimeEx.UnixTimeFromTicks(info.StartTimeZone),
                EndTime = TimeEx.UnixTimeFromTicks(info.EndTimeZone),
                ValueType = info.AggrType switch
                {
                    0 => StatisticValueType.Difference,
                    1 => StatisticValueType.Min,
                    2 => StatisticValueType.Max,
                    3 => StatisticValueType.Average,
                    4 => StatisticValueType.Frequency,
                    _ => (StatisticValueType)info.AggrType
                },
            })
           .ToArray();
    }
}

public enum FeatureStatisticAggregationDimension
{
    Interval30m,
    Interval05h = Interval30m,
    Interval1h,
    Interval2h,
    Interval3h,
    Interval4h,
    Interval5h,
    Interval6h,
    Interval12h,
    Interval1d,
    Interval24h = Interval1d,
    Interval7d,
    Interval30d
}

[Flags]
public enum FeatureStatisticAggregationType : short
{
    Difference = 0b0_0001,
    Min        = 0b0_0010,
    Max        = 0b0_0100,
    Average    = 0b0_1000,
    Frequency  = 0b1_0000,
    All        = 0b1_1111,
    All2       = 0
}