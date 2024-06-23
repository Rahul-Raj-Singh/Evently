using Evently.Common.Application.Messaging;

namespace Evently.Modules.Ticketing.Application.Carts.AddItemToCart;

public sealed record AddItemToCartCommand(Guid CustomerId, Guid TicketTypeId, int Quantity) : ICommand;
