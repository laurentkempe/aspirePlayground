var builder = DistributedApplication.CreateBuilder(args);

var qdrant = builder.AddContainer("qdrant", "qdrant/qdrant")
        .WithEndpoint(port: 6333, targetPort: 6333, name: "qdrant", scheme: "http");

var apiService = builder.AddProject<Projects.Ollama_ApiService>("apiservice")
    .WithEnvironment("QDRANT_ENDPOINT", qdrant.GetEndpoint("qdrant"));

builder.AddProject<Projects.Ollama_Web>("webfrontend")
    .WithReference(apiService);

builder.Build().Run();
