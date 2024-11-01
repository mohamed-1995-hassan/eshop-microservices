using Basket.Api.Data;
using Basket.Api.Models;
using BuildingBlocks.CQRS;
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
	public class StoreBasketCommandHandler(IBasketRepository basketRepository)
		: ICommandHandler<StoreBasketCommand, StoreBasketResult>
	{
		public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
		{
			ShoppingCart cart = command.Cart;
			await basketRepository.StoreBasket(cart, cancellationToken);
			return new StoreBasketResult(command.Cart.UserName);
		}
	}
}
