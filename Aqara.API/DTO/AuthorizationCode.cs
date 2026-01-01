using System.Text.Json.Serialization;

namespace Aqara.API.DTO;

/// <summary>Запрос получения кода авторизации</summary>
public class AuthorizationCodeRequest
{
    /// <summary>Создает пустой экземпляр запроса</summary>
    public AuthorizationCodeRequest() { }

    /// <summary>Создает запрос с указанными параметрами</summary>
    /// <param name="Account">Телефон или адрес электронной почты аккаунта</param>
    /// <param name="AccessTokenValidity">Строка с длительностью действия токена</param>
    /// <param name="AccountType">Тип аккаунта</param>
    /// <exception cref="ArgumentException">Возникает при некорректном формате интервала или отсутствии аккаунта</exception>
    /// <exception cref="ArgumentOutOfRangeException">Возникает при выходе длительности за допустимые пределы</exception>
    public AuthorizationCodeRequest(string Account, string AccessTokenValidity = "1h", AccountType AccountType = AccountType.Aqara)
    {
        var access_token_validity = AccessTokenValidity.AsSpan();
        if (!int.TryParse(access_token_validity[..^1], out var access_token_validity_time))
            throw new ArgumentException("Строка должна начинаться с числа", nameof(AccessTokenValidity));

        if (access_token_validity_time <= 0)
            throw new ArgumentException("Величина интервала времени должна быть больше 0", nameof(AccessTokenValidity));

        switch (access_token_validity[^1])
        {
            default: throw new ArgumentException("Тип временного интервала может быть лишь h - hour, d - day, y - year", nameof(AccessTokenValidity));
            case 'h':
                if (access_token_validity_time > 24)
                    throw new ArgumentOutOfRangeException(nameof(AccessTokenValidity), AccessTokenValidity, "Количество часов не должно быть больше 24");
                break;
            case 'd':
                if (access_token_validity_time > 30)
                    throw new ArgumentOutOfRangeException(nameof(AccessTokenValidity), AccessTokenValidity, "Количество дней не должно превышать 30");
                break;
            case 'y':
                if (access_token_validity_time > 10)
                    throw new ArgumentOutOfRangeException(nameof(AccessTokenValidity), AccessTokenValidity, "Число лет не должно быть больше 10");
                break;
        }
        if (Account is not { Length: > 0 })
            throw new ArgumentException("Не указан аккаунт (телефон, либо адрес электронной почты)", nameof(Account));

        Data = new(Account, AccessTokenValidity, AccountType.ToInt());
    }

    /// <summary>Идентификатор операции API</summary>
    [JsonPropertyName("intent")]
    public string Intent => Addresses.Auth.GetAuthorizationVerificationCode;

    /// <summary>Данные запроса</summary>
    [JsonPropertyName("data")]
    public AuthorizationCodeRequestData Data { get; set; } = null!;

    /// <summary>Данные запроса кода авторизации</summary>
    public class AuthorizationCodeRequestData
    {
        /// <summary>Создает пустые данные запроса</summary>
        public AuthorizationCodeRequestData() { }

        /// <summary>Создает данные запроса с параметрами</summary>
        /// <param name="Account">Аккаунт пользователя</param>
        /// <param name="AccessTokenValidity">Длительность действия токена</param>
        /// <param name="AccountType">Тип аккаунта</param>
        public AuthorizationCodeRequestData(string Account, string AccessTokenValidity, int AccountType)
        {
            this.Account = Account;
            this.AccountType = AccountType;
            this.AccessTokenValidity = AccessTokenValidity;
        }

        /// <summary>Аккаунт пользователя</summary>
        [JsonPropertyName("account")]
        public string Account { get; set; } = null!;

        /// <summary>Тип аккаунта</summary>
        [JsonPropertyName("accountType")]
        public int AccountType { get; set; }

        /// <summary>Длительность действия токена</summary>
        [JsonPropertyName("AccessTokenValidity")]
        public string AccessTokenValidity { get; set; } = null!;

        /// <summary>Возвращает строковое представление данных</summary>
        /// <returns>Строка с аккаунтом и сроком действия</returns>
        public override string ToString() => $"account:{Account},validity:{AccessTokenValidity}";
    }

    /// <summary>Возвращает строковое представление запроса</summary>
    /// <returns>Строка с intent и данными</returns>
    public override string ToString() => $"intent:{Intent},data:{{{Data}}}";
}

/// <summary>Ответ на получение кода авторизации</summary>
public class AuthorizationCodeResponse : Response
{
    /// <summary>Результат запроса</summary>
    [JsonPropertyName("result")]
    public AuthorizationResult? Result { get; set; }

    /// <summary>Данные результата авторизации</summary>
    public class AuthorizationResult
    {
        /// <summary>Код авторизации</summary>
        [JsonPropertyName("authCode")]
        public string? AuthorizationCode { get; set; }
    }

    /// <summary>Возвращает строковое представление ответа</summary>
    /// <returns>Строка с кодом и сообщением</returns>
    public override string ToString() => $"code:{Code}({ErrorCode}),msg:{Message}";
}