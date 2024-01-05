using System.Collections.Immutable;
using Aspire.Hosting.Dapr;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddDapr(options => options.EnableTelemetry = true);

var apiService = builder.AddProject<Projects.DaprOutbox_ApiService>("apiservice");

var daprResourcesPaths = ImmutableHashSet.Create("./components");

var subscriberService = builder.AddProject<Projects.DaprOutbox_Subscriber>("subscriber")
    .WithDaprSidecar(new DaprSidecarOptions
    {
        AppId = "subscriber-dapr",
        Config = "./components/config.yaml",
        ResourcesPaths = daprResourcesPaths
    });

builder.AddProject<Projects.DaprOutbox_Web>("webfrontend")
    .WithReference(apiService)
    .WithDaprSidecar(new DaprSidecarOptions
    {
        AppId = "webfrontend-dapr",
        Config = "./components/config.yaml",
        ResourcesPaths = daprResourcesPaths
    });

builder.Build().Run();
