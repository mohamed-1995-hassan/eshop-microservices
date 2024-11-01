using Catalog.Api.Models;
using Marten;
using Marten.Schema;

namespace Catalog.Api.Data
{
	public class CatalogIntialData : IInitialData
	{
		public async Task Populate(IDocumentStore store, CancellationToken cancellation)
		{
			using var session = store.LightweightSession();
			if(await session.Query<Product>().AnyAsync())
			{
				return;
			}
			session.Store<Product>(GetPreConfiguredProducts());
			await session.SaveChangesAsync();
		}
		private static IEnumerable<Product> GetPreConfiguredProducts() => new List<Product> 
		{
			new Product
			{
				Id = new Guid("e3a67458-fa31-4aee-93e1-e4dba23bbd27"),
				Name = "BMW",
				Description="First Car In This Generation",
				Price = 950.00M,
				Category = new List<string>
				{
					"Smart Phone"
				},
				ImageFile = "Product-1.png"
			},
			new Product
			{
				Id = new Guid("aabeb0b8-cdad-41cf-9a69-1c311a07d4bf"),
				Name = "Samsung 10",
				Description="First Samsung in the market",
				Price = 800.00M,
				Category = new List<string>
				{
					"Carleto"
				},
				ImageFile = "Product-10.png"
			},
		};
	}
}
