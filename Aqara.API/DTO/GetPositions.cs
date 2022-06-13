using System.Text.Json.Serialization;

namespace Aqara.API.DTO;

public class GetPositionsRequest
{
    public GetPositionsRequest(string? ParentPositionId, int? PageNum, int? PageSize) =>
        Data = new(ParentPositionId, PageNum, PageSize);

    [JsonPropertyName("intent")]
    public string Intent => Addresses.Position.QueryTheSubordinatePositionInformationOfTheCurrentParentPosition;

    [JsonPropertyName("data")]
    public GetPositionsRequestData Data { get; }

    public class GetPositionsRequestData
    {
        internal GetPositionsRequestData(string? ParentPositionId, int? PageNum, int? PageSize)
        {
            this.ParentPositionId = ParentPositionId;
            this.PageNum = PageNum;
            this.PageSize = PageSize;
        }

        [JsonPropertyName("parentPositionId")]
        public string? ParentPositionId { get; }

        [JsonPropertyName("pageNum")]
        public int? PageNum { get; }

        [JsonPropertyName("pageSize")]
        public int? PageSize { get; }
    }
}

public class GetPositionsResponse : Response
{
    [JsonPropertyName("result")]
    public GetPositionsResponseResult Result { get; init; } = null!;

    public class GetPositionsResponseResult
    {
        [JsonPropertyName("totalCount")]
        public int TotalCount { get; init; }

        [JsonPropertyName("data")]
        public PositionInfo[] Data { get; init; }

        public class PositionInfo
        {
            [JsonPropertyName("positionName")]
            public string Name { get; init; } = null!;

            [JsonPropertyName("positionId")]
            public string PositionId { get; init; } = null!;

            [JsonPropertyName("createTime")]
            public long CreateTime { get; init; }

            [JsonPropertyName("description")]
            public string Description { get; init; } = null!;

            [JsonPropertyName("parentPositionId")]
            public string ParentPositionId { get; init; } = null!;
        }
    }
}