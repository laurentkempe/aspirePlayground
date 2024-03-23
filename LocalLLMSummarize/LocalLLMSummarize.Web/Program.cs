using Dapr.Workflow;
using Microsoft.FluentUI.AspNetCore.Components;
using LocalLLMSummarize.Web.Components;
using LocalLLMSummarize.Web.Omnivore;
using LocalLLMSummarize.Web.SummarizeWorkflow;
using LocalLLMSummarize.Web.SummarizeWorkflow.Activities;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

builder.AddOmnivoreClient();

builder.Services.AddDaprClient();

builder.Services.AddDaprWorkflow(options =>
{
    options.RegisterWorkflow<ArticleSummarizingWorkflow>();

    options.RegisterActivity<GetArticleActivity>();
    options.RegisterActivity<SummarizeArticleActivity>();
});

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddFluentUIComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();