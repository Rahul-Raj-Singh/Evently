using Microsoft.Extensions.DependencyInjection;

namespace Evently.Common.Infrastructure.Authentication;

internal static class AuthenticationExtensions
{
    internal static IServiceCollection AddAuthenticationInternal(this IServiceCollection services)
    {
        services.AddAuthorization();

        services.AddAuthentication().AddJwtBearer(options => 
        {
            options.TokenValidationParameters.ValidAudience = "account";
            // need to update when app is running within docker
            options.TokenValidationParameters.ValidIssuers = ["http://localhost:18080/realms/evently"];
            options.MetadataAddress = "http://localhost:18080/realms/evently/.well-known/openid-configuration";
            options.RequireHttpsMetadata = false;
        });

        services.AddHttpContextAccessor();

        return services;
    }
}