var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.DaprOutbox_ApiService>("apiservice");

var subscriberService = builder.AddProject<Projects.DaprOutbox_Subscriber>("subscriber");

builder.AddProject<Projects.DaprOutbox_Web>("webfrontend")
    .WithReference(apiService);

builder.Build().Run();
