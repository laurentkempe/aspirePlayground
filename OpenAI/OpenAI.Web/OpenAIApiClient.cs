namespace OpenAI.Web;

public sealed class OpenAIApiClient(HttpClient httpClient)
{
    public async Task<SummarizedArticle?> GetArticleSummary(string articleSlug) =>
        await httpClient.GetFromJsonAsync<SummarizedArticle>($"/omnivore/summarize/{articleSlug}") ?? null;
}

public sealed record SummarizedArticle(string Title, string Summary);