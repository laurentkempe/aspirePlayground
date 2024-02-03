using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDaprClient();

var app = builder.Build();

app.UseCloudEvents();
app.MapSubscribeHandler();

app.MapPost("/counter", ([FromBody]int counter, ILogger<Program> logger) =>
    {
        logger.LogInformation("Counter is equal to {counter}", counter);
    })
    .WithTopic("pubsub", "counter");

app.Run();