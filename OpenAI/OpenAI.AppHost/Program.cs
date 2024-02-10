var builder = DistributedApplication.CreateBuilder(args);

var openai = builder.AddConnectionString("openai");

var apiService = builder.AddProject<Projects.OpenAI_ApiService>("openaiapiservice")
                        .WithReference(openai);

var daprStateStore = builder.AddDaprStateStore("statestore");

builder.AddProject<Projects.OpenAI_Web>("webfrontend")
    .WithReference(apiService)
    .WithReference(daprStateStore)
    .WithDaprSidecar();

builder.Build().Run();
