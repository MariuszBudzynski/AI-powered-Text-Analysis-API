using AITextAnalysis.Application.Interfaces;
using AITextAnalysis.Application.Models;
using System.Net.Http.Json;

namespace AITextAnalysis.Application.Client;

public class OllamaClient : ITextAnalysisAiClient
{
    private const int MaxInputLength = 4000;
    private readonly HttpClient _httpClient;

    public OllamaClient(HttpClient httpClient)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<string> AskAsync(string text, CancellationToken cancellationToken = default)
    {
        ValidateInput(text);

        var truncatedText = Truncate(text, MaxInputLength);
        var prompt = BuildPrompt(truncatedText);

        var request = new OllamaRequest { Prompt = prompt };

        var response = await _httpClient.PostAsJsonAsync(
            "/api/generate",
            request,
            cancellationToken
        );

        await EnsureSuccess(response);

        var result = await response.Content.ReadFromJsonAsync<OllamaResponse>(cancellationToken: cancellationToken);

        if (string.IsNullOrWhiteSpace(result.Response))
            throw new InvalidOperationException("AI returned an empty summary.");

        return result.Response;
    }

    private static void ValidateInput(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            throw new ArgumentException("Text to summarize cannot be empty.", nameof(text));
    }

    private static string Truncate(string text, int maxLength)
        => text.Length <= maxLength
            ? text
            : text[..maxLength] + "... [truncated]";

    private static string BuildPrompt(string question) =>
        $"""
        You are a helpful AI assistant. Answer the following question:

        {question}
        """;

    private static async Task EnsureSuccess(HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
            return;

        var content = await response.Content.ReadAsStringAsync();
        throw new InvalidOperationException(
            $"Ollama returned {(int)response.StatusCode}: {response.ReasonPhrase}. Content: {content}");
    }
}