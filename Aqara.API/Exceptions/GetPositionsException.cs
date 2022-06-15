using System.Runtime.Serialization;
using Aqara.API.DTO;
using Aqara.API.Exceptions.Base;

namespace Aqara.API.Exceptions;

[Serializable]
public class GetPositionsException : AqaraAPIRequestException
{
    public GetPositionsRequest RequestData { get; init; } = null!;

    public GetPositionsResponse? ResponseData { get; init; }

    public GetPositionsException() { }
    public GetPositionsException(string message) : base(message) { }
    public GetPositionsException(string message, Exception inner) : base(message, inner) { }

    protected GetPositionsException(
        SerializationInfo info,
        StreamingContext context) : base(info, context)
    {
    }
}