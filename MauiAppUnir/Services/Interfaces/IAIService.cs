namespace MauiAppUnir.Services.Interfaces;

public interface IAIService
{
    Task<string> GetSummaryAsync(string text);
}
