using System.Text.Json.Serialization;

using Aqara.API.Infrastructure;

namespace Aqara.API.DTO;

/// <summary>Запрос статистики признаков устройства</summary>
public class GetDeviceFeatureStatisticRequest
{
    /// <summary>Создает пустой запрос</summary>
    public GetDeviceFeatureStatisticRequest() { }

    /// <summary>Создает запрос с параметрами</summary>
    /// <param name="DeviceName">Идентификатор устройства</param>
    /// <param name="AggregationType">Типы агрегации</param>
    /// <param name="Features">Список признаков</param>
    /// <param name="StartTime">Начало интервала</param>
    /// <param name="EndTime">Окончание интервала</param>
    /// <param name="Dimension">Размер интервала агрегации</param>
    /// <param name="Size">Количество записей</param>
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

    /// <summary>Идентификатор операции API</summary>
    [JsonPropertyName("intent")]
    public string Intent => Addresses.Resource.QueryTheStatisticalHistoryValueOfTheDeviceAttribute;

    /// <summary>Данные запроса</summary>
    [JsonPropertyName("data")]
    public GetDeviceFeatureStatisticRequestData Data { get; set; } = null!;

    /// <summary>Данные запроса статистики</summary>
    public class GetDeviceFeatureStatisticRequestData
    {
        /// <summary>Создает пустые данные запроса</summary>
        public GetDeviceFeatureStatisticRequestData() { }

        /// <summary>Создает данные запроса с параметрами</summary>
        /// <param name="SubjectId">Идентификатор устройства</param>
        /// <param name="AggrType">Типы агрегации</param>
        /// <param name="ResourceIds">Идентификаторы признаков</param>
        /// <param name="StartTime">Начальная отметка времени</param>
        /// <param name="EndTime">Конечная отметка времени</param>
        /// <param name="Dimension">Размер интервала агрегации</param>
        /// <param name="Size">Количество записей</param>
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

        /// <summary>Начальная отметка времени</summary>
        [JsonPropertyName("startTime")]
        public string StartTime { get; set; } = null!;

        /// <summary>Конечная отметка времени</summary>
        [JsonPropertyName("endTime")]
        public string? EndTime { get; set; }

        /// <summary>Размер интервала агрегации</summary>
        [JsonPropertyName("dimension")]
        public string Dimension { get; set; } = null!;

        /// <summary>Количество записей</summary>
        [JsonPropertyName("size")]
        public int? Size { get; set; }

        /// <summary>Ресурсы для запроса</summary>
        [JsonPropertyName("resources")]
        public RequestResources Resources { get; set; } = null!;

        /// <summary>Данные по ресурсам</summary>
        public class RequestResources
        {
            /// <summary>Создает пустые данные ресурсов</summary>
            public RequestResources() { }

            /// <summary>Создает данные ресурсов с параметрами</summary>
            /// <param name="SubjectId">Идентификатор устройства</param>
            /// <param name="AggrType">Типы агрегации</param>
            /// <param name="ResourceIds">Идентификаторы признаков</param>
            public RequestResources(
                string SubjectId,
                IReadOnlyCollection<int>? AggrType,
                IReadOnlyCollection<string> ResourceIds)
            {
                this.SubjectId = SubjectId;
                this.AggrType = AggrType is not { Count: > 0 } ? null : AggrType;
                this.ResourceIds = ResourceIds;
            }

            /// <summary>Идентификатор устройства</summary>
            [JsonPropertyName("subjectId")]
            public string SubjectId { get; set; } = null!;

            /// <summary>Типы агрегации</summary>
            [JsonPropertyName("aggrType")]
            public IReadOnlyCollection<int>? AggrType { get; set; }

            /// <summary>Идентификаторы признаков</summary>
            [JsonPropertyName("resourceIds")]
            public IReadOnlyCollection<string> ResourceIds { get; set; } = null!;
        }
    }
}

/// <summary>Ответ со статистикой признаков устройства</summary>
public class GetDeviceFeatureStatisticResponse : Response
{
    /// <summary>Результат запроса</summary>
    [JsonPropertyName("result")]
    public GetDeviceFeatureStatisticResponseResult Result { get; set; } = null!;

    /// <summary>Данные результата статистики</summary>
    public class GetDeviceFeatureStatisticResponseResult
    {
        /// <summary>Записи статистики</summary>
        [JsonPropertyName("data")]
        public GetDeviceFeatureStatisticResponseResultData[] Data { get; set; } = null!;

        /// <summary>Идентификатор выборки</summary>
        [JsonPropertyName("scanId")]
        public string ScanId { get; set; } = null!;

        /// <summary>Данные статистики признака</summary>
        public class GetDeviceFeatureStatisticResponseResultData
        {
            /// <summary>Метка времени</summary>
            [JsonPropertyName("timeStamp")]
            public long? TimeStamp { get; set; }

            /// <summary>Идентификатор признака</summary>
            [JsonPropertyName("resourceId")]
            public string ResourceId { get; set; } = null!;

            /// <summary>Конечный часовой пояс</summary>
            [JsonPropertyName("endTimeZone")]
            public long EndTimeZone { get; set; }

            /// <summary>Значение признака</summary>
            [JsonPropertyName("value")]
            public string Value { get; set; } = null!;

            /// <summary>Идентификатор устройства</summary>
            [JsonPropertyName("subjectId")]
            public string SubjectId { get; set; } = null!;

            /// <summary>Тип агрегации</summary>
            [JsonPropertyName("aggrType")]
            public int AggrType { get; set; }

            /// <summary>Начальный часовой пояс</summary>
            [JsonPropertyName("startTimeZone")]
            public long StartTimeZone { get; set; }
        }
    }
}