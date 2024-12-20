﻿
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Infrastructure.Data.Extentions;

public static class DatabaseExtentions
{
	public static async Task IntializeDatabaseAsync(this WebApplication app)
	{
		using var scope = app.Services.CreateScope();
		var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
		context.Database.MigrateAsync().GetAwaiter().GetResult();

		await SeedAsync(context);
	}

	private static async Task SeedAsync(ApplicationDbContext context)
	{
		await SeedCustomerAsync(context);
		await SeedProductAsync(context);
		await SeedOrderWithItemsAsync(context);
	}


	private static async Task SeedCustomerAsync(ApplicationDbContext context)
	{
		if(!await context.Customers.AnyAsync())
		{
			await context.Customers.AddRangeAsync(IntialData.Customers);
			await context.SaveChangesAsync();
		}
	}

	private static async Task SeedProductAsync(ApplicationDbContext context)
	{
		if (!await context.Products.AnyAsync())
		{
			await context.Products.AddRangeAsync(IntialData.Products);
			await context.SaveChangesAsync();
		}
	}

	private static async Task SeedOrderWithItemsAsync(ApplicationDbContext context)
	{
		if (!await context.Orders.AnyAsync())
		{
			await context.Orders.AddRangeAsync(IntialData.OrdersWithItems);
			await context.SaveChangesAsync();
		}
	}
}
