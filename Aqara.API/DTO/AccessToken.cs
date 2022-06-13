using System.ComponentModel;
using System.Text.Json;
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

        public override string ToString() => $"code:{VerificationCode},account:{Account ?? "null"}";
    }

    public override string ToString() => $"{Intent}:{{{Data}}}";
}

public class AccessTokenResponse : Response
{
    [JsonPropertyName("result")]
    [JsonConverter(typeof(EmptyStringToNullConverter))]
    public AccessTokenResponseResult? Result { get; init; }

    public class AccessTokenResponseResult
    {
        [JsonPropertyName("expiresIn")]
        public string ExpiresIn { get; init; }

        [JsonPropertyName("openId")]
        public string OpenId { get; init; }

        [JsonPropertyName("accessToken")]
        public string AccessToken { get; init; } = null!;

        [JsonPropertyName("refreshToken")]
        public string RefreshToken { get; init; } = null!;
    }

    public override string ToString() => $"code:{Code}({ErrorCode}),msg:{Message}";

    private class EmptyStringToNullConverter : JsonConverter<AccessTokenResponseResult>
    {
        public override AccessTokenResponseResult? Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions opt)
        {
            if (reader.TokenType == JsonTokenType.StartObject)
                return JsonSerializer.Deserialize<AccessTokenResponseResult>(ref reader, opt);
            return null;
        }

        public override void Write(Utf8JsonWriter writer, AccessTokenResponseResult? value, JsonSerializerOptions opt)
        {
            if (value is null) return;
            JsonSerializer.Serialize(writer, value, opt);
        }
    }
}