namespace DevApps101.Web;

public sealed class WeatherApiClient(HttpClient httpClient)
{
    public async Task<WeatherForecast[]> GetWeatherAsync() =>
        // 5. Use the client to call the service
        await httpClient.GetFromJsonAsync<WeatherForecast[]>("/weatherforecast") ?? [];
}

public record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
