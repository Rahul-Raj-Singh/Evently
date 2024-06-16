using Evently.Common.Application.Clock;
using Evently.Common.Application.Data;
using Evently.Common.Infrastructure.Clock;
using Evently.Common.Infrastructure.Data;
using Evently.Common.Infrastructure.Interceptors;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;

namespace Evently.Common.Infrastructure;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string dbConnnectionString)
    {
        
        var npgSqlDataSource = new NpgsqlDataSourceBuilder(dbConnnectionString).Build();
        services.TryAddSingleton(npgSqlDataSource);

        services.TryAddSingleton<PublishDomainEventsInterceptor>();
        services.TryAddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();

        return services;
    }
}
