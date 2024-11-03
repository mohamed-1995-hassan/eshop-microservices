﻿using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data
{
	public class DiscountContext : DbContext
	{
		public DbSet<Coupon> Coupons { get; set; } = default;

        public DiscountContext(DbContextOptions<DiscountContext> options) : base(options)
        {
            
        }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Coupon>().HasData(
				new Coupon
				{
					Id = 1,
					Description = "IPhone Discount",
					Amount = 20,
					ProductName = "IPhone X"
				},
				new Coupon
				{
					Id = 2,
					Description = "Samsung Discount",
					Amount = 30,
					ProductName = "Samsung 10"
				}
				);
		}
	}
}
