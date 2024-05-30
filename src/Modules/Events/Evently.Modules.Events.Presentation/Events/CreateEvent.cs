using Evently.Modules.Events.Application.Events.CreateEvent;
using Evently.Modules.Events.Presentation.ApiResults;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Evently.Modules.Events.Presentation.Events;

internal static class CreateEvent
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("events", async (CreateEventRequest request, ISender sender) => 
        {
            var command = new CreateEventCommand(
                request.Title,
                request.Description,
                request.Location,
                request.CategoryId,
                request.StartsAtUtc,
                request.EndsAtUtc
            );

            var result = await sender.Send(command);

            return result.Match(Results.Ok, ApiResults.ApiResults.Problem);

        }).WithTags(Tags.Events);
    }
    internal sealed class CreateEventRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public Guid CategoryId { get; set; }
        public DateTime StartsAtUtc { get; set; }
        public DateTime? EndsAtUtc { get; set; }
    }
}

