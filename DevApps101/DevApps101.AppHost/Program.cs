var builder = DistributedApplication.CreateBuilder(args);
builder.AddDapr();

// Using Aspire Redis Component
var cache = builder.AddRedis("cache");

var apiService = builder.AddProject<Projects.DevApps101_ApiService>("apiservice");

// Using Dapr
var daprStateStore = builder.AddDaprStateStore("statestore");
var daprPubSub = builder.AddDaprPubSub("pubsub");

var countService = builder.AddProject<Projects.DevApps101_CountService>("countservice")
    .WithDaprSidecar();

builder.AddProject<Projects.DevApps101_Web>("webfrontend")
    .WithReference(cache)
    .WithReference(apiService)
    .WithDaprSidecar();

builder.Build().Run();
