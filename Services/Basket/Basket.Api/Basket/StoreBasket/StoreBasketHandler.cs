using Basket.Api.Data;
using Basket.Api.Models;
using BuildingBlocks.CQRS;
using Discount.Grpc;
using FluentValidation;

namespace Basket.Api.Basket.StoreBasket
{
	public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;

	public record StoreBasketResult(string UserName);

	public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
	{
        public StoreBasketCommandValidator()
        {
			RuleFor(x => x.Cart).NotNull().WithMessage("Cart Can not Be Null");
			RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("UserName is required");

        }
    }
	public class StoreBasketCommandHandler(IBasketRepository basketRepository,
										   DiscountProtoService.DiscountProtoServiceClient discountProto)
		: ICommandHandler<StoreBasketCommand, StoreBasketResult>
	{
		public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
		{
			await DeductDiscount(command.Cart, cancellationToken);
			await basketRepository.StoreBasket(command.Cart, cancellationToken);
			return new StoreBasketResult(command.Cart.UserName);
		}

		private async Task DeductDiscount(ShoppingCart cart, CancellationToken cancellationToken)
		{
			foreach (var item in cart.Items)
			{
				var coupon = await discountProto.GetDiscountAsync(new GetDiscountRequest
				{
					ProductName = item.ProductName
				}, cancellationToken: cancellationToken);
				item.Price -= coupon.Amount;
			}
		}
	}
}
