using System;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Evently.Common.Infrastructure.Authorization;

public static class AuthorizationExtensions
{
    public static IServiceCollection AddAuthorizationInternal(this IServiceCollection services)
    {
        services.AddTransient<IClaimsTransformation, CustomClaimsTransformation>();

        services.AddTransient<IAuthorizationPolicyProvider, PermissionAuthorizationPolicyProvider>();

        services.AddTransient<IAuthorizationHandler, PermissionAuthorizationHandler>();

        return services;
    }
}
