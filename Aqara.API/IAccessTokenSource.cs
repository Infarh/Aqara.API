using System.Globalization;
using System.Runtime.Versioning;

using Microsoft.Win32;

using static System.Threading.Tasks.ValueTask;
using static Aqara.API.Infrastructure.ValueTaskEx;

namespace Aqara.API;

/// <summary>Абстракция источника токена доступа</summary>
public interface IAccessTokenSource
{
    /// <summary>Получить токен доступа если он доступен</summary>
    /// <param name="Cancel">Токен отмены операции</param>
    /// <returns>Информация о токене либо null если токен отсутствует</returns>
    ValueTask<AccessTokenInfo?> GetAccessToken(CancellationToken Cancel = default);

    /// <summary>Сохранить или обновить токен доступа</summary>
    /// <param name="Token">Информация о токене для сохранения</param>
    /// <param name="Cancel">Токен отмены операции</param>
    /// <returns>Сохранённая информация о токене</returns>
    ValueTask<AccessTokenInfo> SetAccessToken(AccessTokenInfo Token, CancellationToken Cancel = default);
}

/// <summary>Источник токена доступа, хранящий данные в файле</summary>
/// <param name="FilePath">Путь к файлу для хранения токена</param>
public record AccessTokenFileSource(string FilePath) : IAccessTokenSource
{
    private AccessTokenInfo? _Token;

    /// <summary>Асинхронно получить токен из файла</summary>
    /// <param name="Cancel">Токен отмены операции</param>
    /// <returns>Информация о токене либо null если файл отсутствует или расшифровка не удалась</returns>
    public async ValueTask<AccessTokenInfo?> GetAccessToken(CancellationToken Cancel = default)
    {
        if (_Token is { } token)
            return token;

        if (!File.Exists(FilePath))
            return null;

        token = await AccessTokenInfo.ReadFromFileAsync(FilePath, Cancel).ConfigureAwait(true);
        _Token = token;
        return token;
    }

    /// <summary>Асинхронно сохранить токен в файл</summary>
    /// <param name="Token">Информация о токене для сохранения</param>
    /// <param name="Cancel">Токен отмены операции</param>
    /// <returns>Сохранённая информация о токене</returns>
    public async ValueTask<AccessTokenInfo> SetAccessToken(AccessTokenInfo Token, CancellationToken Cancel = default)
    {
        if (Equals(_Token, Token))
            return Token;

        _Token = Token;
        await Token.SaveToFileAsync(FilePath, Cancel).ConfigureAwait(false);
        return Token;
    }
}

[SupportedOSPlatform("windows")]
/// <summary>Источник токена доступа, использующий реестр Windows</summary>
public class AccessTokenWinRegistrySource : IAccessTokenSource
{
    private AccessTokenInfo? _Token;

    /// <summary>Инициализировать источник реестра</summary>
    /// <exception cref="PlatformNotSupportedException">Если платформа не поддерживает Windows API</exception>
    public AccessTokenWinRegistrySource() => OperatingSystem.CheckWindows("Хранилище токенов авторизации в реестре доступно только для платформы OS Windows");

    /// <summary>Получить токен из реестра</summary>
    /// <param name="Cancel">Токен отмены операции</param>
    /// <returns>Информация о токене либо null если записи отсутствуют</returns>
    public ValueTask<AccessTokenInfo?> GetAccessToken(CancellationToken Cancel = default)
    {
        if (Cancel.IsCancellationRequested)
            return FromCanceled<AccessTokenInfo?>(Cancel);

        if (_Token is { } token)
            return FromResult<AccessTokenInfo?>(token);

        if (Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Aqara\API") is not { } registry)
            return ValueTask.Null<AccessTokenInfo?>();

        if (registry.GetValue("AccessToken") is not string { Length: > 0 } access_token) return Null<AccessTokenInfo?>();
        if (registry.GetValue("RefreshToken") is not string { Length: > 0 } refresh_token) return Null<AccessTokenInfo?>();
        if (registry.GetValue("Expires") is not int expires) return Null<AccessTokenInfo?>();
        if (registry.GetValue("OpenId") is not string { Length: > 0 } open_id) return Null<AccessTokenInfo?>();
        if (registry.GetValue("CreationTime") is not string { Length: > 0 } creation_time_str
            || !DateTime.TryParse(creation_time_str, out var creation_time)) return Null<AccessTokenInfo?>();

        token = new AccessTokenInfo(access_token, refresh_token, expires, open_id) { CreationTime = creation_time };
        _Token = token;
        return FromResult<AccessTokenInfo?>(token);
    }

    /// <summary>Сохранить токен в реестр Windows</summary>
    /// <param name="Token">Информация о токене для сохранения</param>
    /// <param name="Cancel">Токен отмены операции</param>
    /// <returns>Сохранённая информация о токене</returns>
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
