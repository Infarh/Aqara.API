using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http.Json;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

using Aqara.API.DTO;
using Aqara.API.Exceptions;
using Aqara.API.Infrastructure;
using Aqara.API.Models;
using Microsoft.Extensions.Logging;

namespace Aqara.API;

/// <summary>Клиент Aqara API сервера</summary>
public class AqaraClient
{
    /// <summary>Параметры сериализации с подавлением отсутствующих значений</summary>
    private static readonly JsonSerializerOptions __SerializerOptions = new JsonSerializerOptions()
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) },
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
    }
    .WithContext<DTOSerializerContext>();

    /// <summary>Базовый сетевой клиент для отправки запросов</summary>
    private readonly HttpClient _Client;

    /// <summary>Конфигурация клиента</summary>
    private readonly AqaraClientConfig _Configuration;

    /// <summary>Сервис хранилища токена доступа</summary>
    private readonly IAccessTokenSource _AccessTokenSource;

    /// <summary>Логгер</summary>
    private readonly ILogger<AqaraClient> _Logger;

    /// <summary>Инициализация клиента сервера Aqara API</summary>
    /// <param name="Client">Клиент Http для отправки запросов серверу</param>
    /// <param name="AccessTokenSource">Хранилище токена авторизации</param>
    /// <param name="Logger">Логгер</param>
    /// <param name="Configuration">Конфигурация клиента</param>
    public AqaraClient(HttpClient Client, IAccessTokenSource AccessTokenSource, ILogger<AqaraClient> Logger, AqaraClientConfig Configuration)
    {
        _Client = Client;
        _Configuration = Configuration;
        _AccessTokenSource = AccessTokenSource;
        _Logger = Logger;
    }

    /// <summary>Получить клиента с конфигурацией заголовков запроса</summary>
    /// <param name="AccessToken">Токен авторизации</param>
    /// <returns>Сконфигурированный клиент</returns>
    private HttpClient GetClient(string? AccessToken = null) => _Client.AddHeaders(_Configuration, AccessToken);

    /// <summary>Получить клиента с конфигурацией заголовков запроса с добавленным токеном авторизации</summary>
    /// <param name="Cancel">Флаг отмены асинхронной операции</param>
    /// <returns>Сконфигурированный клиент</returns>
    /// <exception cref="InvalidOperationException">В случае если не удалось получить данные от сервиса</exception>
    private async Task<HttpClient> GetClientWithAccessToken(CancellationToken Cancel)
    {
        if (await _AccessTokenSource.GetAccessToken(Cancel).ConfigureAwait(false) is not { } token)
            throw new InvalidOperationException("Отсутствует токен доступа");

        if (!token.IsExpire)
            return GetClient(token.AccessToken).AddAccessToken(token);

        if (_Logger.IsEnabled(LogLevel.Trace))
            _Logger.LogTrace("Токен доступа устарел");

        token = await RefreshAccessToken(Cancel).ConfigureAwait(false);
        if (token is null || token.IsExpire)
        {
            _Logger.LogError("Не удалось выполнить обновление токена доступа");
            throw new InvalidOperationException("Не удалось обновить токен доступа");
        }

        await _AccessTokenSource.SetAccessToken(token, Cancel);

        return GetClient(token.AccessToken).AddAccessToken(token);
    }

    /// <summary>Запрос кода авторизации</summary>
    /// <param name="Account">Аккаунт Aqara (адрес электронной почты, на который придёт код авторизации, либо телефонный номер для СМС)</param>
    /// <param name="AccessTokenValidity">Интервал времени, в который запрошенный код будет актуальным</param>
    /// <param name="AccountType">Тип аккаунта (логин Aqara, проект, виртуальный аккаунт)</param>
    /// <param name="Cancel">Флаг отмены асинхронной операции</param>
    /// <returns>Строка с кодом авторизации - если её вышлет сервер (не вышлет - код придёт на указанный адрес почты)</returns>
    /// <exception cref="RequestAuthorizationCodeException">В случае ошибки авторизации</exception>
    public async Task<string?> GetAuthorizationKey(
        string Account,
        string AccessTokenValidity = "1h",
        AccountType AccountType = AccountType.Aqara,
        CancellationToken Cancel = default)
    {
        var data = new AuthorizationCodeRequest(Account, AccessTokenValidity, AccountType);

        var timer = Stopwatch.StartNew();
        var result = await GetClient()
           .PostAsJsonAsync("", data, Cancel)
           .GetJsonResult<AuthorizationCodeResponse>(Cancel)
           .ConfigureAwait(false);
        timer.Stop();

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

        if (_Logger.IsEnabled(LogLevel.Information))
            _Logger.LogInformation("Запрос кода авторизации выполнен успешно за {0}мс", timer.ElapsedMilliseconds);

        return result
           .Result!
           .AuthorizationCode;
    }

    /// <summary>Запрос токена авторизации на основе кода авторизации</summary>
    /// <param name="VerificationCode">Код авторизации, полученный при помощи <see cref="GetAuthorizationKey"/></param>
    /// <param name="Account">Аккаунт, указанный в запросе кода авторизации <see cref="GetAuthorizationKey"/></param>
    /// <param name="AccountType">Тип аккаунта из запроса <see cref="GetAuthorizationKey"/></param>
    /// <param name="Cancel"></param>
    /// <returns>Токен авторизации</returns>
    /// <exception cref="RequestAccessTokenException">В случае если не удалось получить данные от сервиса</exception>
    public async Task<AccessTokenInfo> GetAccessToken(
        string VerificationCode,
        string? Account = null,
        AccountType AccountType = AccountType.Aqara,
        CancellationToken Cancel = default)
    {
        var data = new AccessTokenRequest(VerificationCode, Account, AccountType);

        var timer = Stopwatch.StartNew();
        var result = await GetClient()
           .PostAsJsonAsync("", data, __SerializerOptions, Cancel)
           .GetJsonResult<AccessTokenResponse>(Cancel)
           .ConfigureAwait(false);
        timer.Stop();

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

        if (_Logger.IsEnabled(LogLevel.Information))
            _Logger.LogInformation("Токен доступа получен за {0}мс. Время жизни {1}c (до {2})",
                timer.ElapsedMilliseconds, token.Expires, token.ExpiresTime);

        await _AccessTokenSource.SetAccessToken(token, Cancel).ConfigureAwait(false);

        return token;
    }

    /// <summary>Обновить токен авторизации</summary>
    /// <param name="Cancel">Флаг отмены асинхронной операции</param>
    /// <returns>Новый токен авторизации</returns>
    /// <exception cref="InvalidOperationException">Возникает при отсутствии токена обновления токена авторизации</exception>
    /// <exception cref="RefreshAccessTokenException">В случае если не удалось получить данные от сервиса</exception>
    public async Task<AccessTokenInfo> RefreshAccessToken(CancellationToken Cancel = default)
    {
        if (await _AccessTokenSource.GetAccessToken(Cancel).ConfigureAwait(false) is { RefreshToken: var refresh_token })
            return await RefreshAccessToken(refresh_token, Cancel);

        _Logger.LogError("Не удалось получить токен обновления токена доступа");
        throw new InvalidOperationException("Невозможно обновить токен авторизации потом, что отсутствует информация о старом токене авторизации");
    }

    /// <summary>Обновить токен авторизации</summary>
    /// <param name="RefreshToken">Токен обновления токена авторизации</param>
    /// <param name="Cancel">Флаг отмены асинхронной операции</param>
    /// <returns>Новый токен авторизации</returns>
    /// <exception cref="InvalidOperationException">Возникает при отсутствии токена обновления токена авторизации</exception>
    /// <exception cref="RefreshAccessTokenException">В случае если не удалось получить данные от сервиса</exception>
    public async Task<AccessTokenInfo> RefreshAccessToken(string RefreshToken, CancellationToken Cancel = default)
    {
        var data = new RefreshAccessTokenRequest(RefreshToken);

        var timer = Stopwatch.StartNew();
        var result = await GetClient()
           .PostAsJsonAsync("", data, __SerializerOptions, Cancel)
           .GetJsonResult<RefreshAccessTokenResponse>(Cancel)
           .ConfigureAwait(false);
        timer.Stop();

        if (result is null)
            throw new RefreshAccessTokenException("Не удалось получить ответ от сервера")
            {
                RequestData = data
            };

        if (result.ErrorCode != ErrorCode.Success)
            throw new RefreshAccessTokenException($"Ошибка получения токена обновления авторизации {result.Message} {result.MessageDetails}")
            {
                RequestData = data,
                ResponseData = result,
            };

        var access_token = result.Result!.AccessToken;
        var refresh_token = result.Result.RefreshToken;
        var expire = int.Parse(result.Result.ExpiresIn);
        var open_id = result.Result.OpenId;

        var result_token = new AccessTokenInfo(access_token, refresh_token, expire, open_id, DateTime.Now);

        if (_Logger.IsEnabled(LogLevel.Information))
            _Logger.LogInformation("Токен доступа обновлён за {0}мс. Полученный токен истекает через {1}с ({2})",
                timer.ElapsedMilliseconds, result_token.Expires, result_token.ExpiresTime);

        return await _AccessTokenSource.SetAccessToken(result_token, Cancel);
    }

    /// <summary>Получить список местоположений</summary>
    /// <param name="ParentPositionId">Родительское местоположение (если указано)</param>
    /// <param name="Page">Номер страницы (должно быть больше 0)</param>
    /// <param name="PageSize">Размер страницы должен быть больше 0</param>
    /// <param name="Cancel">Флаг отмены асинхронной операции</param>
    /// <returns>Массив местоположений</returns>
    /// <exception cref="GetPositionsException">В случае если не удалось получить данные от сервиса</exception>
    public async Task<PositionInfo[]> GetPositions(string? ParentPositionId = null, int? Page = 1, int? PageSize = 30, CancellationToken Cancel = default)
    {
        var data = new GetPositionsRequest(ParentPositionId, Page, PageSize);

        var client = await GetClientWithAccessToken(Cancel);

        var timer = Stopwatch.StartNew();
        var result = await client
           .PostAsJsonAsync("", data, __SerializerOptions, Cancel)
           .GetJsonResult<GetPositionsResponse>(Cancel)
           .ConfigureAwait(false);
        timer.Stop();

        if (result is null)
            throw new GetPositionsException("Не удалось получить ответ от сервера")
            {
                RequestData = data
            };

        if (result.ErrorCode != ErrorCode.Success)
            throw new GetPositionsException($"Ошибка получения местоположений {result.Message} {result.MessageDetails}")
            {
                RequestData = data,
                ResponseData = result,
            };

        if (_Logger.IsEnabled(LogLevel.Information))
            if (ParentPositionId is null)
                _Logger.LogInformation("Запрос местоположений выполнен успешно за {0}мс. Получено мест {1}. Всего мест {2}",
                    timer.ElapsedMilliseconds, result.Result.Data.Count, result.Result.TotalCount);
            else
                _Logger.LogInformation("Запрос местоположений для родительского положения {0} выполнен успешно за {1}мс. Получено мест {2}. Всего мест {3}",
                    ParentPositionId, timer.ElapsedMilliseconds, result.Result.Data.Count, result.Result.TotalCount);

        return result
           .Result
           .Data
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

    /// <summary>Получить перечень устройств по заданному местоположению (если положеие не указано, то возвращается полный список устройств</summary>
    /// <param name="PositionId">Идентификатор местоположения (если не указан, то будет возвращён полный список всех устройств)</param>
    /// <param name="Page">Номер страницы (начиная с 1)</param>
    /// <param name="PageSize">Количество страниц (должно быть больше 0)</param>
    /// <param name="Cancel">Флаг отмены асинхронной операции</param>
    /// <returns>Массив устройств указанного местоположения</returns>
    /// <exception cref="GetDevicesByPositionException">В случае если не удалось получить данные от сервиса</exception>
    public async Task<DeviceInfo[]> GetDevicesByPosition(string? PositionId = null, int? Page = 1, int PageSize = 30, CancellationToken Cancel = default)
    {
        var data = new GetDevicesByPositionRequest(PositionId, Page, PageSize);

        var client = await GetClientWithAccessToken(Cancel);

        var timer = Stopwatch.StartNew();
        var result = await client
           .PostAsJsonAsync("", data, __SerializerOptions, Cancel)
           .GetJsonResult<GetDevicesByPositionResponse>(Cancel)
           .ConfigureAwait(false);
        timer.Stop();

        if (result is null)
            throw new GetDevicesByPositionException("Не удалось получить ответ от сервера")
            {
                RequestData = data
            };

        if (result.ErrorCode != ErrorCode.Success)
            throw new GetDevicesByPositionException($"Ошибка получения перечня устройств по заданному положению {result.Message} {result.MessageDetails}")
            {
                RequestData = data,
                ResponseData = result,
            };

        if (_Logger.IsEnabled(LogLevel.Information))
            if (PositionId is null)
                _Logger.LogInformation("Запрос устройств выполнен успешно за {0}мс. Получено устройств {1}. Всего устройств {2}",
                    timer.ElapsedMilliseconds,
                    result.Result.Data.Length, result.Result.TotalCount);
            else
                _Logger.LogInformation("Запрос устройств для положения {0} выполнен успешно за {1}мс. Получено устройств {2}. Всего устройств {3}",
                    PositionId, timer.ElapsedMilliseconds, result.Result.Data.Length, result.Result.TotalCount);

        return result
           .Result
           .Data
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

    /// <summary>Получить перечень возможностей устройства</summary>
    /// <param name="Model">Идентификатор модели устройства</param>
    /// <param name="ResourceId">Идентификатор ресурса (если не указан, будет возвращена информация о всех возможностях)</param>
    /// <param name="Cancel">Флаг отмены асинхронной операции</param>
    /// <returns>Массив с информацией о возможностях устройства</returns>
    /// <exception cref="GetDeviceModelFeaturesException">В случае если не удалось получить данные от сервиса</exception>
    public async Task<DeviceFeatureInfo[]> GetDeviceModelFeatures(string Model, string? ResourceId = null, CancellationToken Cancel = default)
    {
        var data = new GetDeviceModelFeaturesRequest(Model, ResourceId);

        var client = await GetClientWithAccessToken(Cancel);

        var timer = Stopwatch.StartNew();
        var result = await client
           .PostAsJsonAsync("", data, __SerializerOptions, Cancel)
           .GetJsonResult<GetDeviceModelFeaturesResponse>(Cancel)
           .ConfigureAwait(false);
        timer.Stop();

        if (result is null)
            throw new GetDeviceModelFeaturesException("Не удалось получить ответ от сервера")
            {
                RequestData = data
            };

        if (result.ErrorCode != ErrorCode.Success)
            throw new GetDeviceModelFeaturesException($"Ошибка получения описания модели {result.Message} {result.MessageDetails}")
            {
                RequestData = data,
                ResponseData = result,
            };

        if (_Logger.IsEnabled(LogLevel.Information))
            if (ResourceId is null)
                _Logger.LogInformation("Запрос параметров для модели {0} выполнен успешно за {1}мс. Получено параметров {2}.",
                    Model, timer.ElapsedMilliseconds, result.Result.Length);
            else
                _Logger.LogInformation("Запрос параметров для модели {0} ({1}) выполнен успешно за {2}мс. Получено параметров {3}.",
                    Model, ResourceId, timer.ElapsedMilliseconds, result.Result.Length);

        return result
           .Result
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
                   _ => (DeviceFeatureAccess)info.Access
               },
           })
           .ToArray();
    }

    /// <summary>Получить статистические данные о параметрах устройства</summary>
    /// <param name="DeviceId">Идентификатор устройства</param>
    /// <param name="FeatureId">Перечисление идентификаторов интересующих параметров</param>
    /// <param name="AggregationType">Виды статистических данных</param>
    /// <param name="StartTime">Время начала сбора данных</param>
    /// <param name="Dimension">Разрешающая способность выборки</param>
    /// <param name="EndTime">Время окончания выборки</param>
    /// <param name="Size">Размер выборки</param>
    /// <param name="Cancel">Флаг отмены асинхронной операции</param>
    /// <returns>Массив со значениями выборки</returns>
    /// <exception cref="InvalidEnumArgumentException">При некорректном значении параметра <paramref name="Dimension"/></exception>
    /// <exception cref="GetDeviceFeatureStatisticException">В случае если не удалось получить данные от сервиса</exception>
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

            if ((AggregationType & FeatureStatisticAggregationType.Difference) == FeatureStatisticAggregationType.Difference)
                aggregation_type.Add(0);

            if ((AggregationType & FeatureStatisticAggregationType.Min) == FeatureStatisticAggregationType.Min)
                aggregation_type.Add(1);

            if ((AggregationType & FeatureStatisticAggregationType.Max) == FeatureStatisticAggregationType.Max)
                aggregation_type.Add(2);

            if ((AggregationType & FeatureStatisticAggregationType.Average) == FeatureStatisticAggregationType.Average)
                aggregation_type.Add(3);

            if ((AggregationType & FeatureStatisticAggregationType.Frequency) == FeatureStatisticAggregationType.Frequency)
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

        var client = await GetClientWithAccessToken(Cancel);

        var timer = Stopwatch.StartNew();
        var result = await client
           .PostAsJsonAsync("", data, __SerializerOptions, Cancel)
           .GetJsonResult<GetDeviceFeatureStatisticResponse>(Cancel)
           .ConfigureAwait(false);
        timer.Stop();

        if (result is null)
            throw new GetDeviceFeatureStatisticException("Не удалось получить ответ от сервера")
            {
                RequestData = data
            };

        if (result.ErrorCode != ErrorCode.Success)
            throw new GetDeviceFeatureStatisticException($"Ошибка получения статистики параметров устройства {result.Message} {result.MessageDetails}")
            {
                RequestData = data,
                ResponseData = result,
            };

        if (_Logger.IsEnabled(LogLevel.Information))
            _Logger.LogInformation(
                "Запрос статистики устройства {0} для параметров {1} выполнен успешно за {2}мс. " +
                "Интервал сбора статистики {3} - {4}. " +
                "Разрешающая способность {5}. " +
                "Тип значений {6}. " +
                "Размер выборки {7}. " +
                "Получено значений {8}.",
                DeviceId,
                string.Join(',', features),
                timer.ElapsedMilliseconds,
                StartTime, EndTime ?? DateTime.Now,
                Dimension, AggregationType, Size,
                result.Result.Data.Length);

        return result
           .Result
           .Data
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

    /// <summary>Получить значения параметров указанных устройств</summary>
    /// <param name="Features">Параметры запроса устройств и их параметров</param>
    /// <param name="Cancel">Флаг отмены асинхронной операции</param>
    /// <returns>Значения параметров устройств</returns>
    /// <exception cref="GetDevicesFeaturesValuesException">В случае если не удалось получить данные от сервиса</exception>
    public async Task<DeviceFeatureValue[]> GetDevicesFeaturesValues((string DeviceId, string[] FeatureId)[] Features, CancellationToken Cancel = default)
    {
        var data = new GetDevicesFeaturesValuesRequest(Features);

        var client = await GetClientWithAccessToken(Cancel);

        var timer = Stopwatch.StartNew();
        var result = await client
           .PostAsJsonAsync("", data, __SerializerOptions, Cancel)
           .GetJsonResult<GetDevicesFeaturesValuesResponse>(Cancel)
           .ConfigureAwait(false);
        timer.Stop();

        if (result is null)
            throw new GetDevicesFeaturesValuesException("Не удалось получить ответ от сервера")
            {
                RequestData = data
            };

        if (result.ErrorCode != ErrorCode.Success)
            throw new GetDevicesFeaturesValuesException($"Ошибка получения значений параметров устройств {result.Message} {result.MessageDetails}")
            {
                RequestData = data,
                ResponseData = result,
            };

        if (_Logger.IsEnabled(LogLevel.Information))
            _Logger.LogInformation("Запрос значений параметров ({1}) выполнен за {0}мс. Получено значений {2}",
                timer.ElapsedMilliseconds,
                string.Join(';', Features.Select(f => $"{f.DeviceId},{string.Join(',', f.FeatureId)}")),
                result.Result.Length);
        
        return result
           .Result
           .Select(value => new DeviceFeatureValue
           {
               DeviceId = value.DeviceId,
               FeatureId = value.FeatureId,
               Time = TimeEx.UnixTimeFromTicks(value.TimeStamp),
               Value = double.Parse(value.Value, CultureInfo.InvariantCulture)
           })
           .ToArray();
    }

    /// <summary>Получить значения параметров устройства</summary>
    /// <param name="DeviceId">Идентификатор устройства</param>
    /// <param name="FeaturesIds">Идентификаторы требуемых параметров</param>
    /// <returns>Массив значений запрошенных параметров</returns>
    /// <exception cref="SetDevicesFeaturesValuesException">В случае если не удалось получить данные от сервиса</exception>
    public async Task<DeviceFeatureValue[]> GetDeviceFeaturesValues(string DeviceId, params string[] FeaturesIds) => await GetDevicesFeaturesValues(new[] { (DeviceId, FeaturesIds) }).ConfigureAwait(false);

    /// <summary>Получить значения параметров устройства</summary>
    /// <param name="Cancel">Флаг отмены асинхронной операции</param>
    /// <param name="DeviceId">Идентификатор устройства</param>
    /// <param name="FeaturesIds">Идентификаторы требуемых параметров</param>
    /// <returns>Массив значений параметров устройства</returns>
    /// <exception cref="SetDevicesFeaturesValuesException">В случае если не удалось получить данные от сервиса</exception>
    public async Task<DeviceFeatureValue[]> GetDeviceFeaturesValues(CancellationToken Cancel, string DeviceId, params string[] FeaturesIds) => await GetDevicesFeaturesValues(new[] { (DeviceId, FeaturesIds) }, Cancel).ConfigureAwait(false);

    /// <summary>Получить значение параметра устройства</summary>
    /// <param name="DeviceId">Идентификатор устройства</param>
    /// <param name="FeatureId">Идентификатор параметра</param>
    /// <param name="Cancel">Флаг отмены асинхронной операции</param>
    /// <returns>Значение параметра</returns>
    /// <exception cref="SetDevicesFeaturesValuesException">В случае если не удалось получить данные от сервиса</exception>
    public async Task<DeviceFeatureValue> GetDeviceFeatureValue(string DeviceId, string FeatureId, CancellationToken Cancel = default)
    {
        var values = await GetDeviceFeaturesValues(Cancel, DeviceId, FeatureId).ConfigureAwait(false);
        return values[0];
    }

    /// <summary>Установка значения параметров устройств</summary>
    /// <param name="Values">Идентификаторы устанавливаемых параметров устройств</param>
    /// <param name="Cancel">Флаг отмены асинхронной операции</param>
    /// <returns>Задача установки значения параметров</returns>
    /// <exception cref="SetDevicesFeaturesValuesException">В случае если не удалось получить данные от сервиса</exception>
    public async Task SetDevicesFeaturesValues((string DeviceId, (string FeatureId, double Value)[] Values)[] Values, CancellationToken Cancel = default)
    {
        var data = new SetDevicesFeaturesValuesRequest(Values);

        var client = await GetClientWithAccessToken(Cancel);

        var timer = Stopwatch.StartNew();
        var result = await client
           .PostAsJsonAsync("", data, __SerializerOptions, Cancel)
           .GetJsonResult<SetDevicesFeaturesValuesResponse>(Cancel)
           .ConfigureAwait(false);
        timer.Stop();

        if (result is null)
            throw new SetDevicesFeaturesValuesException("Не удалось получить ответ от сервера")
            {
                RequestData = data
            };

        if (result.ErrorCode != ErrorCode.Success)
            throw new SetDevicesFeaturesValuesException($"Ошибка запроса установки значений {result.Message} {result.MessageDetails}")
            {
                RequestData = data,
                ResponseData = result,
            };

        if (result.Results.Any(r => r.ErrorCode != 0))
            throw new SetDevicesFeaturesValuesException($"Ошибка установки значения свойств {string.Join(',', result.Results.Select(r => $"{r.DeviceId}:{r.Error}"))}")
            {
                RequestData = data,
                ResponseData = result,
                ErrorValues = result.Results.Where(r => r.Error != 0).ToArray(),
            };
    }
}