var builder = DistributedApplication.CreateBuilder(args);

builder.AddConnectionString("openai");

var daprStateStore = builder.AddDaprStateStore("statestore");

builder.AddProject<Projects.OpenAI_Web>("webfrontend")
    .WithReference(daprStateStore)
    .WithDaprSidecar();

builder.Build().Run();
