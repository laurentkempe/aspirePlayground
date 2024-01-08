var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.DaprPubSub_ApiService>("apiservice");

builder.AddProject<Projects.DaprPubSub_Web>("webfrontend")
    .WithReference(apiService);

builder.Build().Run();
