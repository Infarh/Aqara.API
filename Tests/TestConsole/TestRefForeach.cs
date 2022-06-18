using Aqara.API.TestConsole.Infrastructure;

namespace Aqara.API.TestConsole;

internal static class TestRefForeach
{
    public static ArrayRefEnumerator<T> AsRefEnumerable<T>(this T[] array) => new(array);

    public static void Invoke()
    {
        var values = new int[10];

        var i = 1;
        foreach (ref var value in values.AsRefEnumerable())
            value = i++;
    }
}

internal ref struct ArrayRefEnumerator<T>
{
    private readonly T[] _Array;

    private int _Index;

    public ArrayRefEnumerator(T[] array)
    {
        _Array = array;
        _Index = -1;
    }

    public bool MoveNext() => ++_Index < _Array.Length;

    public ref T Current => ref _Array[_Index];

    public ArrayRefEnumerator<T> GetEnumerator() => this;
}
