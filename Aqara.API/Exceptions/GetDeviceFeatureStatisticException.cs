using System.Runtime.Serialization;
using Aqara.API.DTO;
using Aqara.API.Exceptions.Base;

namespace Aqara.API.Exceptions;

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