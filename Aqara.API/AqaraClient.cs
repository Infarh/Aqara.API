using System.Net.Http.Json;
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
    private AccessTokenInfo? _AccessToken = null;

    private AccessTokenInfo? AccessToken
    {
        get => _AccessToken;// ??= GetAccessToken(_Configuration.TokenStorageFile);
        set
        {
            if (Equals(_AccessToken, value)) return;
            _AccessToken = value;
            value?.SaveToFile(_Configuration.TokenStorageFile);
        }
    }

    private static AccessTokenInfo? GetAccessToken(string FileName) => File.Exists(FileName) ? AccessTokenInfo.ReadFromFile(FileName) : null;

    private static async Task<AccessTokenInfo?> GetAccessTokenAsync(string FileName, CancellationToken Cancel) =>
        File.Exists(FileName)
            ? await AccessTokenInfo.ReadFromFileAsync(FileName, Cancel).ConfigureAwait(false)
            : null;

    private HttpClient GetClient(string? AccessToken = null) => _Client.AddHeaders(_Configuration, AccessToken);

    public AqaraClient(HttpClient Client, IOptionsSnapshot<AqaraClientConfig> Configuration)
    {
        _Client = Client;
        _Configuration = Configuration.Value;
    }

    private async Task<HttpClient> GetClientWithAccessToken(CancellationToken Cancel)
    {
        if (AccessToken is not { } token)
        {
            var token_file = _Configuration.TokenStorageFile;
            token = File.Exists(token_file)
                ? _AccessToken = await AccessTokenInfo.ReadFromFileAsync(token_file, Cancel).ConfigureAwait(false)
                : throw new InvalidOperationException("Отсутствует токен доступа");
        }

        if (token.IsExpire)
            await RefreshAccessToken(Cancel).ConfigureAwait(false);

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

        AccessToken = token;

        return token;
    }

    public async Task<AccessTokenInfo> RefreshAccessToken(CancellationToken Cancel = default)
    {
        var token = _AccessToken ?? await GetAccessTokenAsync(_Configuration.TokenStorageFile, Cancel).ConfigureAwait(false);
        if (token is null)
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

        AccessToken = result_token;
        return result_token;
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
               CreationTime = DateTime.UnixEpoch.AddTicks(position.CreateTime * 10000)
           })
           .ToArray();
    }
}
