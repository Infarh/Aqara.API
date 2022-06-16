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
    private readonly ReaderWriterLockSlim _Lock = new();

    public AccessTokenFileSource(string FilePath) => _FilePath = FilePath;

    public async ValueTask<AccessTokenInfo?> GetAccessToken(CancellationToken Cancel = default)
    {
        _Lock.EnterReadLock();
        try
        {
            if (_Token is { } token)
                return token;

            if (!File.Exists(_FilePath))
                return null;

            token = await AccessTokenInfo.ReadFromFileAsync(_FilePath, Cancel).ConfigureAwait(false);
            _Token = token;
            return token;
        }
        finally
        {
            _Lock.ExitReadLock();
        }
    }

    public async ValueTask<AccessTokenInfo> SetAccessToken(AccessTokenInfo Token, CancellationToken Cancel = default)
    {
        _Lock.EnterWriteLock();
        try
        {
            if(Equals(_Token, Token)) return Token;
            _Token = Token;
            await Token.SaveToFileAsync(_FilePath, Cancel).ConfigureAwait(false);
            return Token;
        }
        finally
        {
            _Lock.ExitWriteLock();
        }
    }
}
