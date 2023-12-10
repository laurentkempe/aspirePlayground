var builder = DistributedApplication.CreateBuilder(args);

var apiservice = builder.AddProject<Projects.DevContainer_ApiService>("apiservice");

builder.AddProject<Projects.DevContainer_Web>("webfrontend")
    .WithReference(apiservice);

builder.Build().Run();
