using Dapr.Client;
using OpenAI.Shared;

namespace OpenAI.Web;

public sealed class AzureApiClient(HttpClient httpClient, DaprClient daprClient)
{
    public async Task<SummarizedArticle?> GetArticleSummary(string articleSlug)
    {
        var cachedSummarizedArticle = await daprClient.GetStateAsync<SummarizedArticle>("statestore", articleSlug);

        if (cachedSummarizedArticle is not null)
        {
            return cachedSummarizedArticle;
        }

        var summarizedArticle = await httpClient.GetFromJsonAsync<SummarizedArticle>($"/omnivore/summarize/{articleSlug}") ?? null;

        var ttlInSeconds = 120;

        var metadata = new Dictionary<string, string>
        {
            { "ttlInSeconds", ttlInSeconds.ToString() }
        };

        await daprClient.SaveStateAsync("statestore", articleSlug, summarizedArticle, metadata: metadata);
        
        return summarizedArticle;
    }
}