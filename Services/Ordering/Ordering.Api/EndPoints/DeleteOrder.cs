using Carter;
using Mapster;
using MediatR;
using Ordering.Application.Orders.DeleteOrder;

namespace Ordering.Api.EndPoints;
public record DeleteOrderResponse(bool IsSuccess);
public class DeleteOrder : ICarterModule
{
	public void AddRoutes(IEndpointRouteBuilder app)
	{
		app.MapDelete("/orders/{Id}", async(Guid Id, ISender sender) =>
		{
			var result = await sender.Send(new DeleteOrderCommand(Id));
			var response = result.Adapt<DeleteOrderResponse>();
			return Results.Ok(response);
		})
			.WithName("DeleteOrder")
			.Produces<CreateOrderResponse>(StatusCodes.Status200OK)
			.ProducesProblem(StatusCodes.Status400BadRequest)
			.ProducesProblem(StatusCodes.Status404NotFound)
			.WithSummary("Delete Order")
			.WithDescription("Delete Order"); ;
	}
}
