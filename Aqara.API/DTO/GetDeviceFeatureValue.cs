using System.Text.Json.Serialization;

namespace Aqara.API.DTO;

public class GetDeviceFeatureValueRequest
{
    public GetDeviceFeatureValueRequest(params (string Device, string[] Features)[] Resources) => Data = new(Resources);

    [JsonPropertyName("intent")]
    public string Intent => Addresses.Resource.QueryDeviceAttribute;

    [JsonPropertyName("data")]
    public GetDeviceFeatureValueRequestData Data { get; init; }

    public class GetDeviceFeatureValueRequestData
    {
        internal GetDeviceFeatureValueRequestData(IEnumerable<(string Device, string[] Features)> Resources) => this.Resources = Resources.Select(r => new GetDeviceFeatureValueRequestDataResource(r.Device, r.Features));

        [JsonPropertyName("resources")]
        public IEnumerable<GetDeviceFeatureValueRequestDataResource> Resources { get; init; }

        public class GetDeviceFeatureValueRequestDataResource
        {
            internal GetDeviceFeatureValueRequestDataResource(string DeviceId, string[] Fratures)
            {
                this.DeviceId = DeviceId;
                FeatureIds = Fratures;
            }

            [JsonPropertyName("subjectId")]
            public string DeviceId { get; init; }

            [JsonPropertyName("resourceIds")]
            public IReadOnlyCollection<string> FeatureIds { get; init; }
        }
    }
}

public class GetDeviceFeatureValueResponse : Response
{
    [JsonPropertyName("result")]
    public GetDeviceFeatureValueResponseResult[] Result { get; init; } = null!;

    public class GetDeviceFeatureValueResponseResult
    {
        [JsonPropertyName("subjectId")]
        public string DeviceId { get; init; } = null!;

        [JsonPropertyName("resourceId")]
        public string FeatureId { get; init; } = null!;

        [JsonPropertyName("timeStamp")]
        public long TimeStamp { get; init; }

        [JsonPropertyName("value")]
        public string Value { get; init; } = null!;

        public override string ToString() => $"{DeviceId}({FeatureId})[{TimeStamp}]:{Value}";
    }
}