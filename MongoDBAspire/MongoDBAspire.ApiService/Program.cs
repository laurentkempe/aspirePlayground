using MongoDB.Driver;
using MongoDBAspire.ApiService;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// 👇🏼 Keep the container running when AppHost is stopped 
builder.AddMongoDBClient("mongodb");

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment()) app.MapOpenApi();

// 👇🏼 Inject IMongoClient from IoC container
app.MapGet("/weatherforecast", (IMongoClient client) =>
    {
        var database = client.GetDatabase("weather");
        var collection = database.GetCollection<WeatherForecast>("forecast");

        // Check if the collection exists with 5 elements initialized from AppHost WithInitBindMount call.
        // If the seed was done, extend with 5 more elements.
        if (collection.CountDocuments(FilterDefinition<WeatherForecast>.Empty) == 5)
        {
            SeedWeatherForecasts(collection);
        }

        return  collection.Find(FilterDefinition<WeatherForecast>.Empty).ToList();
        
        static void SeedWeatherForecasts(IMongoCollection<WeatherForecast> mongoCollection)
        {
            string[] summaries =
                ["Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"];

            var forecast = Enumerable.Range(1, 5).Select(index =>
                    new WeatherForecast
                    (
                        DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                        Random.Shared.Next(-20, 55),
                        summaries[Random.Shared.Next(summaries.Length)]
                    ))
                .ToArray();

            mongoCollection.InsertMany(forecast);
        }
    })
    .WithName("GetWeatherForecast");

app.MapDefaultEndpoints();

app.Run();