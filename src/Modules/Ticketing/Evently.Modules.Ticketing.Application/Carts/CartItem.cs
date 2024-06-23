namespace Evently.Modules.Ticketing.Application.Carts;

public class CartItem
{
    public Guid TicketTypeId { get; set;}
    public decimal Price { get; set;}
    public int Quantity { get; set;}
    public string Currency { get; set;}
}