using Dapr.Client;
using DevApps101.Web;
using DevApps101.Web.Components;

var builder = WebApplication.CreateBuilder(args);

// 3. Add service defaults & Aspire components.
builder.AddServiceDefaults();
builder.AddRedisOutputCache("cache");

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient<WeatherApiClient>(
    // 4. Configure the client using the service name
    client => client.BaseAddress = new("http://apiservice"));
builder.Services.AddSingleton(new DaprClientBuilder().Build());

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
