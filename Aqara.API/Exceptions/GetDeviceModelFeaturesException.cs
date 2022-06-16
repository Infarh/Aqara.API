using System.Runtime.Serialization;
using Aqara.API.DTO;
using Aqara.API.Exceptions.Base;

namespace Aqara.API.Exceptions;

[Serializable]
public class GetDeviceModelFeaturesException : AqaraAPIRequestException
{
    public GetDeviceModelFeaturesRequest RequestData { get; init; } = null!;

    public GetDeviceModelFeaturesResponse? ResponseData { get; init; }

    public GetDeviceModelFeaturesException() { }
    public GetDeviceModelFeaturesException(string message) : base(message) { }
    public GetDeviceModelFeaturesException(string message, Exception inner) : base(message, inner) { }

    protected GetDeviceModelFeaturesException(
        SerializationInfo info,
        StreamingContext context) : base(info, context)
    {
    }
}

[Serializable]
public class GetDeviceFeatureStatisticException : AqaraAPIRequestException
{
    public GetDeviceFeatureStatisticRequest RequestData { get; init; } = null!;

    public GetDeviceFeatureStatisticResponse? ResponseData { get; init; }

    public GetDeviceFeatureStatisticException() { }
    public GetDeviceFeatureStatisticException(string message) : base(message) { }
    public GetDeviceFeatureStatisticException(string message, Exception inner) : base(message, inner) { }

    protected GetDeviceFeatureStatisticException(
        SerializationInfo info,
        StreamingContext context) : base(info, context)
    {
    }
}
