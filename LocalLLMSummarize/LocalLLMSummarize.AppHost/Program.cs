using Projects;

var builder = DistributedApplication.CreateBuilder(args);
builder.AddDapr(options => options.EnableTelemetry = true);

var summarizeService = builder.AddProject<LocalLLMSummarize_SummarizeService>("summarizeservice")
    .WithDaprSidecar();

builder.AddProject<LocalLLMSummarize_Web>("frontend")
    .WithReference(summarizeService)
    .WithDaprSidecar();

builder.Build().Run();