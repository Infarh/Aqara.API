using System.Text.Json.Serialization;

namespace Aqara.API.DTO;

public abstract class Response
{
    [JsonPropertyName("code")]
    public int Code { get; set; }

    [JsonIgnore]
    public ErrorCode ErrorCode => (ErrorCode)Code;

    [JsonPropertyName("requestId")]
    public string RequestId { get; set; } = null!;

    [JsonPropertyName("message")]
    public string Message { get; set; } = null!;

    [JsonPropertyName("msgDetails")]
    //[JsonPropertyName("messageDetail")]
    public string? MessageDetails { get; set; }

    [JsonPropertyName("messageDetail")]
    public string? OtherMessages { get; set; }
}
