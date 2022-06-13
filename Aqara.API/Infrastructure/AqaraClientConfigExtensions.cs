using System.Net.Http.Headers;

namespace Aqara.API.Infrastructure;

internal static class HttpRequestHeadersExtensions
{
    public static void Replace(this HttpRequestHeaders headers, string name, string value)
    {
        headers.Remove(name);
        headers.Add(name, value);
    }

    public static string AddOrReplace(this HttpRequestHeaders headers, string name, string value)
    {
        if (!headers.Contains(name))
        {
            headers.Add(name, value);
            return value;
        }

        if (headers.GetValues(name).Single() != value)
            headers.Replace(name, value);

        return value;
    }

    public static void AddIfNotExist(this HttpRequestHeaders headers, string name, string value)
    {
        if (headers.Contains(name)) return;
        headers.Add(name, value);
    }

}

internal static class AqaraClientConfigExtensions
{
    public static HttpClient AddHeaders(this HttpClient client, AqaraClientConfig Configuration)
    {
        var headers = client.DefaultRequestHeaders;

        const string header_app_id = nameof(Configuration.AppId);
        const string header_key_id = nameof(Configuration.KeyId);

        const string header_nonce = "Nonce";
        const string header_time = "Time";
        const string header_sign = "Sign";
        const string header_lang = "Lang";

        var app_id = headers.AddOrReplace(header_app_id, Configuration.AppId);
        var key_id = headers.AddOrReplace(header_key_id, Configuration.KeyId);

        headers.AddIfNotExist(header_lang, "en");

        var sign = SignBuilder.Create()
           .AddAppId(app_id)
           .AddKeyId(key_id)
           .AddAppKey(Configuration.AppKey);

        headers.Replace(header_nonce, sign.Nonce);
        headers.Replace(header_time, sign.Time.ToString());
        headers.Replace(header_sign, sign.GetMD5());

        return client;
    }
}

internal static class AccessTokenInfoExtensions
{
    public static HttpClient AddAccessToken(this HttpClient client, AccessTokenInfo? TokenInfo)
    {
        if (TokenInfo is not { AccessToken: { Length: > 0 } access_token }) return client;

        const string header_access_token = "AccessToken";
        client.DefaultRequestHeaders.AddOrReplace(header_access_token, access_token);

        return client;
    }
}