
namespace BuildingBlocks.Exceptions
{
	public class InternalServerError : Exception
	{
        public InternalServerError(string message) : base(message)
        {
            
        }

		public InternalServerError(string message, string details) : base(message)
		{
            Details = details;
		}

        public string? Details { get; set; }
    }
}
