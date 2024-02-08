var builder = DistributedApplication.CreateBuilder(args);

// Using Dapr
builder.AddDapr();

var daprPubSub = builder.AddDaprPubSub("pubsub");
var daprStateStore = builder.AddDaprStateStore("statestore");

// Using Aspire Redis Component
var cache = builder.AddRedis("cache");

// Source generator for project metadata
var apiService = builder.AddProject<Projects.DevApps101_ApiService>("apiservice")
                        .WithReplicas(2);

var countService = builder.AddProject<Projects.DevApps101_CountService>("countservice")
                          .WithDaprSidecar()
                          .WithReference(daprPubSub);

builder.AddProject<Projects.DevApps101_Web>("webfrontend")
    .WithReference(cache)
    .WithReference(apiService)
    .WithDaprSidecar()
    .WithReference(daprPubSub)
    .WithReference(daprStateStore);

builder.Build().Run();
