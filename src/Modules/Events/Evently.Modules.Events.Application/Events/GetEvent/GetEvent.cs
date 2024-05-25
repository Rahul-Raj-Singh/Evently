using Dapper;
using Evently.Modules.Events.Application.Abstractions.Data;
using MediatR;

namespace Evently.Modules.Events.Application.Events.GetEvent;
public sealed class GetEventQueryHandler(IDbConnectionFactory connectionFactory) 
: IRequestHandler<GetEventQuery, EventResponse?>
{
    public async Task<EventResponse?> Handle(GetEventQuery request, CancellationToken cancellationToken)
    {
        using var connection = await connectionFactory.OpenConnectionAsync();
        const string sql =
        $"""
        SELECT
            e.id AS {nameof(EventResponse.Id)},
            e.title AS {nameof(EventResponse.Title)},
            e.description AS {nameof(EventResponse.Description)},
            e.location AS {nameof(EventResponse.Location)},
            e.starts_at_utc AS {nameof(EventResponse.StartsAtUtc)},
            e.ends_at_utc AS {nameof(EventResponse.EndsAtUtc)}
        FROM events.events e
        WHERE e.id = @EventId
        """;

        var eventResponse = await connection.QuerySingleOrDefaultAsync<EventResponse?>(sql, request);

        return eventResponse;
    }
}
