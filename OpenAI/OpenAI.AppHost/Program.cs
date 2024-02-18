var builder = DistributedApplication.CreateBuilder(args);

var openai = builder.AddConnectionString("openai");

var azureService = builder.AddProject<Projects.OpenAI_AzureService>("azureservice")
                        .WithReference(openai);

var ollamaService = builder.AddProject<Projects.OpenAI_OllamaService>("ollamaservice");

var daprStateStore = builder.AddDaprStateStore("statestore");

builder.AddProject<Projects.OpenAI_Web>("webfrontend")
    .WithReference(azureService)
    .WithReference(ollamaService)
    .WithReference(daprStateStore)
    .WithDaprSidecar();

builder.Build().Run();
