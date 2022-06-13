using System.Text.Json.Serialization;
using Aqara.API.Infrastructure;

namespace Aqara.API.DTO;

public class AccessTokenRequest
{
    public AccessTokenRequest(string VerificationCode, string? Account = null, AccountType AccountType = AccountType.Aqara) => 
        Data = new AccessTokenRequestData(VerificationCode, Account, AccountType.ToInt());

    [JsonPropertyName("intent")]
    public string Intent => Addresses.Auth.ObtainAccessToken;

    [JsonPropertyName("data")]
    public AccessTokenRequestData Data { get; }

    public class AccessTokenRequestData
    {
        internal AccessTokenRequestData(string VerificationCode, string? Account, int AccountType)
        {
            this.VerificationCode = VerificationCode;
            this.Account = Account;
            this.AccountType = AccountType;
        }

        [JsonPropertyName("authCode")]
        public string VerificationCode { get; }

        [JsonPropertyName("account")]
        public string? Account { get; }

        [JsonPropertyName("accountType")]
        public int AccountType { get; }
    }
}

public class AccessTokenResponse : Response
{
    [JsonPropertyName("result")]
    public AccessTokenResponseResult? Result { get; init; }

    public class AccessTokenResponseResult
    {
        [JsonPropertyName("expiresIn")]
        public int ExpiresIn { get; init; }

        [JsonPropertyName("openId")]
        public string OpenId { get; init; }

        [JsonPropertyName("accessToken")]
        public string AccessToken { get; init; } = null!;

        [JsonPropertyName("refreshToken")]
        public string RefreshToken { get; init; } = null!;
    }
}