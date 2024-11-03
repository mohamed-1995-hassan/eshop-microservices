using Ordering.Api;
using Ordering.Application;
using Ordering.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationService()
				.AddInfrastructureService(builder.Configuration)
				.AddApiService();
var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
