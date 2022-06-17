using System.Text;

namespace Aqara.API.Infrastructure;

public readonly ref struct SignBuilder
{
    private static readonly string __NonceChars = "abcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    private static string MakeRandomString(int Length = 20)
    {
        var rnd = Random.Shared;
        var chars = new char[Length];

        var nonce_chars_length = __NonceChars.Length;
        for (var i = 0; i < Length; i++)
            chars[i] = __NonceChars[rnd.Next(nonce_chars_length)];

        return new string(chars);
    }

    public static SignBuilder Create() => new()
    {
        Time = DateTime.UtcNow.ToUnixTimeTicks(),
        Nonce = MakeRandomString(),
    };

    public string? AccessToken { get; init; }

    public string? AppId { get; init; }

    public string? KeyId { get; init; }

    public string Nonce { get; init; }

    public long Time { get; init; }

    public string? AppKey { get; init; }

    public SignBuilder AddAccessToken(string? AccessToken) => this with { AccessToken = AccessToken };
    public SignBuilder AddAppId(string AppId) => this with { AppId = AppId };
    public SignBuilder AddKeyId(string KeyId) => this with { KeyId = KeyId };
    public SignBuilder AddNonce(string Nonce) => this with { Nonce = Nonce };
    public SignBuilder MakeNonce() => this with { Nonce = MakeRandomString() };
    public SignBuilder AddCurrentTime() => AddTime(DateTime.UtcNow.ToUnixTimeTicks());
    public SignBuilder AddTime(DateTime Time) => this with { Time = Time.ToUnixTimeTicks() };
    public SignBuilder AddTime(long Time) => this with { Time = Time };
    public SignBuilder AddAppKey(string AppKey) => this with { AppKey = AppKey };

    public override string ToString()
    {
        var result = new StringBuilder();

        if (AccessToken is { Length: > 0 })
            result.Append("AccessToken=").Append(AccessToken).Append('&');

        result.Append("AppId=").Append(AppId).Append('&');
        result.Append("KeyId=").Append(KeyId).Append('&');
        result.Append("Nonce=").Append(Nonce).Append('&');
        result.Append("Time=").Append(Time);
        result.Append(AppKey);

        return result.ToString();
    }

    public string GetMD5() => ToString()
       .ToLower()
       .GetMd5String(Encoding.ASCII);
}
