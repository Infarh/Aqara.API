using System.Globalization;
using System.Runtime.Versioning;

using Microsoft.Win32;

namespace Aqara.API;

public interface IAccessTokenSource
{
    ValueTask<AccessTokenInfo?> GetAccessToken(CancellationToken Cancel = default);

    ValueTask<AccessTokenInfo> SetAccessToken(AccessTokenInfo Token, CancellationToken Cancel = default);
}

public class AccessTokenFileSource : IAccessTokenSource
{
    private readonly string _FilePath;

    private AccessTokenInfo? _Token;

    public AccessTokenFileSource(string FilePath) => _FilePath = FilePath;

    public async ValueTask<AccessTokenInfo?> GetAccessToken(CancellationToken Cancel = default)
    {
        if (_Token is { } token)
            return token;

        if (!File.Exists(_FilePath))
            return null;

        token = await AccessTokenInfo.ReadFromFileAsync(_FilePath, Cancel).ConfigureAwait(true);
        _Token = token;
        return token;
    }

    public async ValueTask<AccessTokenInfo> SetAccessToken(AccessTokenInfo Token, CancellationToken Cancel = default)
    {
        if (Equals(_Token, Token))
            return Token;

        _Token = Token;
        await Token.SaveToFileAsync(_FilePath, Cancel).ConfigureAwait(false);
        return Token;
    }
}

[SupportedOSPlatform("windows")]
public class AccessTokenWinRegistrySource : IAccessTokenSource
{
    private AccessTokenInfo? _Token;

    public AccessTokenWinRegistrySource()
    {
        if (!OperatingSystem.IsWindows())
            throw new PlatformNotSupportedException("Хранилище токенов авторизации в реестре доступно только для платформы OS Windows");
    }

    public ValueTask<AccessTokenInfo?> GetAccessToken(CancellationToken Cancel = default)
    {
        if (Cancel.IsCancellationRequested)
            return ValueTask.FromCanceled<AccessTokenInfo?>(Cancel);

        if (_Token is { } token)
            return ValueTask.FromResult<AccessTokenInfo?>(token);

        if (Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Aqara\API") is not { } registry)
            return ValueTask.FromResult<AccessTokenInfo?>(null);

        if (registry.GetValue("AccessToken") is not string { Length: > 0 } access_token) return ValueTask.FromResult<AccessTokenInfo?>(null);
        if (registry.GetValue("RefreshToken") is not string { Length: > 0 } refresh_token) return ValueTask.FromResult<AccessTokenInfo?>(null);
        if (registry.GetValue("Expires") is not int expires) return ValueTask.FromResult<AccessTokenInfo?>(null);
        if (registry.GetValue("OpenId") is not string { Length: > 0 } open_id) return ValueTask.FromResult<AccessTokenInfo?>(null);
        if (registry.GetValue("CreationTime") is not string { Length: > 0 } creation_time_str
            || !DateTime.TryParse(creation_time_str, out var creation_time)) return ValueTask.FromResult<AccessTokenInfo?>(null);

        token = new AccessTokenInfo(access_token, refresh_token, expires, open_id) { CreationTime = creation_time };
        _Token = token;
        return ValueTask.FromResult<AccessTokenInfo?>(token);
    }

    public ValueTask<AccessTokenInfo> SetAccessToken(AccessTokenInfo Token, CancellationToken Cancel = default)
    {
        if (Cancel.IsCancellationRequested)
            return ValueTask.FromCanceled<AccessTokenInfo>(Cancel);

        if (Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Aqara\API", true) is not { } registry)
            throw new InvalidOperationException(@"Не удалось открыть раздел реестра HKCU\SOFTWARE\Aqara\API");

        _Token = Token;

        registry.SetValue("AccessToken", Token.AccessToken, RegistryValueKind.String);
        registry.SetValue("RefreshToken", Token.RefreshToken, RegistryValueKind.String);
        registry.SetValue("Expires", Token.Expires, RegistryValueKind.DWord);
        registry.SetValue("OpenId", Token.OpenId, RegistryValueKind.String);
        registry.SetValue("CreationTime", Token.CreationTime.ToString("O", CultureInfo.InvariantCulture), RegistryValueKind.String);

        return ValueTask.FromResult(Token);
    }
}
