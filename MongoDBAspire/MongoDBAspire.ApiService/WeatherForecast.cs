using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDBAspire.ApiService;

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    [BsonId]
    [JsonIgnore]
    public ObjectId Id { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}