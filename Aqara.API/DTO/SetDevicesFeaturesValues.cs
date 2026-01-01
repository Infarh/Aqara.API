using System.Globalization;
using System.Text.Json.Serialization;

namespace Aqara.API.DTO;

/// <summary>Запрос управления значениями признаков устройств</summary>
public class SetDevicesFeaturesValuesRequest
{
    /// <summary>Создает пустой запрос</summary>
    public SetDevicesFeaturesValuesRequest() { }

    /// <summary>Создает запрос для указанных устройств и признаков</summary>
    /// <param name="Values">Набор устройств и значений признаков</param>
    public SetDevicesFeaturesValuesRequest((string DeviceId, (string FeatureId, double Value)[] Values)[] Values) =>
        Data = [.. Values.Select(device => new SetDevicesFeaturesValuesRequestData(device.DeviceId, device.Values))];

    /// <summary>Идентификатор операции API</summary>
    [JsonPropertyName("intent")]
    public string Intent => Addresses.Resource.ControlDevice;

    /// <summary>Данные запроса</summary>
    [JsonPropertyName("data")]
    public SetDevicesFeaturesValuesRequestData[] Data { get; set; } = null!;

    /// <summary>Данные управления признаками устройства</summary>
    public class SetDevicesFeaturesValuesRequestData
    {
        /// <summary>Создает пустые данные запроса</summary>
        public SetDevicesFeaturesValuesRequestData() { }

        /// <summary>Создает данные запроса для устройства</summary>
        /// <param name="DeviceId">Идентификатор устройства</param>
        /// <param name="Values">Набор признаков и значений</param>
        public SetDevicesFeaturesValuesRequestData(string DeviceId, (string FeatureId, double Value)[] Values)
        {
            this.DeviceId = DeviceId;
            Features = [.. Values.Select(value => new FeatureValue(value.FeatureId, value.Value.ToString(CultureInfo.InvariantCulture)))];
        }

        /// <summary>Идентификатор устройства</summary>
        [JsonPropertyName("subjectId")]
        public string DeviceId { get; set; } = null!;

        /// <summary>Признаки и значения</summary>
        [JsonPropertyName("resources")]
        public FeatureValue[] Features { get; set; } = null!;

        /// <summary>Значение признака</summary>
        public class FeatureValue
        {
            /// <summary>Создает пустое значение признака</summary>
            public FeatureValue() { }

            /// <summary>Создает значение признака с параметрами</summary>
            /// <param name="FeatureId">Идентификатор признака</param>
            /// <param name="Value">Значение признака</param>
            public FeatureValue(string FeatureId, string Value)
            {
                this.FeatureId = FeatureId;
                this.Value = Value;
            }

            /// <summary>Идентификатор признака</summary>
            [JsonPropertyName("resourceId")]
            public string FeatureId { get; set; } = null!;

            /// <summary>Значение признака</summary>
            [JsonPropertyName("value")]
            public string Value { get; set; } = null!;
        }
    }
}

/// <summary>Ответ на управление признаками устройства</summary>
public class SetDevicesFeaturesValuesResponse : Response
{
    /// <summary>Результаты выполнения команд</summary>
    [JsonPropertyName("result")]
    public SetDevicesFeaturesValuesResponseResult[] Results { get; set; } = null!;

    /// <summary>Результат выполнения команды для устройства</summary>
    public class SetDevicesFeaturesValuesResponseResult
    {
        /// <summary>Код ошибки</summary>
        [JsonPropertyName("errorCode")]
        public int ErrorCode { get; set; }

        /// <summary>Код ошибки как перечисление</summary>
        [JsonIgnore]
        public ErrorCode Error => (ErrorCode)ErrorCode;

        /// <summary>Идентификатор устройства</summary>
        [JsonPropertyName("subjectId")]
        public string DeviceId { get; set; } = null!;
    }
}
