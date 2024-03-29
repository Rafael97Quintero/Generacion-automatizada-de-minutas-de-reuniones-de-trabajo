

using ChatGPT.Net;

using MauiAppUnir.Services.Interfaces;

using Newtonsoft.Json;

using System.Text;

namespace MauiAppUnir.Services;

public class AIService: IAIService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey = "";

    public AIService()
    {
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
    }

    public async Task<string> GetSummaryAsync(string text)
    {
        // retrieve ai key from configuration
        var openAiKey = _apiKey;

        if (openAiKey == null)
        {
            return "";
        }

        var openai = new ChatGpt(openAiKey);

        var fixedSentence = await openai.Ask($"Haz un resumen de lo siguiente: {text}");
        if (fixedSentence == null)
        {
            return "unable to call chat gpt";
        }
        return  fixedSentence;


    }
}
