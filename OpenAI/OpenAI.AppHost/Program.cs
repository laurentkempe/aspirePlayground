var builder = DistributedApplication.CreateBuilder(args);

var openai = builder.AddConnectionString("openai");

var apiService = builder.AddProject<Projects.OpenAI_ApiService>("apiservice")
                        .WithReference(openai);

builder.AddProject<Projects.OpenAI_Web>("webfrontend")
    .WithReference(apiService)
    .WithReference(openai);

builder.Build().Run();
