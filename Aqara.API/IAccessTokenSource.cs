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
