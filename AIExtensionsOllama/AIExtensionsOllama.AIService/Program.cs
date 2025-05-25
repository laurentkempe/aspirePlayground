var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

// 👇🏼 Configure OpenTelemetry Exporter
var sourceName = Guid.NewGuid().ToString();
var tracerProvider = OpenTelemetry.Sdk.CreateTracerProviderBuilder()
    .AddSource(sourceName)
    .AddOtlpExporter()
    .Build();

// 👇🏼 Injecting Ollama Chat Client with Open Telemetry and Tool calling 
builder.Services.AddChatClient(clientBuilder => clientBuilder
    .UseLogging()
    .UseFunctionInvocation()
    .UseOpenTelemetry(sourceName: sourceName, configure: instance => {
        instance.EnableSensitiveData = true;
    })
    .Use(new OllamaChatClient(new Uri("http://localhost:11434/"), "llama3.2:3b")));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

// 👇🏼 Out Chat endpoint getting IChatClient injected
app.MapGet("/chat/{query}", async (string query, IChatClient chatClient) =>
{
    var chatOptions = new ChatOptions { Tools = [AIFunctionFactory.Create(GetCurrentDateAndTime)] };
    var chatCompletion = await chatClient.CompleteAsync(query, options: chatOptions);
    return chatCompletion.Message.Text ?? "I don't have an answer.";
});

app.MapDefaultEndpoints();

app.Run();

// 👇🏼 Our Tool
[Description("Get the current date and time")]
string GetCurrentDateAndTime() => DateTime.Now.ToString(CultureInfo.CurrentCulture);
