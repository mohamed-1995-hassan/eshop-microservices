namespace Ordering.Api;

public static class DependencyInjection
{
	public static IServiceCollection AddApiService(this IServiceCollection services)
	{

		return services;
	}
	public static WebApplication UseApiServices(this WebApplication app)
	{
		return app;
	}
}
