﻿
using BuildingBlocks.CQRS;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Data;
using Ordering.Application.Extentions;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer;

public class GetOrdersByCustomerHandler(IApplicationDbContext dbContext)
	: IQueryHandler<GetOrdersByCustomerQuery, GetOrdersByCustomerResult>
{
	public async Task<GetOrdersByCustomerResult> Handle(GetOrdersByCustomerQuery query, CancellationToken cancellationToken)
	{
		var orders = await dbContext.Orders
						.Include(o => o.OrderItems)
						.AsNoTracking()
						.Where(o => o.CoustomerId == CoustomerId.Of(query.CustomerId))
						.OrderBy(o => o.OrderName.Value)
						.ToListAsync(cancellationToken);

		return new GetOrdersByCustomerResult(orders.ToOrderDtoList());
	}
}
