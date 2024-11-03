
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Infrastructure.Data;

namespace Ordering.Infrastructure;

public static class DependencyInjection
{
	public static IServiceCollection AddInfrastructureService(this IServiceCollection services,
														   IConfiguration configuration)
	{
		var configurationString = configuration.GetConnectionString("Database");

		services.AddDbContext<ApplicationDbContext>(options =>
		{
			options.UseSqlServer(configurationString);
		});
		return services;
	}
}
