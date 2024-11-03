﻿
using Ordering.Domain.Exceptions;

namespace Ordering.Domain.ValueObjects
{
	public record ProductId
	{
		public Guid Value { get; set; }

		private ProductId(Guid value) => Value = value;

		public static ProductId Of(Guid value)
		{
			ArgumentNullException.ThrowIfNull(value);
			if (value == Guid.Empty)
			{
				throw new DomainException("customer Id must not be empty");
			}
			return new ProductId(value);
		}
	}
}
