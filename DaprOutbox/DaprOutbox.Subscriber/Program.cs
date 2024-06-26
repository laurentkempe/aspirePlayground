using CloudNative.CloudEvents;
using DaprOutbox.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

var app = builder.Build();

// app.UseCloudEvents();
app.MapSubscribeHandler();

app.MapPost("/count", (HttpContext context, CloudEvent cloudEvent, ILogger<Program> logger) =>
{
    logger.LogInformation("Received count event: {Counter}", cloudEvent.Data);
    return Task.CompletedTask;
}).WithTopic("pubsub", "Counter");

app.Run();
