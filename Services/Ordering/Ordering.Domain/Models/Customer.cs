

namespace Ordering.Domain.Models;

public class Customer : Entity<CoustomerId>
{
    public string Name { get; private set; } = default;
    public string Email { get; private set; } = default;

    public static Customer Create(CoustomerId coustomerId, string name, string email)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(email);

        var customer = new Customer
        {
            Id = coustomerId,
            Name = name,
            Email = email
        };
        return customer;
    }
}
