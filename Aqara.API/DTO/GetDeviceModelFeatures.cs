using System.Text.Json.Serialization;

namespace Aqara.API.DTO;

/// <summary>Запрос списка признаков модели устройства</summary>
public class GetDeviceModelFeaturesRequest
{
    /// <summary>Создает пустой запрос</summary>
    public GetDeviceModelFeaturesRequest() { }

    /// <summary>Создает запрос с параметрами</summary>
    /// <param name="Model">Модель устройства</param>
    /// <param name="ResourceId">Идентификатор признака</param>
    public GetDeviceModelFeaturesRequest(string Model, string? ResourceId = null) => Data = new(Model, ResourceId);

    /// <summary>Идентификатор операции API</summary>
    [JsonPropertyName("intent")]
    public string Intent => Addresses.Resource.QueryTheDetailsOfTheAttributesThatHaveBeenOpened;

    /// <summary>Данные запроса</summary>
    [JsonPropertyName("data")]
    public GetDeviceModelFeaturesRequestData Data { get; set; } = null!;

    /// <summary>Данные запроса признаков модели</summary>
    public class GetDeviceModelFeaturesRequestData
    {
        /// <summary>Создает пустые данные запроса</summary>
        public GetDeviceModelFeaturesRequestData() { }

        /// <summary>Создает данные запроса с параметрами</summary>
        /// <param name="Model">Модель устройства</param>
        /// <param name="ResourceId">Идентификатор признака</param>
        public GetDeviceModelFeaturesRequestData(string Model, string? ResourceId)
        {
            this.Model = Model;
            this.ResourceId = ResourceId;
        }

        /// <summary>Модель устройства</summary>
        [JsonPropertyName("model")]
        public string Model { get; set; } = null!;

        /// <summary>Идентификатор признака</summary>
        [JsonPropertyName("resourceId")]
        public string? ResourceId { get; set; }
    }
}

/// <summary>Ответ со списком признаков модели</summary>
public class GetDeviceModelFeaturesResponse : Response
{
    /// <summary>Перечень признаков</summary>
    [JsonPropertyName("result")]
    public GetDeviceModelFeaturesResponseResult[] Result { get; set; } = Array.Empty<GetDeviceModelFeaturesResponseResult>();

    /// <summary>Описание признака модели устройства</summary>
    public class GetDeviceModelFeaturesResponseResult
    {
        /// <summary>Модель устройства</summary>
        [JsonPropertyName("model")]
        public string SubjectModel { get; set; } = null!; // соответствует полю "model" от сервера

        /// <summary>Идентификатор признака</summary>
        [JsonPropertyName("resourceId")]
        public string ResourceId { get; set; } = null!;

        /// <summary>Минимальное значение признака</summary>
        [JsonPropertyName("minValue")]
        public long? MinValue { get; set; } // может быть null в ответе сервера

        /// <summary>Максимальное значение признака</summary>
        [JsonPropertyName("maxValue")]
        public long? MaxValue { get; set; } // может быть null в ответе сервера

        /// <summary>Права доступа (0-чтение, 1-запись, 2-чтение/запись)</summary>
        [JsonPropertyName("access")]
        public int Access { get; set; }

        /// <summary>Название признака</summary>
        [JsonPropertyName("name")]
        public string Name { get; set; } = null!;

        /// <summary>Название признака на английском</summary>
        [JsonPropertyName("nameEn")]
        public string NameEn { get; set; } = null!;

        /// <summary>Описание признака</summary>
        [JsonPropertyName("description")]
        public string Description { get; set; } = null!;

        /// <summary>Описание признака на английском</summary>
        [JsonPropertyName("descriptionEn")]
        public string DescriptionEn { get; set; } = null!;

        /// <summary>Имя сервера</summary>
        [JsonPropertyName("server")]
        public string Server { get; set; } = null!;

        /// <summary>Перечень значений перечисления</summary>
        [JsonPropertyName("enums")]
        public string Enums { get; set; } = null!;

        /// <summary>Значение по умолчанию</summary>
        [JsonPropertyName("defaultValue")]
        public string DefaultValue { get; set; } = null!;

        /// <summary>Единица измерения</summary>
        [JsonPropertyName("unit")]
        public int? Unit { get; set; }
    }
}
