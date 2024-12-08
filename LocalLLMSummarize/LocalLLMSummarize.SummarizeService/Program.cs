using Microsoft.SemanticKernel;

var builder = WebApplication.CreateBuilder(args);

var kernelBuilder = Kernel.CreateBuilder();
kernelBuilder.Services.AddTransient<HttpClient>();
#pragma warning disable SKEXP0070
kernelBuilder.AddOllamaTextGeneration("phi3:3.8b", new Uri("http://localhost:11434"));
#pragma warning restore SKEXP0070

builder.Services.AddSingleton(kernelBuilder.Build());

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapPost("/summarize", async (ArticleContent article, ILogger<Program> logger) =>
{
    logger.LogInformation("Received page with title: {Title}", article.Title);

    var summarizedArticle = await SummarizeUsingOllama(article, kernelBuilder.Build(), logger);
    
    return Results.Json(summarizedArticle);
});

app.Run();

async Task<SummarizedArticle> SummarizeUsingOllama(ArticleContent article, Kernel kernel, ILogger<Program> logger)
{
    var prompt = $$"""
                   ## Instructions
                   Summarize the key take away of the following article in two to three sentences and return only the summary content.
                   
                   ## Article
                   The article to summarize is:

                   Title: {{article.Title}}
                   {{article.Content}}
                   """;

    var result = await kernel.InvokePromptAsync(prompt, new KernelArguments());

    var summary = result.GetValue<string>();

    logger.LogInformation("Json: {Json}", summary);
    return new SummarizedArticle(article.Title, summary ?? "", []);
}

public sealed record ArticleContent(string ArticleId, string Title, string Content);

public sealed record SummarizedArticle(string Title, string Summary, string[] Tags);