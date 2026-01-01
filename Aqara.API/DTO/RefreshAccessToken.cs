using System.Text.Json.Serialization;

namespace Aqara.API.DTO;

/// <summary>Запрос обновления токена доступа</summary>
public class RefreshAccessTokenRequest
{
    /// <summary>Создает пустой запрос</summary>
    public RefreshAccessTokenRequest() { }

    /// <summary>Создает запрос с токеном обновления</summary>
    /// <param name="RefreshToken">Токен обновления</param>
    public RefreshAccessTokenRequest(string RefreshToken) => Data = new(RefreshToken);

    /// <summary>Идентификатор операции API</summary>
    [JsonPropertyName("intent")]
    public string Intent => Addresses.Auth.RefreshAccessToken;

    /// <summary>Данные запроса</summary>
    public RefreshAccessTokenRequestData Data { get; set; } = null!;

    /// <summary>Данные запроса на обновление токена</summary>
    public class RefreshAccessTokenRequestData
    {
        /// <summary>Создает пустые данные запроса</summary>
        public RefreshAccessTokenRequestData() { }

        /// <summary>Создает данные запроса с токеном</summary>
        /// <param name="RefreshToken">Токен обновления</param>
        public RefreshAccessTokenRequestData(string RefreshToken) => this.RefreshToken = RefreshToken;

        /// <summary>Токен обновления</summary>
        [JsonPropertyName("refreshToken")]
        public string RefreshToken { get; set; } = null!;
    }
}

/// <summary>Ответ на обновление токена доступа</summary>
public class RefreshAccessTokenResponse : AccessTokenResponse { }
