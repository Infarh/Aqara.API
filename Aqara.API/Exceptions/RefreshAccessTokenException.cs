using System.Runtime.Serialization;
using Aqara.API.DTO;
using Aqara.API.Exceptions.Base;

namespace Aqara.API.Exceptions;

[Serializable]
public class RefreshAccessTokenException : AqaraAPIRequestException
{
    public RefreshAccessTokenRequest RequestData { get; init; } = null!;

    public RefreshAccessTokenResponse? ResponseData { get; init; }

    public RefreshAccessTokenException() { }
    public RefreshAccessTokenException(string message) : base(message) { }
    public RefreshAccessTokenException(string message, Exception inner) : base(message, inner) { }

    protected RefreshAccessTokenException(
        SerializationInfo info,
        StreamingContext context) : base(info, context)
    {
    }
}