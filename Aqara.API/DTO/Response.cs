using System.Text.Json.Serialization;

namespace Aqara.API.DTO;

public abstract class Response
{
    [JsonPropertyName("code")]
    public int Code { get; init; }

    [JsonIgnore]
    public ErrorCode ErrorCode => (ErrorCode)Code;

    [JsonPropertyName("requestId")]
    public string RequestId { get; init; } = null!;

    [JsonPropertyName("message")]
    public string Message { get; init; } = null!;

    [JsonPropertyName("msgDetails")]
    public string? MessageDetails { get; init; }
}
