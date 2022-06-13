using System.Runtime.Serialization;
using Aqara.API.DTO;
using Aqara.API.Exceptions.Base;

namespace Aqara.API.Exceptions;

[Serializable]
public class RequestAuthorizationCodeException : AqaraAPIRequestException
{
    public AuthorizationCodeRequest RequestData { get; init; } = null!;

    public AuthorizationCodeResponse? ResponseData { get; init; }

    public RequestAuthorizationCodeException() { }
    public RequestAuthorizationCodeException(string message) : base(message) { }
    public RequestAuthorizationCodeException(string message, Exception inner) : base(message, inner) { }

    protected RequestAuthorizationCodeException(
        SerializationInfo info,
        StreamingContext context) : base(info, context)
    {
    }
}