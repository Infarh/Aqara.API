using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Aqara.API;

/// <summary>Конфигурация клиента API Aqara сервиса</summary>
public class AqaraClientConfig
{
    /// <summary>Идентификатор приложения</summary>
    [Required, StringLength(24, MinimumLength = 24)]
    public string AppId { get; init; } = null!;

    /// <summary>Ключ приложения</summary>
    [Required, StringLength(32, MinimumLength = 32)]
    public string AppKey { get; init; } = null!;

    /// <summary>Идентификатор ключа</summary>
    [Required, StringLength(20, MinimumLength = 20)]
    public string KeyId { get; init; } = null!;

    /// <summary>Дополнительные параметры</summary>
    [JsonExtensionData]
    public IDictionary<string, JsonElement> Values { get; set; } = null!;
}