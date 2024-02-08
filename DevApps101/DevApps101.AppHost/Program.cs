var builder = DistributedApplication.CreateBuilder(args);

// Using Dapr
builder.AddDapr();

// Using Aspire Redis Component
var cache = builder.AddRedis("cache");

// Source generator for project metadata
var apiService = builder.AddProject<Projects.DevApps101_ApiService>("apiservice")
                        .WithReplicas(2);

var countService = builder.AddProject<Projects.DevApps101_CountService>("countservice")
                          .WithDaprSidecar();

builder.AddProject<Projects.DevApps101_Web>("webfrontend")
    .WithReference(cache)
    .WithReference(apiService)
    .WithDaprSidecar();

builder.Build().Run();
