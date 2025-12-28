namespace AITextAnalysis.Application.Interfaces
{
    public interface ITextAnalysisAiClient
    {
        Task<string> AskAsync(string text, CancellationToken cancellationToken);
    }
}
