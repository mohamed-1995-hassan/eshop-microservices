﻿using Basket.Api.Models;
using Carter;
using Mapster;
using MediatR;

namespace Basket.Api.Basket.StoreBasket
{
	public record StoreBasketRequest(ShoppingCart Cart);
	public record StoreBasketResponse(string UserName);
	public class StoreBasketEndPoint : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapPost("/basket", async (StoreBasketRequest request, ISender sender) =>
			{
				var command = request.Adapt<StoreBasketCommand>();
				var result = await sender.Send(command);
				var response = result.Adapt<StoreBasketResponse>();
				return Results.Created($"/basket/{response.UserName}", response);
			})
				.WithName("BasketProduct")
				.Produces<StoreBasketResponse>(StatusCodes.Status201Created)
				.ProducesProblem(StatusCodes.Status400BadRequest)
				.WithSummary("Create Basket")
				.WithDescription("Create Basket");
		}
	}
}
