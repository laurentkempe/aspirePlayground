using DaprOutbox.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

var app = builder.Build();

// app.UseCloudEvents();
app.MapSubscribeHandler();

app.MapPost("/count", (CountEvent countEvent, ILogger<Program> logger) =>
{
    logger.LogInformation("Received count event: {Counter}", countEvent.Counter);
    
}).WithTopic("pubsub", "Counter");

app.Run();
