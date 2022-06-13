using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Channels;

using Aqara.API.DTO;
using Aqara.API.Exceptions;
using Aqara.API.Infrastructure;

using Microsoft.Extensions.Options;

namespace Aqara.API;

public class AqaraClient
{
    private readonly HttpClient _Client;
    private readonly AqaraClientConfig _Configuration;
    private AccessTokenInfo? _AccessToken = null;

    private AccessTokenInfo? AccessToken
    {
        get => _AccessToken ??= GetAccessToken(_Configuration.TokenStorageFile);
        set
        {
            if (Equals(_AccessToken, value)) return;
            _AccessToken = value;
            value?.SaveToFile(_Configuration.TokenStorageFile);
        }
    }

    private static AccessTokenInfo? GetAccessToken(string FileName) => File.Exists(FileName) ? AccessTokenInfo.ReadFromFile(FileName) : null;

    private HttpClient ClientWithoutAccessToken => _Client.AddHeaders(_Configuration);

    private HttpClient Client => ClientWithoutAccessToken.AddAccessToken(AccessToken);

    public AqaraClient(HttpClient Client, IOptionsSnapshot<AqaraClientConfig> Configuration)
    {
        _Client = Client;
        _Configuration = Configuration.Value;
        if (_Configuration.TokenStorageFile is { Length: > 0 } access_token_file && File.Exists(access_token_file))
            _AccessToken = AccessTokenInfo.ReadFromFile(access_token_file);
    }

    public async Task<string?> RequestAuthorizationKey(
        string Account,
        string AccessTokenValidity = "1h",
        AccountType AccountType = AccountType.Aqara,
        CancellationToken Cancel = default)
    {
        var data = new AuthorizationCodeRequest(Account, AccessTokenValidity, AccountType);

        var result = await ClientWithoutAccessToken
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

        var response = await ClientWithoutAccessToken
           .PostAsJsonAsync("", data, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull }, Cancel)
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
        if (_AccessToken is null)
            throw new InvalidOperationException("Невозможно обновить токен авторизации потом, что отсутствует информация о старом токене авторизации");

        var data = new RefreshAccessTokenRequest(_AccessToken.RefreshToken);

        var response = await ClientWithoutAccessToken
           .PostAsJsonAsync("", data, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull }, Cancel)
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

        var token = new AccessTokenInfo(access_token, refresh_token, expire, open_id, DateTime.Now);

        AccessToken = token;

        return token;
    }
}
