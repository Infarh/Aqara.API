using System.Text.Json.Serialization;

namespace Aqara.API.DTO;

/// <summary>Запрос списка устройств по позиции</summary>
public class GetDevicesByPositionRequest
{
    /// <summary>Создает пустой запрос</summary>
    public GetDevicesByPositionRequest() { }

    /// <summary>Создает запрос с параметрами</summary>
    /// <param name="PositionId">Идентификатор позиции</param>
    /// <param name="PageNum">Номер страницы</param>
    /// <param name="PageSize">Размер страницы</param>
    public GetDevicesByPositionRequest(string? PositionId, int? PageNum, int? PageSize) =>
        Data = new(PositionId, PageNum, PageSize);

    /// <summary>Идентификатор операции API</summary>
    [JsonPropertyName("intent")]
    public string Intent => Addresses.Device.QueryDeviceInformation;

    /// <summary>Идентификаторы устройств</summary>
    [JsonPropertyName("dids")]
    public List<string>? DevicesIds { get; set; } = [];

    /// <summary>Данные запроса</summary>
    [JsonPropertyName("data")]
    public GetDevicesRequestData Data { get; set; } = null!;

    /// <summary>Данные запроса устройств по позиции</summary>
    public class GetDevicesRequestData
    {
        /// <summary>Создает пустые данные запроса</summary>
        public GetDevicesRequestData() { }

        /// <summary>Создает данные запроса с параметрами</summary>
        /// <param name="PositionId">Идентификатор позиции</param>
        /// <param name="PageNum">Номер страницы</param>
        /// <param name="PageSize">Размер страницы</param>
        public GetDevicesRequestData(string? PositionId, int? PageNum, int? PageSize)
        {
            this.PositionId = PositionId;
            this.PageNum = PageNum;
            this.PageSize = PageSize;
        }

        /// <summary>Идентификатор родительской позиции</summary>
        [JsonPropertyName("parentPositionId")]
        public string? PositionId { get; set; }

        /// <summary>Номер страницы</summary>
        [JsonPropertyName("pageNum")]
        public int? PageNum { get; set; }

        /// <summary>Размер страницы</summary>
        [JsonPropertyName("pageSize")]
        public int? PageSize { get; set; }

        /// <summary>Возвращает строковое представление данных</summary>
        /// <returns>Строка с позицией и параметрами страницы</returns>
        public override string ToString() => $"pos:{(PositionId is { Length: > 0 } pos ? pos : "all")},page:{PageNum ?? 1},size:{PageSize ?? 30}";
    }

    /// <summary>Возвращает строковое представление запроса</summary>
    /// <returns>Строка с intent и данными</returns>
    public override string ToString() => $"{Intent}:{{{Data}}}";
}

/// <summary>Ответ на запрос устройств по позиции</summary>
public class GetDevicesByPositionResponse : Response
{
    /// <summary>Результат запроса</summary>
    [JsonPropertyName("result")]
    public GetDevicesResponseResult Result { get; set; } = null!;

    /// <summary>Данные результата запроса устройств</summary>
    public class GetDevicesResponseResult
    {
        /// <summary>Общее количество устройств</summary>
        [JsonPropertyName("totalCount")]
        public int TotalCount { get; set; }

        /// <summary>Список устройств</summary>
        [JsonPropertyName("data")]
        public PositionInfo[] Data { get; set; } = null!;

        /// <summary>Информация об устройстве и позиции</summary>
        public class PositionInfo
        {
            /// <summary>Идентификатор устройства</summary>
            [JsonPropertyName("did")]
            public string Id { get; set; } = null!;

            /// <summary>Идентификатор родителя</summary>
            [JsonPropertyName("parentDid")]
            public string ParentId { get; set; } = null!;

            /// <summary>Идентификатор позиции</summary>
            [JsonPropertyName("positionId")]
            public string PositionId { get; set; } = null!;

            /// <summary>Время создания</summary>
            [JsonPropertyName("createTime")]
            public long CreateTime { get; set; }

            /// <summary>Часовой пояс устройства</summary>
            [JsonPropertyName("timeZone")]
            public string TimeZone { get; set; } = null!;

            /// <summary>Модель устройства</summary>
            [JsonPropertyName("model")]
            public string Model { get; set; } = null!;

            /// <summary>Время обновления</summary>
            [JsonPropertyName("updateTime")]
            public long UpdateTime { get; set; }

            /// <summary>Тип модели</summary>
            [JsonPropertyName("modelType")]
            public int ModelType { get; set; }

            /// <summary>Состояние устройства</summary>
            [JsonPropertyName("state")]
            public int State { get; set; }

            /// <summary>Версия прошивки</summary>
            [JsonPropertyName("firmwareVersion")]
            public string FirmwareVersion { get; set; } = null!;

            /// <summary>Имя устройства</summary>
            [JsonPropertyName("deviceName")]
            public string DeviceName { get; set; } = null!;

            /// <summary>Возвращает строковое представление устройства</summary>
            /// <returns>Строка с идентификатором и моделью</returns>
            public override string ToString() => $"{Id}:{DeviceName}(type:{Model})online:{(Model == "1")}";
        }

        /// <summary>Возвращает строковое представление результата</summary>
        /// <returns>Строка с количеством устройств</returns>
        public override string ToString() => $"count:{TotalCount}";
    }

    /// <summary>Возвращает строковое представление ответа</summary>
    /// <returns>Строка с результатом</returns>
    public override string ToString() => $"devices:{{{Result}}}";
}
