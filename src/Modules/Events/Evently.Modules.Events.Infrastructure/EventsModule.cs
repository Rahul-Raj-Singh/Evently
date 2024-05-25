using Evently.Modules.Events.Application.Abstractions.Data;
using Evently.Modules.Events.Domain.Events;
using Evently.Modules.Events.Infrastructure.Database;
using Evently.Modules.Events.Infrastructure.Events;
using Evently.Modules.Events.Presentation;
using FluentValidation;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;

namespace Evently.Modules.Events.Infrastructure;

public static class EventsModule
{
    public static void MapEndpoints(IEndpointRouteBuilder  app)
    {
        EventEndpoints.MapEventsEndpoints(app);
    }
    
    public static IServiceCollection AddEventsModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatR(config => 
        {
            config.RegisterServicesFromAssembly(Application.AssemblyReference.Assembly);
        });

        services.AddValidatorsFromAssembly(Application.AssemblyReference.Assembly, includeInternalTypes: true);

        var dbConnnectionString = configuration.GetConnectionString("Database");

        var npgSqlDataSource = new NpgsqlDataSourceBuilder(dbConnnectionString).Build();
        services.TryAddSingleton(npgSqlDataSource);

        services.AddDbContext<EventsDbContext>(options => options
            .UseNpgsql(dbConnnectionString)
            .UseSnakeCaseNamingConvention());

        services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<EventsDbContext>());

        return services;
    }
}
