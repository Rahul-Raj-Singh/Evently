using Evently.Modules.Events.Api.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace Evently.Modules.Events.Api.Events;

public static class GetEvent
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("events/{id}", async(Guid id, EventsDbContext dbContext) => 
        {
            var eventResponse = await dbContext.Events
            .Where(x => x.Id == id)
            .Select(x => new EventResponse 
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Title,
                Location = x.Title,
                StartsAtUtc = x.StartsAtUtc,
                EndsAtUtc = x.EndsAtUtc
            })
            .SingleOrDefaultAsync();

            return eventResponse is null ? Results.NotFound() : Results.Ok(eventResponse);

        }).WithTags(Tags.Events);
    }
}

public class EventResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Location { get; set; }
    public DateTime StartsAtUtc { get; set; }
    public DateTime? EndsAtUtc { get; set; }
}