using Evently.Modules.Events.Application.Events.GetEvent;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Events.Presentation.Events;

public static class GetEvent
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("events/{id}", async(Guid id, ISender sender) => 
        {
            var query = new GetEventQuery(id);

            var response = await sender.Send(query);

            return response is null ? Results.NotFound() : Results.Ok(response);

        }).WithTags(Tags.Events);
    }
}
