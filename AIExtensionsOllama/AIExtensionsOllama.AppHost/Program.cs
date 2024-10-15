var builder = DistributedApplication.CreateBuilder(args);

var aiService = builder.AddProject<Projects.AIExtensionsOllama_AIService>("aiservice");

builder.AddProject<Projects.AIExtensionsOllama_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(aiService)
    .WaitFor(aiService);

builder.Build().Run();
