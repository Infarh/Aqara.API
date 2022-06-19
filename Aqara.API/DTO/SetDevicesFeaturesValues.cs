using System.Globalization;
using System.Text.Json.Serialization;

namespace Aqara.API.DTO;

public class SetDevicesFeaturesValuesRequest
{
    public SetDevicesFeaturesValuesRequest() { }

    public SetDevicesFeaturesValuesRequest((string DeviceId, (string FeatureId, double Value)[] Values)[] Values) =>
        Data = Values.Select(device => new SetDevicesFeaturesValuesRequestData(device.DeviceId, device.Values)).ToArray();

    [JsonPropertyName("intent")]
    public string Intent => Addresses.Resource.ControlDevice;

    [JsonPropertyName("data")]
    public SetDevicesFeaturesValuesRequestData[] Data { get; set; } = null!;

    public class SetDevicesFeaturesValuesRequestData
    {
        public SetDevicesFeaturesValuesRequestData() { }

        public SetDevicesFeaturesValuesRequestData(string DeviceId, (string FeatureId, double Value)[] Values)
        {
            this.DeviceId = DeviceId;
            Features = Values.Select(value => new FeatureValue(value.FeatureId, value.Value.ToString(CultureInfo.InvariantCulture))).ToArray();
        }

        [JsonPropertyName("subjectId")]
        public string DeviceId { get; set; } = null!;

        [JsonPropertyName("resources")]
        public FeatureValue[] Features { get; set; } = null!;

        public class FeatureValue
        {
            public FeatureValue() { }

            public FeatureValue(string FeatureId, string Value)
            {
                this.FeatureId = FeatureId;
                this.Value = Value;
            }

            [JsonPropertyName("resourceId")]
            public string FeatureId { get; set; } = null!;

            [JsonPropertyName("value")]
            public string Value { get; set; } = null!;
        }
    }
}

public class SetDevicesFeaturesValuesResponse : Response
{
    [JsonPropertyName("result")]
    public SetDevicesFeaturesValuesResponseResult[] Results { get; set; } = null!;

    public class SetDevicesFeaturesValuesResponseResult
    {
        [JsonPropertyName("errorCode")]
        public int ErrorCode { get; set; }

        [JsonIgnore]
        public ErrorCode Error => (ErrorCode)ErrorCode;

        [JsonPropertyName("subjectId")]
        public string DeviceId { get; set; } = null!;
    }
}
