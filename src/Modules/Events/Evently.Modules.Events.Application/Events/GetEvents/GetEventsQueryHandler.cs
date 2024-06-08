using System.Data.Common;
using Dapper;
using Evently.Common.Application.Data;
using Evently.Common.Domain;
using MediatR;

namespace Evently.Modules.Events.Application.Events.GetEvents;

public sealed class GetEventsQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IRequestHandler<GetEventsQuery, Result<List<EventResponse>>>
{
    public async Task<Result<List<EventResponse>>> Handle(
        GetEventsQuery request,
        CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT
                 id AS {nameof(EventResponse.Id)},
                 category_id AS {nameof(EventResponse.CategoryId)},
                 title AS {nameof(EventResponse.Title)},
                 description AS {nameof(EventResponse.Description)},
                 location AS {nameof(EventResponse.Location)},
                 starts_at_utc AS {nameof(EventResponse.StartsAtUtc)},
                 ends_at_utc AS {nameof(EventResponse.EndsAtUtc)}
             FROM events.events
             """;

        List<EventResponse> events = (await connection.QueryAsync<EventResponse>(sql, request)).AsList();

        return Result.Success(events);
    }
}
