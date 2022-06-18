﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Aqara.API;

public class AqaraClientConfig
{
    [Required, StringLength(24, MinimumLength = 24)]
    public string AppId { get; init; } = null!;

    [Required, StringLength(32, MinimumLength = 32)]
    public string AppKey { get; init; } = null!;

    [Required, StringLength(20, MinimumLength = 20)]
    public string KeyId { get; init; } = null!;

    [Required]
    public string TokenStorageFile { get; init; } = null!;

    [JsonExtensionData]
    private IDictionary<string, JsonElement> Values { get; set; } = null!;
}