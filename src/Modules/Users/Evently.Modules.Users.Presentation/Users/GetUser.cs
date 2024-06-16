using Evently.Common.Domain;
using Evently.Common.Presentation.ApiResults;
using Evently.Modules.Users.Application.GetUser;
using Evently.Modules.Users.Application.UpdateUser;
using Evently.Modules.Users.Presentation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace EventlyModules.Users.Presentation.Users;

public static class GetUser
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("users/{userId}", async (Guid userId, ISender sender) => 
        {
            Result<UserResponse> result = await sender.Send(new GetUserQuery
            {
                UserId = userId,
            });

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .WithTags(Tags.Users);
    }
}
