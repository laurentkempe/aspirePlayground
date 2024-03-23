using Dapr.Workflow;
using LocalLLMSummarize.Web.SummarizeWorkflow.Activities;
using LocalLLMSummarize.Web.SummarizeWorkflow.Models;

namespace LocalLLMSummarize.Web.SummarizeWorkflow;

public sealed class ArticleSummarizingWorkflow : Workflow<ArticleToSummarize, ArticleSummarized>
{
    public override async Task<ArticleSummarized> RunAsync(WorkflowContext context, ArticleToSummarize input)
    {
        context.SetCustomStatus("Getting article content");

        var articleContent = await context.CallActivityAsync<ArticleContent>(nameof(GetArticleActivity), input);
        
        
        context.SetCustomStatus("Summarizing article");

        var summarizedContent = await context.CallActivityAsync<ArticleSummarized>(nameof(SummarizeArticleActivity), articleContent);
        
        return summarizedContent with { ArticleId = input.ArticleId };
    }
}