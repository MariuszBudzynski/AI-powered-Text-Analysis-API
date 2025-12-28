using AITextAnalysis.Application.Client;
using AITextAnalysis.Application.DTOS;
using AITextAnalysis.Application.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Configuration
var ollamaBaseUrl = builder.Configuration["Ollama:BaseUrl"] ?? String.Empty;

// Services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<ITextAnalysisAiClient, OllamaClient>(client =>
{
    client.BaseAddress = new Uri(ollamaBaseUrl);
});

// App
var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Endpoints
var textApi = app.MapGroup("/api/text");

textApi.MapPost("/ask", async (
    AskRequest request,
    ITextAnalysisAiClient aiClient,
    CancellationToken ct
    ) =>
{
    if (string.IsNullOrWhiteSpace(request.Text))
        return Results.BadRequest("Text cannot be empty.");

    try
    {
        var summary = await aiClient.AskAsync(request.Text, ct);
        return Results.Ok(new AskResponse(summary));
    }
    catch (OperationCanceledException)
    {
        return Results.StatusCode(StatusCodes.Status499ClientClosedRequest);
    }
    catch (Exception)
    {
        return Results.Problem("Summarization failed.");
    }
});

await app.RunAsync();