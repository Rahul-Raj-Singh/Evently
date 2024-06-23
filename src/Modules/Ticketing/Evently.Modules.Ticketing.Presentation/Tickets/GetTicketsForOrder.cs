using Evently.Common.Domain;
using Evently.Common.Presentation.ApiResults;
using Evently.Modules.Ticketing.Application.Tickets.GetTicket;
using Evently.Modules.Ticketing.Application.Tickets.GetTicketForOrder;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Ticketing.Presentation.Tickets;

internal sealed class GetTicketsForOrder
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("tickets/order/{orderId}", async (Guid orderId, ISender sender) =>
        {
            var result = await sender.Send(
                new GetTicketsForOrderQuery(orderId));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .WithTags(Tags.Tickets);
    }
}
