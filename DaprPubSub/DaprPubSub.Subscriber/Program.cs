using DaprPubSub.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseCloudEvents();
app.MapSubscribeHandler();

app.MapPost("/count", async (HttpContext _, CountEvent countEvent, ILogger<Program> logger) =>
{
    logger.LogInformation("Received count event: {Counter}", countEvent.Counter);
    
    await Task.Delay(5000);

    return Results.NoContent();    
}).WithTopic("pubsub", "Counter");

app.Run();
