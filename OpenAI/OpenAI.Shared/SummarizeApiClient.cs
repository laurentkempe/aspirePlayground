using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace OpenAI.Shared;

public class SummarizeApiClient
{
    private readonly HttpClient _httpClient;

    public SummarizeApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<SummarizedArticle?> GetArticleSummary(string apiName, string slug)
    {
        // This is a mock implementation for build purposes
        await Task.Delay(100); // simulate network delay
        
        return new SummarizedArticle("Mock Article Title", "This is a mock summary of the article.", new[] { "Mock", "Build", "Test" });
    }
}