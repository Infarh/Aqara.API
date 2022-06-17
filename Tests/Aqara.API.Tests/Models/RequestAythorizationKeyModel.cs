namespace Aqara.API.Tests.Models;

public class AuthorizationCodeRequestModel
{
    public string intent { get; init; } = null!;

    public AuthorizationCodeRequestData data { get; init; } = null!;

    public class AuthorizationCodeRequestData
    {
        public string account { get; init; } = null!;

        public int? accountType { get; init; }

        public string accessTokenValidity { get; init; } = null!;

        public override string ToString() => $"account:{account},validity:{accessTokenValidity}";
    }

    public override string ToString() => $"intent:{intent},data:{{{data}}}";
}
