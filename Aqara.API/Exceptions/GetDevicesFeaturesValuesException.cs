using System.Runtime.Serialization;
using Aqara.API.DTO;
using Aqara.API.Exceptions.Base;

namespace Aqara.API.Exceptions;

[Serializable]
public class GetDevicesFeaturesValuesException : AqaraAPIRequestException
{
    public GetDevicesFeaturesValuesRequest RequestData { get; init; } = null!;

    public GetDevicesFeaturesValuesResponse? ResponseData { get; init; }

    public GetDevicesFeaturesValuesException() { }
    public GetDevicesFeaturesValuesException(string message) : base(message) { }
    public GetDevicesFeaturesValuesException(string message, Exception inner) : base(message, inner) { }

    protected GetDevicesFeaturesValuesException(
        SerializationInfo info,
        StreamingContext context) : base(info, context)
    {
    }
}