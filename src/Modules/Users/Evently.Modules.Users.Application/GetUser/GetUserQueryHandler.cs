using Dapper;
using Evently.Common.Application.Data;
using Evently.Common.Domain;
using Evently.Modules.Users.Domain.Users;
using MediatR;

namespace Evently.Modules.Users.Application.GetUser;

public class GetUserQueryHandler(IDbConnectionFactory connectionFactory) 
: IRequestHandler<GetUserQuery, Result<UserResponse>>
{
    public async Task<Result<UserResponse>> Handle(
        GetUserQuery request, CancellationToken cancellationToken)
    {
        using var connection = await connectionFactory.OpenConnectionAsync();

        const string sql =
        $"""
            SELECT
                id AS {nameof(UserResponse.Id)},
                email AS {nameof(UserResponse.Email)},
                first_name AS {nameof(UserResponse.FirstName)},
                last_name AS {nameof(UserResponse.LastName)}
            FROM users.users
            WHERE id = @UserId
        """;

        var user = await connection.QuerySingleOrDefaultAsync<UserResponse>(sql, request);

        if (user is null)
        {
            return Result.Failure<UserResponse>(UserErrors.NotFound(request.UserId));
        }

        return Result.Success(user);
    }
}