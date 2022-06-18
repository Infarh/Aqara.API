using System.Text.Json.Serialization;

using Aqara.API.Infrastructure;

namespace Aqara.API.DTO;

public class GetDeviceFeatureStatisticRequest
{
    public GetDeviceFeatureStatisticRequest() { }

    public GetDeviceFeatureStatisticRequest(
        string DeviceName,
        IReadOnlyCollection<int>? AggregationType,
        IReadOnlyCollection<string> Features,
        DateTime StartTime,
        DateTime? EndTime,
        string Dimension,
        int? Size)
    {
        var start_time = StartTime.ToUnixTimeTicks();
        var end_time = EndTime?.ToUnixTimeTicks();

        Data = new(DeviceName, AggregationType, Features, start_time.ToString(), end_time?.ToString(), Dimension, Size);
    }

    [JsonPropertyName("intent")]
    public string Intent => Addresses.Resource.QueryTheStatisticalHistoryValueOfTheDeviceAttribute;

    [JsonPropertyName("data")]
    public GetDeviceFeatureStatisticRequestData Data { get; set; } = null!;

    public class GetDeviceFeatureStatisticRequestData
    {
        public GetDeviceFeatureStatisticRequestData() { }

        public GetDeviceFeatureStatisticRequestData(
            string SubjectId,
            IReadOnlyCollection<int>? AggrType,
            IReadOnlyCollection<string> ResourceIds,
            string StartTime,
            string? EndTime,
            string Dimension,
            int? Size)
        {
            Resources = new(SubjectId, AggrType, ResourceIds);
            this.StartTime = StartTime;
            this.EndTime = EndTime;
            this.Dimension = Dimension;
            this.Size = Size;
        }

        [JsonPropertyName("startTime")]
        public string StartTime { get; set; } = null!;

        [JsonPropertyName("endTime")]
        public string? EndTime { get; set; }

        [JsonPropertyName("dimension")]
        public string Dimension { get; set; } = null!;

        [JsonPropertyName("size")]
        public int? Size { get; set; }

        [JsonPropertyName("resources")]
        public RequestResources Resources { get; set; } = null!;

        public class RequestResources
        {
            public RequestResources() { }

            public RequestResources(
                string SubjectId,
                IReadOnlyCollection<int>? AggrType,
                IReadOnlyCollection<string> ResourceIds)
            {
                this.SubjectId = SubjectId;
                this.AggrType = AggrType is not { Count: > 0 } ? null : AggrType;
                this.ResourceIds = ResourceIds;
            }

            [JsonPropertyName("subjectId")]
            public string SubjectId { get; set; } = null!;

            [JsonPropertyName("aggrType")]
            public IReadOnlyCollection<int>? AggrType { get; set; }

            [JsonPropertyName("resourceIds")]
            public IReadOnlyCollection<string> ResourceIds { get; set; } = null!;
        }
    }
}

public class GetDeviceFeatureStatisticResponse : Response
{
    [JsonPropertyName("result")]
    public GetDeviceFeatureStatisticResponseResult Result { get; set; } = null!;

    public class GetDeviceFeatureStatisticResponseResult
    {
        [JsonPropertyName("data")]
        public GetDeviceFeatureStatisticResponseResultData[] Data { get; set; } = null!;

        [JsonPropertyName("scanId")]
        public string ScanId { get; set; } = null!;

        public class GetDeviceFeatureStatisticResponseResultData
        {
            [JsonPropertyName("timeStamp")]
            public long? TimeStamp { get; set; }

            [JsonPropertyName("resourceId")]
            public string ResourceId { get; set; } = null!;

            [JsonPropertyName("endTimeZone")]
            public long EndTimeZone { get; set; }

            [JsonPropertyName("value")]
            public string Value { get; set; } = null!;

            [JsonPropertyName("subjectId")]
            public string SubjectId { get; set; } = null!;

            [JsonPropertyName("aggrType")]
            public int AggrType { get; set; }

            [JsonPropertyName("startTimeZone")]
            public long StartTimeZone { get; set; }
        }
    }
}