
using Ordering.Domain.Exceptions;

namespace Ordering.Domain.ValueObjects
{
	public record OrderItemId
	{
		public Guid Value { get; set; }
		private OrderItemId(Guid value) => Value = value;
		public static OrderItemId Of(Guid value)
		{
			ArgumentNullException.ThrowIfNull(value);
			if (value == Guid.Empty)
			{
				throw new DomainException("customer Id must not be empty");
			}
			return new OrderItemId(value);
		}
	}
}
