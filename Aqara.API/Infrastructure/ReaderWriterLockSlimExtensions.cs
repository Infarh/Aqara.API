namespace Aqara.API.Infrastructure;

internal static class ReaderWriterLockSlimExtensions
{
    public readonly ref struct ReadLock
    {
        private readonly ReaderWriterLockSlim _Lock;

        public ReadLock(ReaderWriterLockSlim Lock)
        {
            Lock.EnterReadLock();
            _Lock = Lock;
        }

        public void Dispose() => _Lock.ExitReadLock();
    }
    private class ReadLockObj : IDisposable
    {
        private readonly ReaderWriterLockSlim _Lock;

        public ReadLockObj(ReaderWriterLockSlim Lock)
        {
            Lock.EnterReadLock();
            _Lock = Lock;
        }

        public void Dispose() => _Lock.ExitReadLock();
    }

    public static ReadLock LockRead(this ReaderWriterLockSlim Lock) => new(Lock);

    public static IDisposable LockReadRef(this ReaderWriterLockSlim Lock) => new ReadLockObj(Lock);

    public readonly ref struct WriteLock
    {
        private readonly ReaderWriterLockSlim _Lock;

        public WriteLock(ReaderWriterLockSlim Lock)
        {
            Lock.EnterWriteLock();
            _Lock = Lock;
        }

        public void Dispose() => _Lock.ExitWriteLock();
    }

    private class WriteLockRef : IDisposable
    {
        private readonly ReaderWriterLockSlim _Lock;

        public WriteLockRef(ReaderWriterLockSlim Lock)
        {
            Lock.EnterWriteLock();
            _Lock = Lock;
        }

        public void Dispose() => _Lock.ExitWriteLock();
    }

    public static WriteLock LockWrite(this ReaderWriterLockSlim Lock) => new(Lock);

    public static IDisposable LockWriteRef(this ReaderWriterLockSlim Lock) => new WriteLockRef(Lock);
}
