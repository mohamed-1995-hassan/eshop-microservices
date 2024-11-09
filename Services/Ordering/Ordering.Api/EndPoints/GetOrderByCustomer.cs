using Carter;
using Mapster;
using MediatR;
using Ordering.Application.Dtos;
using Ordering.Application.Orders.Queries.GetOrdersByCustomer;

namespace Ordering.Api.EndPoints
{
	public record GetOrderByCustomerResponse(IEnumerable<OrderDto> Orders);
	public class GetOrderByCustomer : ICarterModule
	{
		public void AddRoutes(IEndpointRouteBuilder app)
		{
			app.MapGet("/order/customer/{customerId}", async (Guid customerId, ISender sender) =>
			{
				var result = await sender.Send(new GetOrdersByCustomerQuery(customerId));
				var response = result.Adapt<GetOrderByCustomerResponse>();
				return Results.Ok(response);
			})
				.WithName("GetOrdersByCustomer")
				.Produces<CreateOrderResponse>(StatusCodes.Status200OK)
				.ProducesProblem(StatusCodes.Status400BadRequest)
				.ProducesProblem(StatusCodes.Status404NotFound)
				.WithSummary("Get Orders By Customer")
				.WithDescription("Get Orders By Customer"); ;
		}
	}
}
