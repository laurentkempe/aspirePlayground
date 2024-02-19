using Microsoft.FluentUI.AspNetCore.Components;
using OpenAI.Web;
using OpenAI.Web.Components;
using OpenAI.Web.OpenAIClients;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddFluentUIComponents();

builder.Services.AddOutputCache();

builder.Services.AddHttpClient<OllamaApiClient>(client => client.BaseAddress = new("http://ollamaservice"));
builder.Services.AddHttpClient<AzureApiClient>(client => client.BaseAddress = new("http://azureservice"));
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
