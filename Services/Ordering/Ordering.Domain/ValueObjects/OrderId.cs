﻿
using Ordering.Domain.Exceptions;

namespace Ordering.Domain.ValueObjects;

public record OrderId
{
	public Guid Value { get; set; }
	private OrderId(Guid value) => Value = value;
	public static OrderId Of(Guid value)
	{
		ArgumentNullException.ThrowIfNull(value);
		if (value == Guid.Empty)
		{
			throw new DomainException("customer Id must not be empty");
		}
		return new OrderId(value);
	}
}