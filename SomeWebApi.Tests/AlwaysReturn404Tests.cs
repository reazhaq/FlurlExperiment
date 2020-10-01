using System;
using Flurl;
using Flurl.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace SomeWebApi.Tests
{
    public class AlwaysReturn404Tests : IClassFixture<WebApplicationFactory<SomeWebApi.Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        private readonly ITestOutputHelper _testOutputHelper;

        private readonly JsonSerializerSettings _jsonSettings = new JsonSerializerSettings
            {ReferenceLoopHandling = ReferenceLoopHandling.Ignore};

        public AlwaysReturn404Tests(WebApplicationFactory<SomeWebApi.Startup> factory, ITestOutputHelper testOutputHelper)
        {
            _factory = factory;
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public async void CallGetTwice()
        {
            var client = _factory.CreateClient();

            var uri = "api/AlwaysReturn404";

            var response1 = await client.GetAsync(uri);
            _testOutputHelper.WriteLine($"1st GET response: {response1}");

            var response2 = await client.GetAsync(uri);
            _testOutputHelper.WriteLine($"2nd GET response: {response2}");
        }

        [Fact]
        public async void CallGetTwiceUsingFlurl()
        {
            var client = _factory.CreateClient();
            var uriSegments = new [] {"api", "AlwaysReturn404"};
            using var flurlClient = new FlurlClient(client);

            var getRequest = flurlClient.Request(uriSegments);
            try
            {
                _ = await getRequest.GetAsync();
            }
            catch(FlurlHttpException exception)
            {
                _testOutputHelper.WriteLine($"1st GET response: {exception.Call?.Response?.StatusCode}");
            }

            try
            {
                _ = await getRequest.GetAsync();
            }
            catch (FlurlHttpException exception)
            {
                _testOutputHelper.WriteLine($"2nd GET response: {exception.Call?.Response?.StatusCode}");
            }
        }

        [Fact]
        public async void CallPutTwice()
        {
            var client = _factory.CreateClient();

            var uri = "api/AlwaysReturn404";

            var response1 = await client.PutAsync(uri, null);
            _testOutputHelper.WriteLine($"1st PUT response: {response1}");

            var response2 = await client.PutAsync(uri, null);
            _testOutputHelper.WriteLine($"2nd PUT response: {response2}");
        }

        [Fact]
        public async void CallPutTwiceUsingFlurl()
        {
            var client = _factory.CreateClient();
            var uriSegments = new[] { "api", "AlwaysReturn404" };
            using var flurlClient = new FlurlClient(client);

            var putRequest = flurlClient.Request(uriSegments);
            try
            {
                _ = await putRequest.PutAsync(null);
            }
            catch (FlurlHttpException exception)
            {
                _testOutputHelper.WriteLine($"1st PUT response: {exception.Call?.Response?.StatusCode}");
            }

            try
            {
                _ = await putRequest.PutAsync(null);
            }
            catch (FlurlHttpException exception)
            {
                _testOutputHelper.WriteLine($"2nd PUT response: {exception.Call?.Response?.StatusCode}");
            }
        }
    }
}