﻿using Basket.Api.Exceptions;
using Basket.Api.Models;
using Marten;

namespace Basket.Api.Data
{
	public class BasketRepository(IDocumentSession session) : IBasketRepository
	{
		public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
		{
			session.Delete<ShoppingCart>(userName);
			await session.SaveChangesAsync(cancellationToken);
			return true;
		}

		public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
		{
			var basket = await session.LoadAsync<ShoppingCart>(userName, cancellationToken);
			return basket ?? throw new BasketNotFoundException(userName);
		}

		public async Task<ShoppingCart> StoreBasket(ShoppingCart shoppingCart, CancellationToken cancellationToken = default)
		{
			session.Store(shoppingCart);
			await session.SaveChangesAsync(cancellationToken);
			return shoppingCart;
		}
	}
}
