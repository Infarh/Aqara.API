using System.Net.Http.Headers;

namespace Aqara.API.Tests;

public class TestHttpClientBuilder
{
    public delegate Task<HttpResponseMessage> ProcessRequestAsync(HttpRequestMessage request, CancellationToken cancel);
    public delegate HttpResponseMessage ProcessRequest(HttpRequestMessage request);

    private readonly ProcessRequestAsync? _RequestAsyncProcessor;
    private readonly ProcessRequest? _RequestProcessor;

    private Action<HttpRequestMessage>? _RequestChecker;
    private Action<HttpRequestHeaders>? _RequestHeadersChecker;
    private Action<HttpContent>? _RequestContentChecker;
    private List<Func<HttpContent, Task>>? _RequestContentAsyncCheckers;
    private Action<Uri?>? _RequestUriChecker;

    public TestHttpClientBuilder(ProcessRequestAsync RequestProcessor) => _RequestAsyncProcessor = RequestProcessor;
    public TestHttpClientBuilder(ProcessRequest RequestProcessor) => _RequestProcessor = RequestProcessor;

    public static TestHttpClientBuilder Create(ProcessRequestAsync RequestProcessor) => new(RequestProcessor);
    public static TestHttpClientBuilder Create(ProcessRequest RequestProcessor) => new(RequestProcessor);

    public TestHttpClientBuilder CheckRequest(Action<HttpRequestMessage> Checker)
    {
        _RequestChecker += Checker;
        return this;
    }

    public TestHttpClientBuilder CheckHeaders(Action<HttpRequestHeaders> Checker)
    {
        _RequestHeadersChecker += Checker;
        return this;
    }

    public TestHttpClientBuilder CheckContent(Action<HttpContent> Checker)
    {
        _RequestContentChecker += Checker;
        return this;
    }

    public TestHttpClientBuilder CheckContent(Func<HttpContent, Task> Checker)
    {
        _RequestContentAsyncCheckers ??= new();
        _RequestContentAsyncCheckers.Add(Checker);
        return this;
    }

    public TestHttpClientBuilder CheckUri(Action<Uri?> Checker)
    {
        _RequestUriChecker += Checker;
        return this;
    }

    public HttpClient Build(Uri? BaseAddress = null)
    {
        var handler = _RequestAsyncProcessor is { } request_async_processor
            ? new TestHandler(request_async_processor)
            : _RequestProcessor is { } request_processor
                ? new TestHandler(request_processor)
                : throw new InvalidOperationException("Не задан метод синхронной обработки запроса");


        if (_RequestChecker is { } request_checker)
            handler.AddRequestChecker(request_checker);

        if (_RequestHeadersChecker is { } request_headers_checker)
            handler.AddRequestHeadersChecker(request_headers_checker);

        if (_RequestContentChecker is { } request_content_checker)
            handler.AddRequestContentChecker(request_content_checker!);

        if(_RequestContentAsyncCheckers is { Count: > 0 } request_content_async_checkers)
            foreach (var request_content_async_checker in request_content_async_checkers)
                handler.AddRequestContentChecker(request_content_async_checker);

        if (_RequestUriChecker is { } request_uri_checker)
            handler.AddRequestUriChecker(request_uri_checker);

        var client = new HttpClient(handler);
        if (BaseAddress is { } uri)
            client.BaseAddress = uri;

        return client;
    }

    private sealed class TestHandler : HttpMessageHandler
    {
        private readonly ProcessRequestAsync? _RequestProcessorAsync;
        private readonly ProcessRequest? _RequestProcessor;

        private Action<HttpRequestMessage>? _RequestChecker;
        private Action<HttpRequestHeaders>? _RequestHeadersChecker;
        private Action<HttpContent>? _RequestContentChecker;
        private List<Func<HttpContent, Task>>? _RequestContentAsyncCheckers;
        private Action<Uri?>? _RequestUriChecker;

        public TestHandler(ProcessRequestAsync RequestProcessor) => _RequestProcessorAsync = RequestProcessor;
        public TestHandler(ProcessRequest RequestProcessor) => _RequestProcessor = RequestProcessor;

        public void AddRequestChecker(Action<HttpRequestMessage> Checker) => _RequestChecker += Checker;
        public void AddRequestHeadersChecker(Action<HttpRequestHeaders> Checker) => _RequestHeadersChecker += Checker;
        public void AddRequestContentChecker(Action<HttpContent> Checker) => _RequestContentChecker += Checker;
        public void AddRequestContentChecker(Func<HttpContent, Task> Checker) => (_RequestContentAsyncCheckers ??= new()).Add(Checker);
        public void AddRequestUriChecker(Action<Uri?> Checker) => _RequestUriChecker += Checker;

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancel)
        {
            _RequestUriChecker?.Invoke(request.RequestUri);
            _RequestHeadersChecker?.Invoke(request.Headers);

            if (request.Content is { } content)
            {
                _RequestContentChecker?.Invoke(content);
                if(_RequestContentAsyncCheckers is { Count: > 0 } async_checkers)
                    foreach (var async_checker in async_checkers)
                        await async_checker(content);
            }
            else if (_RequestContentChecker is not null || _RequestContentAsyncCheckers is { Count: > 0 })
                throw new InvalidOperationException("При наличии правил проверки содержимого запроса, тело запроса отсутствует");

            _RequestChecker?.Invoke(request);

            var response = _RequestProcessorAsync is { } async_processor
                ? await async_processor(request, cancel).ConfigureAwait(false)
                : _RequestProcessor is { } sync_processor
                    ? sync_processor(request)
                    : throw new InvalidOperationException("Не задан метод обработки сообщения запроса");

            return response;
        }
    }
}