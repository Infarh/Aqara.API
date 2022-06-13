using System.ComponentModel;
using System.Text.Json.Serialization;

using Aqara.API.Infrastructure;

namespace Aqara.API.DTO;

public class AuthorizationCodeRequest
{
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
        if (Account is not { Length: > 0 }) throw new ArgumentException("Не указан аккаунт (телефон, либо адрес электронной почты)", nameof(Account));

        Data = new(Account, AccessTokenValidity, AccountType.ToInt());
    }

    [JsonPropertyName("intent")]
    public string Intent => Addresses.Auth.GetAuthorizationVerificationCode;

    [JsonPropertyName("data")]
    public AuthorizationCodeRequestData Data { get; }

    public class AuthorizationCodeRequestData
    {
        internal AuthorizationCodeRequestData(string Account, string AccessTokenValidity, int AccountType)
        {
            this.Account = Account;
            this.AccountType = AccountType;
            this.AccessTokenValidity = AccessTokenValidity;
        }

        [JsonPropertyName("account")]
        public string Account { get; }

        [JsonPropertyName("accountType")]
        public int AccountType { get; }

        [JsonPropertyName("AccessTokenValidity")]
        public string AccessTokenValidity { get; }

        public override string ToString() => $"account:{Account},validity:{AccessTokenValidity}";
    }

    public override string ToString() => $"intent:{Intent},data:{{{Data}}}";
}

public class AuthorizationCodeResponse : Response
{
    [JsonPropertyName("result")]
    public AuthorizationResult? Result { get; init; }

    public class AuthorizationResult
    {
        [JsonPropertyName("authCode")]
        public string? AuthorizationCode { get; init; }
    }

    public override string ToString() => $"code:{Code}({ErrorCode}),msg:{Message}";
}