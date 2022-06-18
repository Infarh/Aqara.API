using System.Text.Json.Serialization;

namespace Aqara.API.DTO;

public class GetPositionsRequest
{
    public GetPositionsRequest() { }

    public GetPositionsRequest(string? ParentPositionId, int? PageNum, int? PageSize) =>
        Data = new(ParentPositionId, PageNum, PageSize);

    [JsonPropertyName("intent")]
    public string Intent => Addresses.Position.QueryTheSubordinatePositionInformationOfTheCurrentParentPosition;

    [JsonPropertyName("data")]
    public GetPositionsRequestData Data { get; set; } = null!;

    public class GetPositionsRequestData
    {
        public GetPositionsRequestData() { }

        public GetPositionsRequestData(string? ParentPositionId, int? PageNum, int? PageSize)
        {
            this.ParentPositionId = ParentPositionId;
            this.PageNum = PageNum;
            this.PageSize = PageSize;
        }

        [JsonPropertyName("parentPositionId")]
        public string? ParentPositionId { get; set; }

        [JsonPropertyName("pageNum")]
        public int? PageNum { get; set; }

        [JsonPropertyName("pageSize")]
        public int? PageSize { get; set; }
    }
}

public class GetPositionsResponse : Response
{
    [JsonPropertyName("result")]
    public GetPositionsResponseResult Result { get; set; } = null!;

    public class GetPositionsResponseResult
    {
        [JsonPropertyName("totalCount")]
        public int TotalCount { get; set; }

        [JsonPropertyName("data")]
        public ICollection<GetPositionsResponseResultData> Data { get; set; } = null!;

        public class GetPositionsResponseResultData
        {
            [JsonPropertyName("positionName")]
            public string Name { get; set; } = null!;

            [JsonPropertyName("positionId")]
            public string PositionId { get; set; } = null!;

            [JsonPropertyName("createTime")]
            public long CreateTime { get; set; }

            [JsonPropertyName("description")]
            public string Description { get; set; } = null!;

            [JsonPropertyName("parentPositionId")]
            public string ParentPositionId { get; set; } = null!;
        }
    }
}