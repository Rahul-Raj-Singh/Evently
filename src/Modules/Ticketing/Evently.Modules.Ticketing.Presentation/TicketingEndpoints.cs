using Evently.Modules.Ticketing.Application.Tickets.GetTicketByCode;
using Evently.Modules.Ticketing.Presentation.Carts;
using Evently.Modules.Ticketing.Presentation.Orders;
using Evently.Modules.Ticketing.Presentation.Tickets;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Ticketing.Presentation;

public static class TicketingEndpoints
{
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        AddToCart.MapEndpoint(app);

        GetOrder.MapEndpoint(app);

        GetTicket.MapEndpoint(app);
        GetTicketByCode.MapEndpoint(app);
        GetTicketsForOrder.MapEndpoint(app);
    }
}
