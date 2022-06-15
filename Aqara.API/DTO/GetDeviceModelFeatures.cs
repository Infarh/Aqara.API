using System.Text.Json;
using System.Text.Json.Serialization;

namespace Aqara.API.DTO;

public class GetDeviceModelFeaturesRequest
{
    public GetDeviceModelFeaturesRequest(string Model, string? ResourceId = null) => Data = new(Model, ResourceId);

    [JsonPropertyName("intent")]
    public string Intent => Addresses.Resource.QueryTheDetailsOfTheAttributesThatHaveBeenOpened;

    [JsonPropertyName("data")]
    public GetDeviceModelFeaturesRequestData Data { get; }

    public class GetDeviceModelFeaturesRequestData
    {
        internal GetDeviceModelFeaturesRequestData(string Model, string? ResourceId)
        {
            this.Model = Model;
            this.ResourceId = ResourceId;
        }

        [JsonPropertyName("model")]
        public string Model { get; }

        [JsonPropertyName("resourceId")]
        public string? ResourceId { get; }
    }
}

public class GetDeviceModelFeaturesResponse : Response
{
    [JsonPropertyName("result")]
    public GetDeviceModelFeaturesResponseResult[] Result { get; init; } = Array.Empty<GetDeviceModelFeaturesResponseResult>();

    public class GetDeviceModelFeaturesResponseResult
    {
        [JsonPropertyName("subjectModel")]
        public string SubjectModel { get; init; } = null!;

        [JsonPropertyName("resourceId")]
        public string ResourceId { get; init; } = null!;

        [JsonPropertyName("minValue")]
        public long MinValue { get; init; }

        [JsonPropertyName("maxValue")]
        public long MaxValue { get; init; }

        /// <summary>Permissions (0-readable, 1-writable, 2-readable/writable)</summary>
        [JsonPropertyName("access")]
        public int Access { get; init; }

        [JsonPropertyName("name")]
        public string Name { get; init; } = null!;

        [JsonPropertyName("nameEn")]
        public string NameEn { get; init; } = null!;

        [JsonPropertyName("description")]
        public string Description { get; init; } = null!;

        [JsonPropertyName("descriptionEn")]
        public string DescriptionEn { get; init; } = null!;

        [JsonPropertyName("server")]
        public string Server { get; init; } = null!;

        [JsonPropertyName("enums")]
        public string Enums { get; init; } = null!;

        [JsonPropertyName("defaultValue")]
        public string DefaultValue { get; init; } = null!;

        [JsonPropertyName("unit")]
        public int? Unit { get; init; }
    }
}
