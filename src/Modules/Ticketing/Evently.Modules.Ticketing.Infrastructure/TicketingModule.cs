using Evently.Common.Infrastructure.Interceptors;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Evently.Modules.Ticketing.Infrastructure;

public static class TicketingModule
{
    public static void MapEndpoints(IEndpointRouteBuilder  app)
    {
        throw new NotImplementedException();
    }

    
    public static IServiceCollection AddTicketingModule(this IServiceCollection services, IConfiguration configuration)
    {
        // var dbConnnectionString = configuration.GetConnectionString("Database");

        // services.AddDbContext<EventsDbContext>((sp, options) => options
        //     .UseNpgsql(dbConnnectionString)
        //     .UseSnakeCaseNamingConvention()
        //     .AddInterceptors(sp.GetRequiredService<PublishDomainEventsInterceptor>()));

        // services.AddScoped<IEventRepository, EventRepository>();
        // services.AddScoped<ITicketTypeRepository, TicketTypeRepository>();
        // services.AddScoped<ICategoryRepository, CategoryRepository>();
        // services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<EventsDbContext>());

        return services;
    }
}
