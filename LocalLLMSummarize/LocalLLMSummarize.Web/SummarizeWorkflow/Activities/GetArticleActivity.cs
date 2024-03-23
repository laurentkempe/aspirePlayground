using Dapr.Workflow;
using LocalLLMSummarize.Web.SummarizeWorkflow.Models;

namespace LocalLLMSummarize.Web.SummarizeWorkflow.Activities;

public class GetArticleActivity(OmnivoreClient.OmnivoreClient omnivoreClient, ILogger<SummarizeArticleActivity> logger)
    : WorkflowActivity<ArticleToSummarize, ArticleContent>
{
    public override async Task<ArticleContent> RunAsync(WorkflowActivityContext context, ArticleToSummarize input)
    {
        if (logger.IsEnabled(LogLevel.Information))
        {
            logger.LogInformation("Getting article {ArticleId}", input.ArticleId);
        }

        var articleContent = await omnivoreClient.GetArticleContent(input.ArticleId);

        if (logger.IsEnabled(LogLevel.Information))
        {
            logger.LogInformation("Loaded article {ArticleId} with '{ArticleTitle}'", input.ArticleId, articleContent.Title);
        }
        
        return await Task.FromResult(new ArticleContent(input.ArticleId,  articleContent.Title, articleContent.Content));
    }
}