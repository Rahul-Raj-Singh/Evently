﻿using Evently.Common.Domain;
using Evently.Common.Presentation.ApiResults;
using Evently.Modules.Events.Application.TicketTypes.UpdateTicketTypePrice;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Events.Presentation.TicketTypes;

internal static class ChangeTicketTypePrice
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("ticket-types/{id}/price", async (Guid id, ChangeTicketTypePriceRequest request, ISender sender) =>
            {
                Result result = await sender.Send(new UpdateTicketTypePriceCommand(id, request.Price));

                return result.Match(Results.NoContent, ApiResults.Problem);
            })
            .WithTags(Tags.TicketTypes);
    }

    internal sealed class ChangeTicketTypePriceRequest
    {
        public decimal Price { get; init; }
    }
}
