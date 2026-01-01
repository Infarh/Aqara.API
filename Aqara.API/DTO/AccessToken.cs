using System.Text.Json;
using System.Text.Json.Serialization;

using Aqara.API.Infrastructure;

namespace Aqara.API.DTO;

/// <summary>Запрос на получение токена доступа</summary>
public class AccessTokenRequest
{
    /// <summary>Создает пустой запрос</summary>
    public AccessTokenRequest() { }

    /// <summary>Создает запрос с параметрами</summary>
    /// <param name="VerificationCode">Код авторизации</param>
    /// <param name="Account">Аккаунт пользователя</param>
    /// <param name="AccountType">Тип аккаунта</param>
    public AccessTokenRequest(string VerificationCode, string? Account = null, AccountType AccountType = AccountType.Aqara) =>
        Data = new AccessTokenRequestData(VerificationCode, Account, AccountType.ToInt());

    /// <summary>Идентификатор операции API</summary>
    [JsonPropertyName("intent")]
    public string Intent => Addresses.Auth.ObtainAccessToken;

    /// <summary>Данные запроса</summary>
    [JsonPropertyName("data")]
    public AccessTokenRequestData Data { get; set; } = null!;

    /// <summary>Данные запроса токена доступа</summary>
    public class AccessTokenRequestData
    {
        /// <summary>Создает пустые данные запроса</summary>
        public AccessTokenRequestData() { }

        /// <summary>Создает данные запроса с параметрами</summary>
        /// <param name="VerificationCode">Код авторизации</param>
        /// <param name="Account">Аккаунт пользователя</param>
        /// <param name="AccountType">Тип аккаунта</param>
        public AccessTokenRequestData(string VerificationCode, string? Account, int AccountType)
        {
            this.VerificationCode = VerificationCode;
            this.Account = Account;
            this.AccountType = AccountType;
        }

        /// <summary>Код авторизации</summary>
        [JsonPropertyName("authCode")]
        public string VerificationCode { get; set; } = null!;

        /// <summary>Аккаунт пользователя</summary>
        [JsonPropertyName("account")]
        public string? Account { get; set; }

        /// <summary>Тип аккаунта</summary>
        [JsonPropertyName("accountType")]
        public int AccountType { get; set; }

        /// <summary>Возвращает строковое представление данных</summary>
        /// <returns>Строка с кодом и аккаунтом</returns>
        public override string ToString() => $"code:{VerificationCode},account:{Account ?? "null"}";
    }

    /// <summary>Возвращает строковое представление запроса</summary>
    /// <returns>Строка с intent и данными</returns>
    public override string ToString() => $"{Intent}:{{{Data}}}";
}

/// <summary>Ответ на получение токена доступа</summary>
public class AccessTokenResponse : Response
{
    /// <summary>Результат запроса</summary>
    [JsonPropertyName("result")]
    [JsonConverter(typeof(EmptyStringToNullConverter))]
    public AccessTokenResponseResult? Result { get; set; }

    /// <summary>Данные результата получения токена</summary>
    public class AccessTokenResponseResult
    {
        /// <summary>Время жизни токена в секундах</summary>
        [JsonPropertyName("expiresIn")]
        public string ExpiresIn { get; set; } = null!;

        /// <summary>Идентификатор пользователя</summary>
        [JsonPropertyName("openId")]
        public string OpenId { get; set; } = null!;

        /// <summary>Токен доступа</summary>
        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; } = null!;

        /// <summary>Токен обновления</summary>
        [JsonPropertyName("refreshToken")]
        public string RefreshToken { get; set; } = null!;
    }

    /// <summary>Возвращает строковое представление ответа</summary>
    /// <returns>Строка с кодом и сообщением</returns>
    public override string ToString() => $"code:{Code}({ErrorCode}),msg:{Message}";

    /// <summary>Конвертер, преобразующий пустые строки в null</summary>
    private class EmptyStringToNullConverter : JsonConverter<AccessTokenResponseResult>
    {
        /// <summary>Читает данные результата</summary>
        /// <param name="reader">Источник JSON</param>
        /// <param name="type">Тип результата</param>
        /// <param name="opt">Настройки сериализатора</param>
        /// <returns>Данные результата или null</returns>
        public override AccessTokenResponseResult? Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions opt)
        {
            if (reader.TokenType == JsonTokenType.StartObject)
                return JsonSerializer.Deserialize<AccessTokenResponseResult>(ref reader, opt);
            return null;
        }

        /// <summary>Записывает данные результата</summary>
        /// <param name="writer">Приемник JSON</param>
        /// <param name="value">Данные результата</param>
        /// <param name="opt">Настройки сериализатора</param>
        public override void Write(Utf8JsonWriter writer, AccessTokenResponseResult? value, JsonSerializerOptions opt)
        {
            if (value is null) return;
            JsonSerializer.Serialize(writer, value, opt);
        }
    }
}