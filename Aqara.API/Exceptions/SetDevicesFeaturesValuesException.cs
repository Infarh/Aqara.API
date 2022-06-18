using System.Runtime.Serialization;
using Aqara.API.DTO;
using Aqara.API.Exceptions.Base;

namespace Aqara.API.Exceptions;

[Serializable]
public class SetDevicesFeaturesValuesException : AqaraAPIRequestException
{
    public SetDevicesFeaturesValuesRequest RequestData { get; init; } = null!;

    public SetDevicesFeaturesValuesResponse? ResponseData { get; init; }

    public SetDevicesFeaturesValuesException() { }
    public SetDevicesFeaturesValuesException(string message) : base(message) { }
    public SetDevicesFeaturesValuesException(string message, Exception inner) : base(message, inner) { }

    protected SetDevicesFeaturesValuesException(
        SerializationInfo info,
        StreamingContext context) : base(info, context)
    {
    }
}