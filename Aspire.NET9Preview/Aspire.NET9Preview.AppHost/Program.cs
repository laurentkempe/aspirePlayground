var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.Aspire_NET9Preview_ApiService>("apiservice");

builder.AddProject<Projects.Aspire_NET9Preview_Web>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService);

builder.Build().Run();
