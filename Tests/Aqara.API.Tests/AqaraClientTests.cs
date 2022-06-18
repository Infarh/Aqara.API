using System.Net;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;
using Aqara.API.DTO;
using Aqara.API.Tests.Models;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting.Extensions;

using Moq;

namespace Aqara.API.Tests;

[TestClass]
public class AqaraClientTests
{
    private class TestLogger : ILogger<AqaraClient>
    {
        private readonly List<(LogLevel Level, EventId Id, string Meesage, Exception? Error)> _Log;

        public TestLogger(List<(LogLevel Level, EventId Id, string Meesage, Exception? Error)> Log) => _Log = Log;

        public IDisposable BeginScope<TState>(TState state) => null!;

        public bool IsEnabled(LogLevel Level) => true;

        public void Log<TState>(
            LogLevel Level,
            EventId Id,
            TState State,
            Exception? Exception,
            Func<TState, Exception?, string> Formatter) =>
            _Log.Add((Level, Id, Formatter(State, Exception), Exception));
    }

    private List<(LogLevel Level, EventId Id, string Meesage, Exception? Error)> _Log = new();
    private ILogger<AqaraClient> _Logger;

    [TestInitialize]
    public void Initialize() => _Logger = new TestLogger(_Log);

    [TestMethod]
    public async Task RequestAuthorizationKey_Test()
    {
        const string app_id = "app_id-test";
        const string key_id = "key_id-test";
        const string app_key = "app_key-test";
        const string account = "user@server.ru";
        const string response_id = "test_response_id";
        const string response_message = "test_response_message";
        const string response_authorization_code = "test_authorization_code";
        const string test_url = "https://test.server.ru";

        var token_store_mock = new Mock<IAccessTokenSource>();

        var http = TestHttpClientBuilder
           .Create(ProcessRequestAuthorizationKeyRequest)
           .CheckHeaders(headers =>
            {
                var app_id_header = headers.Single(h => string.Equals(h.Key, "AppId")).Value.Single();
                var key_id_header = headers.Single(h => string.Equals(h.Key, "KeyId")).Value.Single();
                var nonce_header = headers.Single(h => string.Equals(h.Key, "Nonce")).Value.Single();
                var time_header = headers.Single(h => string.Equals(h.Key, "Time")).Value.Single();

                app_id_header.AssertEquals(app_id);
                key_id_header.AssertEquals(key_id);

                var sign_header = headers.Single(h => string.Equals(h.Key, "Sign")).Value.Single();

                var sign_str = new StringBuilder()
                   .Append("AppId=").Append(app_id_header).Append('&')
                   .Append("KeyId=").Append(key_id_header).Append('&')
                   .Append("Nonce=").Append(nonce_header).Append('&')
                   .Append("Time=").Append(time_header)
                   .Append(app_key)
                   .ToString()
                   .ToLower();

                var hash = MD5.HashData(Encoding.UTF8.GetBytes(sign_str));
                var hash_string = string.Join("", hash.Select(v => v.ToString("x2")));

                sign_header.AssertEquals(hash_string);
            })
           .CheckContent(async content =>
            {
                var request = await content.ReadFromJsonAsync<AuthorizationCodeRequestModel>();

                Assert.IsNotNull(request);

                request.intent.AssertEquals("config.auth.getAuthCode");
                request.data.account.AssertEquals(account);
                request.data.accessTokenValidity.AssertEquals("1h");
                request.data.accountType.AssertEquals(0);
            })
           .Build(new(test_url));

        HttpResponseMessage ProcessRequestAuthorizationKeyRequest(HttpRequestMessage request)
        {
            var response_value = new AuthorizationCodeResponse
            {
                Code = 0,
                Message = response_message,
                RequestId = response_id,
                Result = new()
                {
                    AuthorizationCode = response_authorization_code
                }
            };

            var content = JsonContent.Create(response_value);

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = content
            };

            return response;
        }

        var client = new AqaraClient(http, token_store_mock.Object, _Logger, new AqaraClientConfig
        {
            TokenStorageFile = "access_token.json",
            AppId = app_id,
            KeyId = key_id,
            AppKey = app_key,
        });

        var result = await client.RequestAuthorizationKey(account);

        result.AssertEquals(response_authorization_code);
    }
}