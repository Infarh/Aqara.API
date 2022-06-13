using System.Runtime.Serialization;

namespace Aqara.API.Exceptions.Base;

[Serializable]
public class AqaraAPIRequestException : AqaraAPIException
{
    public ErrorCode ErrorCode { get; init; } = ErrorCode.Unknown;

    public AqaraAPIRequestException() { }
    public AqaraAPIRequestException(string message) : base(message) { }
    public AqaraAPIRequestException(string message, Exception inner) : base(message, inner) { }

    protected AqaraAPIRequestException(
        SerializationInfo info,
        StreamingContext context) : base(info, context)
    {
    }
}
