using Evently.Common.Domain;
using Evently.Common.Presentation.ApiResults;
using Evently.Modules.Users.Application.UpdateUser;
using Evently.Modules.Users.Presentation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace EventlyModules.Users.Presentation.Users;

public static class UpdateUser
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("users/{userId}/update", async (Guid userId, Request request, ISender sender) => 
        {
            Result result = await sender.Send(new UpdateUserCommand
            {
                UserId = userId,
                FirstName = request.FirstName,
                LastName = request.LastName,
            });

            return result.Match(Results.NoContent, ApiResults.Problem);
        })
        .WithTags(Tags.Users);
    }

    internal sealed record Request(string FirstName, string LastName);
}
