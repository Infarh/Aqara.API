using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Aqara.API;

public class AqaraClientConfig
{
    public string AppId { get; set; } = null!;

    public string AppKey { get; set; } = null!;

    public string KeyId { get; set; } = null!;

    public string TokenStorageFile { get; init; } = null!;

    [JsonExtensionData]
    private IDictionary<string, JsonElement> Values { get; set; }
}