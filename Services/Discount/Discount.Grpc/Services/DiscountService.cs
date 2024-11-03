using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services
{
	public class DiscountService(DiscountContext dbContext, ILogger<DiscountService>logger)
		: DiscountProtoService.DiscountProtoServiceBase
	{
		public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
		{
			var copoun = await dbContext
										.Coupons
										.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);
			if (copoun == null) 
			{
				copoun = new Coupon
				{
					ProductName = "no discount",
					Amount = 0,
					Description = "no discount description"
				};
			}
			logger.LogInformation("dicount is received for productName : {productName}, Amount : {Amount}", copoun.ProductName, copoun.Amount);
			var copounModel = copoun.Adapt<CouponModel>();
			return copounModel;
		}
		public async override Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
		{
			var copoun = request.Coupon.Adapt<Coupon>();
			if (copoun is null)
				throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));
			dbContext.Coupons.Add(copoun);
			await dbContext.SaveChangesAsync();

			logger.LogInformation("dicount successfully created. productName {productName}, Amount {Amount}", copoun.ProductName, copoun.Amount);

			var copounModel = copoun.Adapt<CouponModel>();
			return copounModel;
		}
		public async override Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
		{
			var copoun = request.Coupon.Adapt<Coupon>();
			if (copoun is null)
				throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));
			dbContext.Coupons.Update(copoun);
			await dbContext.SaveChangesAsync();

			logger.LogInformation("dicount successfully updated. productName {productName}, Amount {Amount}", copoun.ProductName, copoun.Amount);

			var copounModel = copoun.Adapt<CouponModel>();
			return copounModel;
		}
		public async override Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
		{
			var coupon = await dbContext
										.Coupons
										.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

			if (coupon is null)
				throw new RpcException(new Status(StatusCode.NotFound, "Discount with product is not found"));

			dbContext.Coupons.Remove(coupon);
			await dbContext.SaveChangesAsync();

			logger.LogInformation("dicount successfully deleted. productName {productName}, Amount {Amount}", coupon.ProductName, coupon.Amount);
			return new DeleteDiscountResponse
			{
				Success = true
			};
		}
	}
}
