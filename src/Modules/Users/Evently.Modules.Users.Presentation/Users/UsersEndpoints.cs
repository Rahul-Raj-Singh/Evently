using Microsoft.AspNetCore.Routing;

namespace EventlyModules.Users.Presentation.Users;

public static class UsersEndpoints
{
    public static void MapEndpoints(IEndpointRouteBuilder app)
    {
        GetUser.MapEndpoint(app);
        RegisterUser.MapEndpoint(app);
        UpdateUser.MapEndpoint(app);
    }
}
