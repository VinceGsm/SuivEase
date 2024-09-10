







var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

builder.AddProject<Projects.SuivEase>("SuivEase")
    .WithExternalHttpEndpoints()
    .WithReference(cache);

builder.Build().Run();