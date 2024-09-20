using System.Net.Mime;

#pragma warning disable SKEXP0003

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

// Semantic Kernel
builder.Services.AddScoped<Kernel>(sp =>
{
    var kernelBuilder = Kernel.CreateBuilder();
    kernelBuilder.Services.AddTransient<HttpClient>();
    kernelBuilder.Services.AddLogging(b => b.AddConsole());
    kernelBuilder.AddOllamaChatCompletion(modelId: "phi3:3.8b", "http://localhost:11434");

    return kernelBuilder.Build();
});

// Add services to the container.
builder.Services.AddProblemDetails();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

app.MapGet(pattern: "/chat/{prompt}", async (string prompt, Kernel kernel, ILogger<Program> logger) =>
{
    logger.LogInformation("Received chat prompt: {Prompt}", prompt);
    
    var result = await kernel.InvokePromptAsync(prompt, new KernelArguments());

    var answer = result.GetValue<string>();

    logger.LogInformation("Chat response: {Response}", answer);

    return answer;
});

app.MapDefaultEndpoints();

app.Run();