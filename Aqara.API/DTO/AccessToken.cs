using System.Text.Json;
using System.Text.Json.Serialization;

using Aqara.API.Infrastructure;

namespace Aqara.API.DTO;

public class AccessTokenRequest
{
    public AccessTokenRequest() { }

    public AccessTokenRequest(string VerificationCode, string? Account = null, AccountType AccountType = AccountType.Aqara) =>
        Data = new AccessTokenRequestData(VerificationCode, Account, AccountType.ToInt());

    [JsonPropertyName("intent")]
    public string Intent => Addresses.Auth.ObtainAccessToken;

    [JsonPropertyName("data")]
    public AccessTokenRequestData Data { get; set; } = null!;

    public class AccessTokenRequestData
    {
        public AccessTokenRequestData() { }

        public AccessTokenRequestData(string VerificationCode, string? Account, int AccountType)
        {
            this.VerificationCode = VerificationCode;
            this.Account = Account;
            this.AccountType = AccountType;
        }

        [JsonPropertyName("authCode")]
        public string VerificationCode { get; set; } = null!;

        [JsonPropertyName("account")]
        public string? Account { get; set; }

        [JsonPropertyName("accountType")]
        public int AccountType { get; set; }

        public override string ToString() => $"code:{VerificationCode},account:{Account ?? "null"}";
    }

    public override string ToString() => $"{Intent}:{{{Data}}}";
}

public class AccessTokenResponse : Response
{
    [JsonPropertyName("result")]
    [JsonConverter(typeof(EmptyStringToNullConverter))]
    public AccessTokenResponseResult? Result { get; set; }

    public class AccessTokenResponseResult
    {
        [JsonPropertyName("expiresIn")]
        public string ExpiresIn { get; set; } = null!;

        [JsonPropertyName("openId")]
        public string OpenId { get; set; } = null!;

        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; } = null!;

        [JsonPropertyName("refreshToken")]
        public string RefreshToken { get; set; } = null!;
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