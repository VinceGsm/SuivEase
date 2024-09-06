







var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.SuivEase>("SuivEase");

builder.Build().Run();