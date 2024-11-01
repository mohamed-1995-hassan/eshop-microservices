﻿using Basket.Api.Models;
using Carter;
using Mapster;
using MediatR;

namespace Basket.Api.Basket.GetBasket
{
	public record GetBasketResponse(ShoppingCart Cart);
	public class GetBasketEndPoint : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapGet("/basket/{UserName}", async (string userName, ISender sender) =>
			{
				var result = await sender.Send(new GetBasketQuery(userName));
				var response = result.Adapt<GetBasketResponse>();
				return Results.Ok(response);
			})
				.WithName("GetBasketById")
				.Produces<GetBasketResponse>(StatusCodes.Status200OK)
				.ProducesProblem(StatusCodes.Status400BadRequest)
				.WithSummary("Get Basket By Id")
				.WithDescription("Get Basket By Id");
		}
	}
}