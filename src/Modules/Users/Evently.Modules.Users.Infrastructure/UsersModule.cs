using Evently.Common.Infrastructure.Interceptors;
using Evently.Modules.Users.Application.Abstractions.Data;
using Evently.Modules.Users.Domain.Users;
using Evently.Modules.Users.Infrastructure.Database;
using Evently.Modules.Users.Infrastructure.Users;
using EventlyModules.Users.Presentation.Users;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Evently.Modules.Users.Infrastructure;

public static class UsersModule
{
    public static void MapEndpoints(IEndpointRouteBuilder  app)
    {
        UsersEndpoints.MapEndpoints(app);
    }
    
    public static IServiceCollection AddUsersModule(this IServiceCollection services, IConfiguration configuration)
    {
        var dbConnnectionString = configuration.GetConnectionString("Database");

        services.AddDbContext<UsersDbContext>((sp, options) => options
            .UseNpgsql(dbConnnectionString)
            .UseSnakeCaseNamingConvention()
            .AddInterceptors(sp.GetRequiredService<PublishDomainEventsInterceptor>()));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<UsersDbContext>());

        return services;
    }
}
