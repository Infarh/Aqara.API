using System.Text.Json.Serialization;

namespace Aqara.API.DTO;

public class RefreshAccessTokenRequest
{
    public RefreshAccessTokenRequest() { }

    public RefreshAccessTokenRequest(string RefreshToken) => Data = new(RefreshToken);

    [JsonPropertyName("intent")]
    public string Intent => Addresses.Auth.RefreshAccessToken;

    public RefreshAccessTokenRequestData Data { get; set; } = null!;

    public class RefreshAccessTokenRequestData
    {
        public RefreshAccessTokenRequestData() { }

        public RefreshAccessTokenRequestData(string RefreshToken) => this.RefreshToken = RefreshToken;

        [JsonPropertyName("refreshToken")]
        public string RefreshToken { get; set; } = null!;
    }
}

public class RefreshAccessTokenResponse : AccessTokenResponse { }
