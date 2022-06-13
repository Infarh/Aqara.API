using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
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
            if(Equals(_AccessToken, value)) return;
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

    //public async Task<AccessTokenInfo> ObtainAccessToken(string VerificationCode)
    //{

    //}
}
