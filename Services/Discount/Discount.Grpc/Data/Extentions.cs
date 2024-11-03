﻿using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data
{
	public static class Extentions
	{
		public static IApplicationBuilder UseMigtration(this IApplicationBuilder app) 
		{
			using var scope = app.ApplicationServices.CreateScope();
			using var dbContext = scope.ServiceProvider.GetRequiredService<DiscountContext>();
			dbContext.Database.MigrateAsync();
			return app;
		}
	}
}
