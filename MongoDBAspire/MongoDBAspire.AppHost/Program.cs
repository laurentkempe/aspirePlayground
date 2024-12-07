var builder = DistributedApplication.CreateBuilder(args);

var mongoServer = builder.AddMongoDB("mongoserver")
        .WithDataVolume()
        // 👇🏼 Seed data using script(s) in mongodb-init folder. You can have multiple ones 
        .WithInitBindMount("mongodb-init")
        // 👇🏼 Add web-based MongoDB admin interface   
        .WithMongoExpress()
        // 👇🏼 Keep the container running when AppHost is stopped 
        .WithLifetime(ContainerLifetime.Persistent);

// 👇🏼 Add a database, used by the client registration in MongoDBAspire.ApiService\Program.cs:16
var mongoDb = mongoServer.AddDatabase("mongodb", "weather");

var apiService = 
    builder.AddProject<Projects.MongoDBAspire_ApiService>("apiservice")
        // 👇🏼 API project reference mongoDb and wait that mongo is started to start   
        .WithReference(mongoDb)
        .WaitFor(mongoDb);

var cache = builder.AddRedis("cache");

builder.AddProject<Projects.MongoDBAspire_FluentWeb>("fluentwebfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
