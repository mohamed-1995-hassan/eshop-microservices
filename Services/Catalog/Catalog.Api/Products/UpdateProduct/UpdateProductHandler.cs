using BuildingBlocks.CQRS;
using Catalog.Api.Exceptions;
using Catalog.Api.Models;
using FluentValidation;
using Marten;

namespace Catalog.Api.Products.UpdateProduct
{
	public record UpdateProductCommand(Guid Id,
									   string Name,
									   List<string> Category,
									   string Description,
									   string ImageFile,
									   decimal Price)
					: ICommand<UpdateProductResult>;

	public record UpdateProductResult(bool IsSuccess);

	public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
	{
        public UpdateProductCommandValidator()
        {
			RuleFor(c => c.Id).NotEmpty().WithMessage("Id Is Required");
			RuleFor(c => c.Name).NotEmpty()
				.WithMessage("Name Id Required")
				.Length(2, 150).WithMessage("Length Should Be in 2 , 150");

			RuleFor(c => c.Price).GreaterThan(0).WithMessage("Price should greater than 0");
        }
    }
	public class UpdateProductHandler(IDocumentSession session)
			   : ICommandHandler<UpdateProductCommand, UpdateProductResult>
	{
		public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
		{
			var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
			if(product is null)
			{
				throw new ProductNotFoundException(command.Id);
			}
			product.Name = command.Name;
			product.Category = command.Category;
			product.Description = command.Description;
			product.ImageFile = command.ImageFile;
			product.Price = command.Price;

			session.Update(product);

			await session.SaveChangesAsync(cancellationToken);
			return new UpdateProductResult(true);

		}
	}
}
