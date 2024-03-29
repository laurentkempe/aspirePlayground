var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

// Add OpenAI client
builder.AddAzureOpenAI("openai");

// Add services to the container.
builder.Services.AddProblemDetails();

builder.Services.AddSingleton(GetConfiguredOmnivoreGraphQLClient);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

// Omnivore webhook endpoint
app.MapPost("/omnivore/webhook", async (OmnivoreWebhookRequest omnivoreWebhookRequest, OpenAIClient aiClient, IGraphQLClient graphQLClient, ILogger<Program> logger) =>
{
    var article = await OmnivoreClient.GetOmnivoreArticleContent(omnivoreWebhookRequest.Page.Slug, graphQLClient);

    if (article is null) return Results.NotFound("Article not found.");
    
    logger.LogInformation("Received page: {Page}", article.Title);
    
    var articleSummary = await GetArticleSummary(aiClient, article.Content);

    if (articleSummary == null) return Results.Problem("Could not get article summary.");

    logger.LogInformation("Article title: {ArticleTitle}", article.Title);

    logger.LogInformation("Open AI: {ArticleSummary}", articleSummary);
        
    return Results.Ok(new
    {
        Title = article.Title,
        Summary = articleSummary
    });
});

app.MapGet("/omnivore/summarize/{articleSlug}", async (string articleSlug, OpenAIClient aiClient, IGraphQLClient graphQLClient, ILogger<Program> logger) =>
{
    var article = await OmnivoreClient.GetOmnivoreArticleContent(articleSlug, graphQLClient);

    if (article is null) return Results.NotFound("Article not found.");
    
    logger.LogInformation("Received page: {Page}", article.Title);
    
    var articleSummary = await GetArticleSummary(aiClient, article.Content);

    if (articleSummary == null) return Results.Problem("Could not get article summary.");

    logger.LogInformation("Article title: {ArticleTitle}", article.Title);

    logger.LogInformation("Open AI: {ArticleSummary}", articleSummary);
        
    return Results.Ok(new SummarizedArticle(article.Title, articleSummary, []));
});

app.MapDefaultEndpoints();

app.Run();

async Task<string?> GetArticleSummary(OpenAIClient openAiClient, string articleContent)
{
    ChatCompletionsOptions chatCompletionOptions = new()
    {
        // TODO add this to configuration
        DeploymentName = "testing-16k",
        Messages =
        {
            new ChatRequestSystemMessage("You are a bot returning a tweet-length TL;DR of the following article.")
        }
    };
    
    chatCompletionOptions.Messages.Add(new ChatRequestUserMessage(articleContent));

    var response = await openAiClient.GetChatCompletionsAsync(chatCompletionOptions);

    if (!response.HasValue) return null;
    
    return response.Value.Choices[0].Message.Content;
}

IGraphQLClient GetConfiguredOmnivoreGraphQLClient(IServiceProvider serviceProvider)
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    var apiUrl = configuration.GetConnectionString("omnivoreApiUrl");
    var authToken = configuration.GetConnectionString("omnivoreAuthToken");
    var client = new GraphQLHttpClient(apiUrl, new SystemTextJsonSerializer());
    client.HttpClient.DefaultRequestHeaders.Add("Cookie", $"auth={authToken};");
    
    return client;
}