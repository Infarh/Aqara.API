using System.Text.Json.Serialization;

namespace Aqara.API.DTO;

public class RefreshAccessTokenRequest
{
    public RefreshAccessTokenRequest(string RefreshToken) => Data = new(RefreshToken);

    [JsonPropertyName("intent")]
    public string Intent => Addresses.Auth.RefreshAccessToken;

    public RefreshAccessTokenRequestData Data { get; }

    public class RefreshAccessTokenRequestData
    {
        internal RefreshAccessTokenRequestData(string RefreshToken) => this.RefreshToken = RefreshToken;

        [JsonPropertyName("refreshToken")]
        public string RefreshToken { get; }
    }
}

public class RefreshAccessTokenResponse : AccessTokenResponse { }
