using System.Text.Json.Serialization;

namespace Aqara.API.DTO;

public class GetDevicesByPositionRequest
{
    public GetDevicesByPositionRequest() { }

    public GetDevicesByPositionRequest(string? PositionId, int? PageNum, int? PageSize) =>
        Data = new(PositionId, PageNum, PageSize);

    [JsonPropertyName("intent")]
    public string Intent => Addresses.Device.QueryDeviceInformation;

    [JsonPropertyName("dids")]
    public List<string>? DevicesIds { get; set; } = new();

    [JsonPropertyName("data")]
    public GetDevicesRequestData Data { get; set; } = null!;

    public class GetDevicesRequestData
    {
        public GetDevicesRequestData() { }

        public GetDevicesRequestData(string? PositionId, int? PageNum, int? PageSize)
        {
            this.PositionId = PositionId;
            this.PageNum = PageNum;
            this.PageSize = PageSize;
        }

        [JsonPropertyName("parentPositionId")]
        public string? PositionId { get; set; }

        [JsonPropertyName("pageNum")]
        public int? PageNum { get; set; }

        [JsonPropertyName("pageSize")]
        public int? PageSize { get; set; }

        public override string ToString() => $"pos:{(PositionId is { Length: > 0 } pos ? pos : "all")},page:{PageNum ?? 1},size:{PageSize ?? 30}";
    }

    public override string ToString() => $"{Intent}:{{{Data}}}";
}

public class GetDevicesByPositionResponse : Response
{
    [JsonPropertyName("result")]
    public GetDevicesResponseResult Result { get; set; } = null!;

    public class GetDevicesResponseResult
    {
        [JsonPropertyName("totalCount")]
        public int TotalCount { get; set; }

        [JsonPropertyName("data")]
        public PositionInfo[] Data { get; set; } = null!;

        public class PositionInfo
        {
            [JsonPropertyName("did")]
            public string Id { get; set; } = null!;

            [JsonPropertyName("parentDid")]
            public string ParentId { get; set; } = null!;

            [JsonPropertyName("positionId")]
            public string PositionId { get; set; } = null!;

            [JsonPropertyName("createTime")]
            public long CreateTime { get; set; }

            [JsonPropertyName("timeZone")]
            public string TimeZone { get; set; } = null!;

            [JsonPropertyName("model")]
            public string Model { get; set; } = null!;

            [JsonPropertyName("updateTime")]
            public long UpdateTime { get; set; }

            [JsonPropertyName("modelType")]
            public int ModelType { get; set; }

            [JsonPropertyName("state")]
            public int State { get; set; }

            [JsonPropertyName("firmwareVersion")]
            public string FirmwareVersion { get; set; } = null!;

            [JsonPropertyName("deviceName")]
            public string DeviceName { get; set; } = null!;

            public override string ToString() => $"{Id}:{DeviceName}(type:{Model})online:{(Model == "1")}";
        }

        public override string ToString() => $"count:{TotalCount}";
    }

    public override string ToString() => $"devices:{{{Result}}}";
}
