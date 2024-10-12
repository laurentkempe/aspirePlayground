using System.Collections.Immutable;
using Aspire.Hosting.Dapr;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddDapr(options => options.EnableTelemetry = true);

var daprResourcesPaths = ImmutableHashSet.Create("./components");

builder.AddProject<Projects.DaprPubSub_Subscriber>("subscriber")
    .WithDaprSidecar(new DaprSidecarOptions
    {
        AppId = "subscriber-dapr",
        Config = "./components/config.yaml",
        ResourcesPaths = daprResourcesPaths
    });

builder.AddProject<Projects.DaprPubSub_Web>("webfrontend")
    .WithDaprSidecar(new DaprSidecarOptions
    {
        AppId = "webfrontend-dapr",
        Config = "./components/config.yaml",
        ResourcesPaths = daprResourcesPaths
    });

builder.Build().Run();
