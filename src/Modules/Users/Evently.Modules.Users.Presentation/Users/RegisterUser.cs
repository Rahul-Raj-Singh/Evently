using Evently.Common.Domain;
using Evently.Common.Presentation.ApiResults;
using Evently.Modules.Users.Application.RegisterUser;
using Evently.Modules.Users.Presentation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace EventlyModules.Users.Presentation.Users;

public static class RegisterUser
{
    public static void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("users/register", async (Request request, ISender sender) => 
        {
            Result<Guid> result = await sender.Send(new RegisterUserCommand
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email
            });

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .WithTags(Tags.Users)
        .AllowAnonymous();
    }

    internal sealed record Request(string FirstName, string LastName, string Email);
}
