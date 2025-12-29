using AITextAnalysis.Application.Client;
using AITextAnalysis.Application.Models;
using AITextAnalysis.UnitTests.FakeServices;
using System.Net;
using System.Text;
using System.Text.Json;

namespace AITextAnalysis.UnitTests
{
    public class OllamaClientTests
    {
        [Fact]
        public async Task AskAsync_WhenResponseIsValid_ReturnsText()
        {
            //Arrange
            var ollamaResponse = "This is a text response";
            var httpResponseMessage = CreateFakeOkResponse(ollamaResponse);

            var httpClient = HttpClientBuilder(httpResponseMessage);

            var client = new OllamaClient(httpClient);


            //Act
            var result = await client.AskAsync("Some text", CancellationToken.None);

            //Assert
            Assert.Equal("This is a text response", result);
        }

        [Fact]
        public async Task AskAsync_WhenTextIsEmpty_ThrowsArgumentException()
        {
            //Arrange
            var ollamaResponse = "This is a text response";
            var httpResponseMessage = CreateFakeOkResponse(ollamaResponse);

            var httpClient = HttpClientBuilder(httpResponseMessage);

            var client = new OllamaClient(httpClient);


            //Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => client.AskAsync(String.Empty, CancellationToken.None));
        }

        [Fact]
        public async Task AskAsync_WhenAiReturnsEmptyResponse_ThrowsException()
        {
            //Arrange
            var ollamaResponse = String.Empty;
            var httpResponseMessage = CreateFakeOkResponse(ollamaResponse);

            var httpClient = HttpClientBuilder(httpResponseMessage);

            var client = new OllamaClient(httpClient);


            //Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => client.AskAsync("Test", CancellationToken.None));
        }

        [Fact]
        public async Task AskAsync_WhenOllamaReturns500_ThrowsException()
        {
            //Arrange
            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                ReasonPhrase = "Internal Error",
                Content = new StringContent("Ollama failed")
            };

            var httpClient = HttpClientBuilder(httpResponseMessage);

            var client = new OllamaClient(httpClient);


            //Act
            var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => client.AskAsync("Test", CancellationToken.None));

            //Assert
            Assert.Contains("Ollama returned 500", ex.Message);
        }

        private static HttpClient HttpClientBuilder(HttpResponseMessage httpResponseMessage)
        {

            var handler = new FakeHttpMessageHandler(_ => httpResponseMessage);

            var httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri("http://localhost")
            };

            return httpClient;
        }

        private static HttpResponseMessage CreateFakeOkResponse(string ollamaResponse)
        {
            var fakeResponse = new OllamaResponse
            {
                Response = ollamaResponse
            };

            var httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(
                    JsonSerializer.Serialize(fakeResponse),
                    Encoding.UTF8,
                   "application/json")
            };
            return httpResponseMessage;
        }
    }
}
