namespace LocalLLMSummarize.Web.SummarizeWorkflow.Models;

public sealed record ArticleContent(string ArticleId, string Title, string Content)
{
    public static ArticleContent Empty => new(string.Empty, string.Empty, string.Empty);
}