using System.Text.Json.Serialization;

namespace Aqara.API.DTO;

/// <summary>Запрос информации о позициях</summary>
public class GetPositionsRequest
{
    /// <summary>Создает пустой запрос</summary>
    public GetPositionsRequest() { }

    /// <summary>Создает запрос с параметрами страницы и родителя</summary>
    /// <param name="ParentPositionId">Родительская позиция</param>
    /// <param name="PageNum">Номер страницы</param>
    /// <param name="PageSize">Размер страницы</param>
    public GetPositionsRequest(string? ParentPositionId, int? PageNum, int? PageSize) =>
        Data = new(ParentPositionId, PageNum, PageSize);

    /// <summary>Идентификатор операции API</summary>
    [JsonPropertyName("intent")]
    public string Intent => Addresses.Position.QueryTheSubordinatePositionInformationOfTheCurrentParentPosition;

    /// <summary>Данные запроса</summary>
    [JsonPropertyName("data")]
    public GetPositionsRequestData Data { get; set; } = null!;

    /// <summary>Данные запроса позиций</summary>
    public class GetPositionsRequestData
    {
        /// <summary>Создает пустые данные запроса</summary>
        public GetPositionsRequestData() { }

        /// <summary>Создает данные запроса с параметрами</summary>
        /// <param name="ParentPositionId">Родительская позиция</param>
        /// <param name="PageNum">Номер страницы</param>
        /// <param name="PageSize">Размер страницы</param>
        public GetPositionsRequestData(string? ParentPositionId, int? PageNum, int? PageSize)
        {
            this.ParentPositionId = ParentPositionId;
            this.PageNum = PageNum;
            this.PageSize = PageSize;
        }

        /// <summary>Идентификатор родительской позиции</summary>
        [JsonPropertyName("parentPositionId")]
        public string? ParentPositionId { get; set; }

        /// <summary>Номер страницы</summary>
        [JsonPropertyName("pageNum")]
        public int? PageNum { get; set; }

        /// <summary>Размер страницы</summary>
        [JsonPropertyName("pageSize")]
        public int? PageSize { get; set; }
    }
}

/// <summary>Ответ на запрос позиций</summary>
public class GetPositionsResponse : Response
{
    /// <summary>Результат запроса</summary>
    [JsonPropertyName("result")]
    public GetPositionsResponseResult Result { get; set; } = null!;

    /// <summary>Данные результата запроса позиций</summary>
    public class GetPositionsResponseResult
    {
        /// <summary>Общее количество позиций</summary>
        [JsonPropertyName("totalCount")]
        public int TotalCount { get; set; }

        /// <summary>Список позиций</summary>
        [JsonPropertyName("data")]
        public ICollection<GetPositionsResponseResultData> Data { get; set; } = null!;

        /// <summary>Информация о позиции</summary>
        public class GetPositionsResponseResultData
        {
            /// <summary>Имя позиции</summary>
            [JsonPropertyName("positionName")]
            public string Name { get; set; } = null!;

            /// <summary>Идентификатор позиции</summary>
            [JsonPropertyName("positionId")]
            public string PositionId { get; set; } = null!;

            /// <summary>Время создания</summary>
            [JsonPropertyName("createTime")]
            public long CreateTime { get; set; }

            /// <summary>Описание позиции</summary>
            [JsonPropertyName("description")]
            public string Description { get; set; } = null!;

            /// <summary>Идентификатор родительской позиции</summary>
            [JsonPropertyName("parentPositionId")]
            public string ParentPositionId { get; set; } = null!;
        }
    }
}