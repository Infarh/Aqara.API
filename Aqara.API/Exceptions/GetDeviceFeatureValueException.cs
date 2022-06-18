using System.Runtime.Serialization;
using Aqara.API.DTO;
using Aqara.API.Exceptions.Base;

namespace Aqara.API.Exceptions;

[Serializable]
public class GetDeviceFeatureValueException : AqaraAPIRequestException
{
    public GetDeviceFeatureValueRequest RequestData { get; init; } = null!;

    public GetDeviceFeatureValueResponse? ResponseData { get; init; }

    public GetDeviceFeatureValueException() { }
    public GetDeviceFeatureValueException(string message) : base(message) { }
    public GetDeviceFeatureValueException(string message, Exception inner) : base(message, inner) { }

    protected GetDeviceFeatureValueException(
        SerializationInfo info,
        StreamingContext context) : base(info, context)
    {
    }
}