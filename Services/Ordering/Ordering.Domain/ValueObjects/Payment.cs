
namespace Ordering.Domain.ValueObjects
{
	public record Payment
	{
        public string? CartName { get; } = default;
        public string CartNumber { get; } = default;
        public string Expiration { get; } = default;
        public string CVV { get; } = default;
        public int PaymentMethod { get; } = default;

        protected Payment()
        {

        }

		private Payment(string cartName, string cartNumber, string expiration, string cVV, int paymentMethod)
		{
			CartName = cartName;
			CartNumber = cartNumber;
			Expiration = expiration;
			CVV = cVV;
			PaymentMethod = paymentMethod;
		}

		public static Payment Of(string cartName, string cartNumber, string expiration, string cVV, int paymentMethod)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(cartName);
			ArgumentException.ThrowIfNullOrWhiteSpace(cartNumber);
			ArgumentException.ThrowIfNullOrWhiteSpace(cVV);
			ArgumentOutOfRangeException.ThrowIfNotEqual(cVV.Length, 3);

			return new Payment(cartName, cartNumber, expiration, cVV, paymentMethod);
		}
	} 
}
