using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
using Microsoft.FluentUI.AspNetCore.Components;
using OpenAI.Shared;
using OpenAI.Web;
using OpenAI.Web.Components;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

builder.Services.AddSingleton(GetConfiguredOmnivoreGraphQLClient);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddFluentUIComponents();

builder.Services.AddOutputCache();

builder.Services.AddHttpClient<SummarizeApiClient>(client => client.BaseAddress = new("http://summarizeservice"));
builder.Services.AddDaprClient();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();

app.UseAntiforgery();

app.UseOutputCache();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapDefaultEndpoints();

app.Run();

IGraphQLClient GetConfiguredOmnivoreGraphQLClient(IServiceProvider serviceProvider)
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    var apiUrl = configuration.GetConnectionString("omnivoreApiUrl");
    var authToken = configuration.GetConnectionString("omnivoreAuthToken");
    var client = new GraphQLHttpClient(apiUrl, new SystemTextJsonSerializer());
    client.HttpClient.DefaultRequestHeaders.Add("Cookie", $"auth={authToken};");
    
    return client;
}