using System.Text.Json.Serialization;

namespace Aqara.API.DTO;

[JsonSourceGenerationOptions(WriteIndented = false)]
[JsonSerializable(typeof(AuthorizationCodeRequest))]
//[JsonSerializable(typeof(AuthorizationCodeResponse))]
[JsonSerializable(typeof(GetDeviceFeatureStatisticRequest))]
//[JsonSerializable(typeof(GetDeviceFeatureStatisticResponse))]
[JsonSerializable(typeof(GetDeviceModelFeaturesRequest))]
//[JsonSerializable(typeof(GetDeviceModelFeaturesResponse))]
[JsonSerializable(typeof(GetDevicesByPositionRequest))]
//[JsonSerializable(typeof(GetDevicesByPositionResponse))]
[JsonSerializable(typeof(GetDevicesFeaturesValuesRequest))]
//[JsonSerializable(typeof(GetDevicesFeaturesValuesResponse))]
[JsonSerializable(typeof(GetPositionsRequest))]
//[JsonSerializable(typeof(GetPositionsResponse))]
[JsonSerializable(typeof(RefreshAccessTokenRequest))]
//[JsonSerializable(typeof(RefreshAccessTokenResponse))]
[JsonSerializable(typeof(SetDevicesFeaturesValuesRequest))]
//[JsonSerializable(typeof(SetDevicesFeaturesValuesResponse))]
public partial class DTOSerializerContext : JsonSerializerContext { }
