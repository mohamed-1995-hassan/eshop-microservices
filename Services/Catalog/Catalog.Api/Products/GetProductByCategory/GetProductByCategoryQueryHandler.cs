using BuildingBlocks.CQRS;
using Catalog.Api.Models;
using Catalog.Api.Products.GetProductById;
using Marten;

namespace Catalog.Api.Products.GetProductByCategory
{
	public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;
	public record GetProductByCategoryResult(IEnumerable<Product> Products);
	internal class GetProductByCategoryQueryHandler
		(IDocumentSession session, ILogger<GetProductByIdQueryHandler> logger)
		: IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
	{
		public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
		{
			logger.LogInformation("GetProductByIdQueryHandler.Handle called with {@Query}", query);
			var products = await session.Query<Product>()
										.Where(p => p.Category.Contains(query.Category))
										.ToListAsync(cancellationToken);

			return new GetProductByCategoryResult(products);
		}
	}
}
