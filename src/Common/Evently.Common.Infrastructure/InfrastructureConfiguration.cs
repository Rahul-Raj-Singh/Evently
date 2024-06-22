using Evently.Common.Application.Caching;
using Evently.Common.Application.Clock;
using Evently.Common.Application.Data;
using Evently.Common.Infrastructure.Caching;
using Evently.Common.Infrastructure.Clock;
using Evently.Common.Infrastructure.Data;
using Evently.Common.Infrastructure.Interceptors;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Npgsql;
using StackExchange.Redis;

namespace Evently.Common.Infrastructure;

public static class InfrastructureConfiguration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, 
        string dbConnnectionString, string redisConnectionString)
    {
        // db
        var npgSqlDataSource = new NpgsqlDataSourceBuilder(dbConnnectionString).Build();
        services.TryAddSingleton(npgSqlDataSource);
        services.AddScoped<IDbConnectionFactory, DbConnectionFactory>();

        services.TryAddSingleton<PublishDomainEventsInterceptor>();
        services.TryAddSingleton<IDateTimeProvider, DateTimeProvider>();


        // cache    
        services.TryAddSingleton<ICacheService, CacheService>();

        IConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect(redisConnectionString);
        services.AddStackExchangeRedisCache(options => 
            options.ConnectionMultiplexerFactory = () => Task.FromResult(connectionMultiplexer));

        return services;
    }
}
