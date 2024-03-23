using Dapr.Client;
using Dapr.Workflow;
using LocalLLMSummarize.Web.SummarizeWorkflow.Models;
using OmnivoreClient;

namespace LocalLLMSummarize.Web.SummarizeWorkflow.Activities;

public sealed class SummarizeArticleActivity(DaprClient daprClient, ILogger<SummarizeArticleActivity> logger)
    : WorkflowActivity<ArticleContent, ArticleSummarized>
{
    public override async Task<ArticleSummarized> RunAsync(WorkflowActivityContext context, ArticleContent input)
    {
        if (logger.IsEnabled(LogLevel.Information))
        {
            logger.LogInformation("Summarizing article {ArticleId}", input.ArticleId);
        }
        
        var summarizedArticle = await daprClient.InvokeMethodAsync<ArticleContent, SummarizedArticle>("summarizeservice", "summarize", input);
        
        return await Task.FromResult(new ArticleSummarized(input.ArticleId, summarizedArticle.Summary));
    }
}