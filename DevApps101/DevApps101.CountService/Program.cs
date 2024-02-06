using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

// Will unwrap requests that use the cloud events structured format, allowing the event payload to be read directly
app.UseCloudEvents();
// Maps an endpoint that will respond to requests to /dapr/subscribe
app.MapSubscribeHandler();

app.MapPost("/counter", ([FromBody]int counter, ILogger<Program> logger) =>
    {
        logger.LogInformation("Counter is equal to {counter}", counter);
    })
    .WithTopic("pubsub", "counter");

app.Run();

// Show the Services using .NET Aspire plugin