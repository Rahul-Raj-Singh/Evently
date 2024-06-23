namespace Evently.Modules.Ticketing.Application.Carts;

public class Cart
{
    public Guid CustomerId { get; init; }
    public List<CartItem> CartItems { get; init; } = [];
    public static Cart CreateDefault(Guid customerId) => new() { CustomerId = customerId };
}
