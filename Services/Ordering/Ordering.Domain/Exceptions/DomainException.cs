
namespace Ordering.Domain.Exceptions
{
	public class DomainException : Exception
	{
		public DomainException(string message) 
			: base($"domain exception {message} throws from domain")
		{ }
	}
}
