namespace AITextAnalysis.Application.Models
{
    public class OllamaRequest
    {
        public string Model { get; set; } = "llama3";
        public string Prompt { get; set; } = string.Empty;
        public bool Stream { get; set; } = false;
    }
}
