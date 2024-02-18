using System.Text.Json;
using Codeblaze.SemanticKernel.Connectors.Ollama;
using Microsoft.SemanticKernel;
using OpenAI.Shared;
using OpenAI.Shared.Omnivore;

var kernelBuilder = Kernel.CreateBuilder();
kernelBuilder.Services.AddTransient<HttpClient>();
kernelBuilder.AddOllamaTextGeneration(modelId: "llama2", "http://localhost:11434");
var kernel = kernelBuilder.Build();

var builder = WebApplication.CreateBuilder(args);
// builder.AddServiceDefaults();
builder.Services.AddSingleton(GetConfiguredOmnivoreGraphQLClient);
var app = builder.Build();

app.MapGet("/omnivore/summarize/{articleSlug}", async (string articleSlug, IGraphQLClient graphQLClient, ILogger<Program> logger) =>
{
  var article = await OmnivoreClient.GetOmnivoreArticleContent(articleSlug, graphQLClient);

  var prompt = $$"""
                Article Title: {{article.Title}}
                Article Content: {{article.Content}}

                ---------------------------------------------

                Extract the important topics from the following article. Limit the output to a maximum of three topics. Consider the predefined set of topics: 'dotnet', 'dapr', 'ai', 'docker', 'csharp'.
                Finally summarize the key points of the article above in 3 sentences maximum.
                Please format your response in JSON and use "title", "summary", "tags" as attributes.
                """;

  var result = await kernel.InvokePromptAsync(prompt, new KernelArguments());

  var json = result.GetValue<string>();

  logger.LogInformation("Json: {Json}", json);

  var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
  var summarizedArticle = JsonSerializer.Deserialize<SummarizedArticle>(json, options);

  logger.LogInformation("Article title: {ArticleTitle}", summarizedArticle.Title);
  logger.LogInformation("Summary: {Summary}", summarizedArticle.Summary);
  logger.LogInformation("Tags: {Tags}", summarizedArticle.Tags);
  
  return Results.Json(summarizedArticle);
});

app.Run();

IGraphQLClient GetConfiguredOmnivoreGraphQLClient(IServiceProvider serviceProvider)
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    var apiUrl = configuration.GetConnectionString("omnivoreApiUrl");
    var authToken = configuration.GetConnectionString("omnivoreAuthToken");
    var client = new GraphQLHttpClient(apiUrl, new SystemTextJsonSerializer());
    client.HttpClient.DefaultRequestHeaders.Add("Cookie", $"auth={authToken};");
    
    return client;
}