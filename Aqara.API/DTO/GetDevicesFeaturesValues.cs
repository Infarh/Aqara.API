using System.Text.Json.Serialization;

namespace Aqara.API.DTO;

/// <summary>Запрос значений признаков устройств</summary>
public class GetDevicesFeaturesValuesRequest
{
    /// <summary>Создает пустой запрос</summary>
    public GetDevicesFeaturesValuesRequest() { }

    /// <summary>Создает запрос для указанных устройств и признаков</summary>
    /// <param name="Resources">Набор устройств и идентификаторов признаков</param>
    public GetDevicesFeaturesValuesRequest(params (string Device, string[] Features)[] Resources) => Data = new(Resources);

    /// <summary>Идентификатор операции API</summary>
    [JsonPropertyName("intent")]
    public string Intent => Addresses.Resource.QueryDeviceAttribute;

    /// <summary>Данные запроса</summary>
    [JsonPropertyName("data")]
    public GetDeviceFeatureValueRequestData Data { get; set; } = null!;

    /// <summary>Данные запроса значений признаков</summary>
    public class GetDeviceFeatureValueRequestData
    {
        /// <summary>Создает пустые данные запроса</summary>
        public GetDeviceFeatureValueRequestData() { }

        /// <summary>Создает данные запроса для ресурсов</summary>
        /// <param name="Resources">Набор устройств и признаков</param>
        public GetDeviceFeatureValueRequestData(IEnumerable<(string Device, string[] Features)> Resources) => this.Resources = Resources.Select(r => new GetDeviceFeatureValueRequestDataResource(r.Device, r.Features));

        /// <summary>Перечень ресурсов для запроса</summary>
        [JsonPropertyName("resources")]
        public IEnumerable<GetDeviceFeatureValueRequestDataResource> Resources { get; set; } = null!;

        /// <summary>Описание ресурса устройства</summary>
        public class GetDeviceFeatureValueRequestDataResource
        {
            /// <summary>Создает пустое описание ресурса</summary>
            public GetDeviceFeatureValueRequestDataResource() { }

            /// <summary>Создает описание ресурса с параметрами</summary>
            /// <param name="DeviceId">Идентификатор устройства</param>
            /// <param name="Fratures">Идентификаторы признаков</param>
            public GetDeviceFeatureValueRequestDataResource(string DeviceId, string[] Fratures)
            {
                this.DeviceId = DeviceId;
                FeatureIds = Fratures;
            }

            /// <summary>Идентификатор устройства</summary>
            [JsonPropertyName("subjectId")]
            public string DeviceId { get; set; } = null!;

            /// <summary>Идентификаторы признаков</summary>
            [JsonPropertyName("resourceIds")]
            public IReadOnlyCollection<string> FeatureIds { get; set; } = null!;
        }
    }
}

/// <summary>Ответ со значениями признаков устройств</summary>
public class GetDevicesFeaturesValuesResponse : Response
{
    /// <summary>Результаты по ресурсам</summary>
    [JsonPropertyName("result")]
    public GetDeviceFeatureValueResponseResult[] Result { get; set; } = null!;

    /// <summary>Значение признака устройства</summary>
    public class GetDeviceFeatureValueResponseResult
    {
        /// <summary>Идентификатор устройства</summary>
        [JsonPropertyName("subjectId")]
        public string DeviceId { get; set; } = null!;

        /// <summary>Идентификатор признака</summary>
        [JsonPropertyName("resourceId")]
        public string FeatureId { get; set; } = null!;

        /// <summary>Метка времени значения</summary>
        [JsonPropertyName("timeStamp")]
        public long TimeStamp { get; set; }

        /// <summary>Значение признака</summary>
        [JsonPropertyName("value")]
        public string Value { get; set; } = null!;

        /// <summary>Возвращает строковое представление значения</summary>
        /// <returns>Строка с устройством, признаком и значением</returns>
        public override string ToString() => $"{DeviceId}({FeatureId})[{TimeStamp}]:{Value}";
    }
}