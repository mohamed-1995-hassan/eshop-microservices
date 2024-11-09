
using BuildingBlocks.CQRS;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Application.Dtos;
using Ordering.Application.Extentions;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Orders.Queries.GetOrdersByNames;

public class GetOrderByNameHandler(IApplicationDbContext context)
	: IQueryHandler<GetOrderByNameQuery, GetOrderByNameResult>
{
	public async Task<GetOrderByNameResult> Handle(GetOrderByNameQuery query, CancellationToken cancellationToken)
	{
		var orders = await context.Orders
				.Include(o => o.OrderItems)
				.AsNoTracking()
				.Where(o => o.OrderName.Value.Contains(query.Name))
				.OrderBy(o => o.OrderName.Value)
				.ToListAsync(cancellationToken);

		return new GetOrderByNameResult(orders.ToOrderDtoList());
	}
}
