namespace LocalLLMSummarize.Web.Omnivore;

public static class HostApplicationBuilderExtensions
{
    public static IHostApplicationBuilder AddOmnivoreClient(this IHostApplicationBuilder builder)
    {
        var appsettingsJson = "appsettings.json";
        
        if (builder.Environment.IsDevelopment())
        {
            appsettingsJson = "appsettings.Development.json";
        }

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(appsettingsJson, optional: false, reloadOnChange: true)
            .Build();
        
        var apiUrl = configuration.GetConnectionString("omnivoreApiUrl");
        var authToken = configuration.GetConnectionString("omnivoreAuthToken");
        
        builder.Services.AddSingleton(new OmnivoreClient.OmnivoreClient(apiUrl, authToken));

        return builder;
    }

}