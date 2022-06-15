using System.Runtime.Serialization;
using Aqara.API.DTO;
using Aqara.API.Exceptions.Base;

namespace Aqara.API.Exceptions;

[Serializable]
public class GetDevicesByPositionException : AqaraAPIRequestException
{
    public GetDevicesByPositionRequest RequestData { get; init; } = null!;

    public GetDevicesByPositionResponse? ResponseData { get; init; }

    public GetDevicesByPositionException() { }
    public GetDevicesByPositionException(string message) : base(message) { }
    public GetDevicesByPositionException(string message, Exception inner) : base(message, inner) { }

    protected GetDevicesByPositionException(
        SerializationInfo info,
        StreamingContext context) : base(info, context)
    {
    }
}