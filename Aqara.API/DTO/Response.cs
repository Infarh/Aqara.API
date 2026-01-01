using System.Text.Json.Serialization;

namespace Aqara.API.DTO;

/// <summary>Базовый тип ответа API Aqara</summary>
public abstract class Response
{
    /// <summary>Код результата</summary>
    [JsonPropertyName("code")]
    public int Code { get; set; }

    /// <summary>Код результата как перечисление</summary>
    [JsonIgnore]
    public ErrorCode ErrorCode => (ErrorCode)Code;

    /// <summary>Идентификатор запроса</summary>
    [JsonPropertyName("requestId")]
    public string RequestId { get; set; } = null!;

    /// <summary>Сообщение об ошибке или результате</summary>
    [JsonPropertyName("message")]
    public string Message { get; set; } = null!;

    /// <summary>Подробности сообщения</summary>
    [JsonPropertyName("msgDetails")]
    //[JsonPropertyName("messageDetail")]
    public string? MessageDetails { get; set; }

    /// <summary>Дополнительные сообщения</summary>
    [JsonPropertyName("messageDetail")]
    public string? OtherMessages { get; set; }
}
