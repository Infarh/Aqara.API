using System.Runtime.Serialization;
using Aqara.API.DTO;
using Aqara.API.Exceptions.Base;

namespace Aqara.API.Exceptions;

[Serializable]
public class RequestAccessTokenException : AqaraAPIRequestException
{
    public AccessTokenRequest RequestData { get; init; } = null!;

    public AccessTokenResponse? ResponseData { get; init; }

    public RequestAccessTokenException() { }
    public RequestAccessTokenException(string message) : base(message) { }
    public RequestAccessTokenException(string message, Exception inner) : base(message, inner) { }

    protected RequestAccessTokenException(
        SerializationInfo info,
        StreamingContext context) : base(info, context)
    {
    }
}