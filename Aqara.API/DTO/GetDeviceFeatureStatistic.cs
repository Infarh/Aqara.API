using System.Text.Json.Serialization;

using Aqara.API.Infrastructure;

namespace Aqara.API.DTO;

public class GetDeviceFeatureStatisticRequest
{
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
    public GetDeviceFeatureStatisticRequestData Data { get; }

    public class GetDeviceFeatureStatisticRequestData
    {
        internal GetDeviceFeatureStatisticRequestData(
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
        public string StartTime { get; }

        [JsonPropertyName("endTime")]
        public string? EndTime { get; }

        [JsonPropertyName("dimension")]
        public string Dimension { get; }

        [JsonPropertyName("size")]
        public int? Size { get; }

        [JsonPropertyName("resources")]
        public RequestResources Resources { get; }

        public class RequestResources
        {
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
            public string SubjectId { get; }

            [JsonPropertyName("aggrType")]
            public IReadOnlyCollection<int>? AggrType { get; }

            [JsonPropertyName("resourceIds")]
            public IReadOnlyCollection<string> ResourceIds { get; }
        }
    }
}

public class GetDeviceFeatureStatisticResponse : Response
{
    [JsonPropertyName("result")]
    public GetDeviceFeatureStatisticResponseResult Result { get; init; } = null!;

    public class GetDeviceFeatureStatisticResponseResult
    {
        [JsonPropertyName("data")]
        public GetDeviceFeatureStatisticResponseResultData[] Data { get; init; } = null!;

        [JsonPropertyName("scanId")]
        public string ScanId { get; init; } = null!;

        public class GetDeviceFeatureStatisticResponseResultData
        {
            [JsonPropertyName("timeStamp")]
            public long? TimeStamp { get; init; }

            [JsonPropertyName("resourceId")]
            public string ResourceId { get; init; } = null!;

            [JsonPropertyName("endTimeZone")]
            public long EndTimeZone { get; init; }

            [JsonPropertyName("value")]
            public string Value { get; init; } = null!;

            [JsonPropertyName("subjectId")]
            public string SubjectId { get; init; } = null!;

            [JsonPropertyName("aggrType")]
            public int AggrType { get; init; }

            [JsonPropertyName("startTimeZone")]
            public long StartTimeZone { get; init; }
        }
    }
}