var builder = DistributedApplication.CreateBuilder(args);

builder.AddConnectionString("openai");

var summarizeService = builder.AddProject<Projects.OpenAI_SummarizeService>("summarizeservice");

var daprStateStore = builder.AddDaprStateStore("statestore");

builder.AddProject<Projects.OpenAI_Web>("webfrontend")
    .WithReference(summarizeService)
    .WithReference(daprStateStore)
    .WithDaprSidecar();

builder.Build().Run();
