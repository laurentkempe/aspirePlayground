var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.GrafanaDashboards_ApiService>("apiservice");

builder.AddProject<Projects.GrafanaDashboards_Web>("webfrontend")
    .WithReference(apiService);

builder.Build().Run();
