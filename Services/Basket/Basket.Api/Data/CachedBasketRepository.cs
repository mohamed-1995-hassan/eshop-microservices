using Basket.Api.Models;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.Api.Data
{
	public class CachedBasketRepository(IBasketRepository basketRepository,
										IDistributedCache cache)
		: IBasketRepository
	{
		public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
		{
			await basketRepository.DeleteBasket(userName, cancellationToken);
			await cache.RemoveAsync(userName, cancellationToken);
			return true;
		}

		public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
		{
			var cachedBasket = await cache.GetStringAsync(userName, cancellationToken);

			if(!string.IsNullOrEmpty(cachedBasket))
				return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket)!;

			var basket = await basketRepository.GetBasket(userName, cancellationToken);

			await cache.SetStringAsync(userName, JsonSerializer.Serialize(basket), cancellationToken);

			return basket;
		}

		public async Task<ShoppingCart> StoreBasket(ShoppingCart shoppingCart, CancellationToken cancellationToken = default)
		{
			var basket = await basketRepository.StoreBasket(shoppingCart, cancellationToken);
			await cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket), cancellationToken);
			return basket;

		}
	}
}
