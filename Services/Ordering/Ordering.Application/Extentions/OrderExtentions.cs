
using Ordering.Application.Dtos;
using Ordering.Domain.Models;
using Ordering.Domain.ValueObjects;

namespace Ordering.Application.Extentions;

public static class OrderExtentions
{
	public static IEnumerable<OrderDto> ToOrderDtoList(this IEnumerable<Order> orders)
	{
		return orders.Select(order => new OrderDto
		(
		        Id: order.Id.Value,
				CustomerId: order.CoustomerId.Value,
				OrderName: order.OrderName.Value,
				ShippingAddress: new AddressDto(order.ShippingAddress.FirstName,
		                                        order.ShippingAddress.LastName,
												order.ShippingAddress.EmailAddress!,
												order.ShippingAddress.AddressLine,
												order.ShippingAddress.Country,
												order.ShippingAddress.State,
												order.ShippingAddress.ZipCode),
				BillingAddress: new AddressDto( order.BillingAddress.FirstName,
												order.BillingAddress.LastName,
												order.BillingAddress.EmailAddress!,
												order.BillingAddress.AddressLine,
												order.BillingAddress.Country,
												order.BillingAddress.State,
												order.BillingAddress.ZipCode),
				Payment: new PaymentDto(order.Payment.CartName!,
										 order.Payment.CartNumber,
										 order.Payment.Expiration,
		                                 order.Payment.CVV,
										 order.Payment.PaymentMethod),
				Status: order.Status,
				OrderItems: order.OrderItems.Select(oi => new OrderItemDto(oi.OrderId.Value,
																		   oi.ProductId.Value,
																		   oi.Quantity,
																		   oi.Price)).ToList()
			));
	}

	public static OrderDto ToOrderDto(this Order order)
	{
		return DtoFromOrder(order);
	}

	private static OrderDto DtoFromOrder(this Order order)
	{
		return new OrderDto(
					Id: order.Id.Value,
					CustomerId: order.CoustomerId.Value,
					OrderName: order.OrderName.Value,
					ShippingAddress: new AddressDto(order.ShippingAddress.FirstName, order.ShippingAddress.LastName, order.ShippingAddress.EmailAddress!, order.ShippingAddress.AddressLine, order.ShippingAddress.Country, order.ShippingAddress.State, order.ShippingAddress.ZipCode),
					BillingAddress: new AddressDto(order.BillingAddress.FirstName, order.BillingAddress.LastName, order.BillingAddress.EmailAddress!, order.BillingAddress.AddressLine, order.BillingAddress.Country, order.BillingAddress.State, order.BillingAddress.ZipCode),
					Payment: new PaymentDto(order.Payment.CartName!, order.Payment.CartName, order.Payment.Expiration, order.Payment.CVV, order.Payment.PaymentMethod),
					Status: order.Status,
					OrderItems: order.OrderItems.Select(oi => new OrderItemDto(oi.OrderId.Value, oi.ProductId.Value, oi.Quantity, oi.Price)).ToList()
				);
	}
}
