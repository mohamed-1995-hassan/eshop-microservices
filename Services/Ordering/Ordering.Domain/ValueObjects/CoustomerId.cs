
using Ordering.Domain.Exceptions;

namespace Ordering.Domain.ValueObjects;

public record CoustomerId
{
    public Guid Value { get; set; }
    private CoustomerId(Guid value) => Value = value;

    public static CoustomerId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if(value == Guid.Empty)
        {
            throw new DomainException("customer Id must not be empty");
        }
        return new CoustomerId(value);
    }
}
