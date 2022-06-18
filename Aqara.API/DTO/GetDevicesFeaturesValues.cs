using System.Text.Json.Serialization;

namespace Aqara.API.DTO;

public class GetDevicesFeaturesValuesRequest
{
    public GetDevicesFeaturesValuesRequest() { }

    public GetDevicesFeaturesValuesRequest(params (string Device, string[] Features)[] Resources) => Data = new(Resources);

    [JsonPropertyName("intent")]
    public string Intent => Addresses.Resource.QueryDeviceAttribute;

    [JsonPropertyName("data")]
    public GetDeviceFeatureValueRequestData Data { get; set; } = null!;

    public class GetDeviceFeatureValueRequestData
    {
        public GetDeviceFeatureValueRequestData() { }

        public GetDeviceFeatureValueRequestData(IEnumerable<(string Device, string[] Features)> Resources) => this.Resources = Resources.Select(r => new GetDeviceFeatureValueRequestDataResource(r.Device, r.Features));

        [JsonPropertyName("resources")]
        public IEnumerable<GetDeviceFeatureValueRequestDataResource> Resources { get; set; } = null!;

        public class GetDeviceFeatureValueRequestDataResource
        {
            public GetDeviceFeatureValueRequestDataResource() { }

            public GetDeviceFeatureValueRequestDataResource(string DeviceId, string[] Fratures)
            {
                this.DeviceId = DeviceId;
                FeatureIds = Fratures;
            }

            [JsonPropertyName("subjectId")]
            public string DeviceId { get; set; } = null!;

            [JsonPropertyName("resourceIds")]
            public IReadOnlyCollection<string> FeatureIds { get; set; } = null!;
        }
    }
}

public class GetDevicesFeaturesValuesResponse : Response
{
    [JsonPropertyName("result")]
    public GetDeviceFeatureValueResponseResult[] Result { get; set; } = null!;

    public class GetDeviceFeatureValueResponseResult
    {
        [JsonPropertyName("subjectId")]
        public string DeviceId { get; set; } = null!;

        [JsonPropertyName("resourceId")]
        public string FeatureId { get; set; } = null!;

        [JsonPropertyName("timeStamp")]
        public long TimeStamp { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; } = null!;

        public override string ToString() => $"{DeviceId}({FeatureId})[{TimeStamp}]:{Value}";
    }
}