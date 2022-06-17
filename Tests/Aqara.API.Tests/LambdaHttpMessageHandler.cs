namespace Aqara.API.Tests;

public class LambdaHttpMessageHandler : HttpMessageHandler
{
    private readonly Func<HttpRequestMessage, HttpResponseMessage> _Processor;

    public LambdaHttpMessageHandler(Func<HttpRequestMessage, HttpResponseMessage> Processor) => _Processor = Processor;

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken Cancel) => Task.FromResult(_Processor(request));
}
