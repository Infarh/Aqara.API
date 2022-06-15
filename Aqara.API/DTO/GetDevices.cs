﻿using System.Text.Json.Serialization;

namespace Aqara.API.DTO;

public class GetDevicesByPositionRequest
{
    public GetDevicesByPositionRequest(string? PositionId, int? PageNum, int? PageSize) =>
        Data = new(PositionId, PageNum, PageSize);

    [JsonPropertyName("intent")]
    public string Intent => Addresses.Device.QueryDeviceInformation;

    [JsonPropertyName("dids")]
    public List<string>? DevicesIds { get; init; } = new();

    [JsonPropertyName("data")]
    public GetDevicesRequestData Data { get; }

    public class GetDevicesRequestData
    {
        internal GetDevicesRequestData(string? PositionId, int? PageNum, int? PageSize)
        {
            this.PositionId = PositionId;
            this.PageNum = PageNum;
            this.PageSize = PageSize;
        }

        [JsonPropertyName("parentPositionId")]
        public string? PositionId { get; }

        [JsonPropertyName("pageNum")]
        public int? PageNum { get; }

        [JsonPropertyName("pageSize")]
        public int? PageSize { get; }

        public override string ToString() => $"pos:{(PositionId is { Length: > 0 } pos ? pos : "all")},page:{PageNum ?? 1},size:{PageSize ?? 30}";
    }

    public override string ToString() => $"{Intent}:{{{Data}}}";
}

public class GetDevicesByPositionResponse : Response
{
    [JsonPropertyName("result")]
    public GetDevicesResponseResult Result { get; init; } = null!;

    public class GetDevicesResponseResult
    {
        [JsonPropertyName("totalCount")]
        public int TotalCount { get; init; }

        [JsonPropertyName("data")]
        public PositionInfo[] Data { get; init; } = null!;

        public class PositionInfo
        {
            [JsonPropertyName("did")]
            public string Id { get; init; } = null!;

            [JsonPropertyName("parentDid")]
            public string ParentId { get; init; } = null!;

            [JsonPropertyName("positionId")]
            public string PositionId { get; init; } = null!;

            [JsonPropertyName("createTime")]
            public long CreateTime { get; init; }

            [JsonPropertyName("timeZone")]
            public string TimeZone { get; init; } = null!;

            [JsonPropertyName("model")]
            public string Model { get; init; } = null!;

            [JsonPropertyName("updateTime")]
            public long UpdateTime { get; init; }

            [JsonPropertyName("modelType")]
            public int ModelType { get; init; }

            [JsonPropertyName("state")]
            public int State { get; init; }

            [JsonPropertyName("firmwareVersion")]
            public string FirmwareVersion { get; init; } = null!;

            [JsonPropertyName("deviceName")]
            public string DeviceName { get; init; } = null!;

            public override string ToString() => $"{Id}:{DeviceName}(type:{Model})online:{(Model == "1")}";
        }

        public override string ToString() => $"count:{TotalCount}";
    }

    public override string ToString() => $"devices:{{{Result}}}";
}
