using BuildingBlocks.CQRS;
using Catalog.Api.Models;
using Catalog.Api.Products.UpdateProduct;
using FluentValidation;
using Marten;

namespace Catalog.Api.Products.DeleteProduct
{
	public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;
	public record DeleteProductResult(bool IsSuccess);
	public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
	{
		public DeleteProductCommandValidator()
		{
			RuleFor(c => c.Id).NotEmpty().WithMessage("Id Is Required");
		}
	}
	public class DeleteProductHandler(IDocumentSession session)
		: ICommandHandler<DeleteProductCommand, DeleteProductResult>
	{
		public async Task<DeleteProductResult> Handle(DeleteProductCommand command,
												CancellationToken cancellationToken)
		{
			session.Delete<Product>(command.Id);
			await session.SaveChangesAsync();
			return new DeleteProductResult(true);
		}
	}
}
