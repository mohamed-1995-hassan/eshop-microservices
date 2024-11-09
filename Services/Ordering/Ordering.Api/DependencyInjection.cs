﻿using BuildingBlocks.Exceptions.Handler;
using Carter;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Ordering.Api;

public static class DependencyInjection
{
	public static IServiceCollection AddApiService(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddCarter();
		services.AddExceptionHandler<CustomExceptionHandler>();
		services.AddHealthChecks()
				.AddSqlServer(configuration.GetConnectionString("Database")!);
		return services;
	}
	public static WebApplication UseApiServices(this WebApplication app)
	{
		app.MapCarter();
		app.UseExceptionHandler(opt =>
		{

		});
		app.UseHealthChecks("/health", new HealthCheckOptions
		{
			ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
		});
		return app;
	}
}
