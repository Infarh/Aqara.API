namespace Aqara.API.Exceptions.Base;

[Serializable]
public class AqaraAPIException : Exception
{
    public AqaraAPIException() { }
    public AqaraAPIException(string message) : base(message) { }
    public AqaraAPIException(string message, Exception inner) : base(message, inner) { }
}