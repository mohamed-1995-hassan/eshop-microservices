using Ordering.Api;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Data.Extentions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationService(builder.Configuration)
				.AddInfrastructureService(builder.Configuration)
				.AddApiService(builder.Configuration);
var app = builder.Build();

app.UseApiServices();

if (app.Environment.IsDevelopment())
{
	await app.IntializeDatabaseAsync();
}

app.Run();
