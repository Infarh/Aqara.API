using System.Text.Json;
using System.Text.Json.Serialization;

namespace Aqara.API.DTO;

public class GetDeviceModelFeaturesRequest
{
    public GetDeviceModelFeaturesRequest() { }

    public GetDeviceModelFeaturesRequest(string Model, string? ResourceId = null) => Data = new(Model, ResourceId);

    [JsonPropertyName("intent")]
    public string Intent => Addresses.Resource.QueryTheDetailsOfTheAttributesThatHaveBeenOpened;

    [JsonPropertyName("data")]
    public GetDeviceModelFeaturesRequestData Data { get; set; } = null!;

    public class GetDeviceModelFeaturesRequestData
    {
        public GetDeviceModelFeaturesRequestData() { }

        public GetDeviceModelFeaturesRequestData(string Model, string? ResourceId)
        {
            this.Model = Model;
            this.ResourceId = ResourceId;
        }

        [JsonPropertyName("model")]
        public string Model { get; set; } = null!;

        [JsonPropertyName("resourceId")]
        public string? ResourceId { get; set; }
    }
}

public class GetDeviceModelFeaturesResponse : Response
{
    [JsonPropertyName("result")]
    public GetDeviceModelFeaturesResponseResult[] Result { get; set; } = Array.Empty<GetDeviceModelFeaturesResponseResult>();

    public class GetDeviceModelFeaturesResponseResult
    {
        [JsonPropertyName("subjectModel")]
        public string SubjectModel { get; set; } = null!;

        [JsonPropertyName("resourceId")]
        public string ResourceId { get; set; } = null!;

        [JsonPropertyName("minValue")]
        public long MinValue { get; set; }

        [JsonPropertyName("maxValue")]
        public long MaxValue { get; set; }

        /// <summary>Permissions (0-readable, 1-writable, 2-readable/writable)</summary>
        [JsonPropertyName("access")]
        public int Access { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        [JsonPropertyName("nameEn")]
        public string NameEn { get; set; } = null!;

        [JsonPropertyName("description")]
        public string Description { get; set; } = null!;

        [JsonPropertyName("descriptionEn")]
        public string DescriptionEn { get; set; } = null!;

        [JsonPropertyName("server")]
        public string Server { get; set; } = null!;

        [JsonPropertyName("enums")]
        public string Enums { get; set; } = null!;

        [JsonPropertyName("defaultValue")]
        public string DefaultValue { get; set; } = null!;

        [JsonPropertyName("unit")]
        public int? Unit { get; set; }
    }
}
