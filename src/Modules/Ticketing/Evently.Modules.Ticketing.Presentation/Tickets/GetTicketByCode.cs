﻿using Evently.Common.Domain;
using Evently.Common.Presentation.ApiResults;
using Evently.Modules.Ticketing.Application.Tickets.GetTicket;
using Evently.Modules.Ticketing.Application.Tickets.GetTicketByCode;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Ticketing.Presentation.Tickets;

public sealed class GetTicketByCode
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("tickets/code/{code}", async (string code, ISender sender) =>
        {
            Result<TicketResponse> result = await sender.Send(new GetTicketByCodeQuery(code));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .WithTags(Tags.Tickets);
    }
}
