using System;
using System.Security.Claims;
using Evently.Common.Application.Authorization;
using Evently.Common.Application.Exceptions;
using Evently.Common.Infrastructure.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace Evently.Common.Infrastructure.Authorization;

public class CustomClaimsTransformation(IServiceScopeFactory serviceScopeFactory) : IClaimsTransformation
{
    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        // skip if custom claim is already added
        if (principal.HasClaim(x => x.Type == CustomClaims.Sub))
        {
            return principal;
        }

        // fetch user permissions from db
        // add custom claims -> {sub: userId}, {permission: abc}, {permission: xyz} ... etc
        using var scope = serviceScopeFactory.CreateScope();

        var identityId = principal.GetIdentityId();

        var premissionsService = scope.ServiceProvider.GetRequiredService<IPermissionService>();

        var result = await premissionsService.GetUserPermissionsAsync(identityId);

        if (!result.IsSuccess)
        {
            throw new EventlyException(nameof(IPermissionService.GetUserPermissionsAsync), result.Error);
        }

        var claimsIdentity = new ClaimsIdentity();

        claimsIdentity.AddClaim(new Claim(CustomClaims.Sub, result.Value.UserId.ToString()));
        
        foreach(var permission in result.Value.Permissions)
        {
            claimsIdentity.AddClaim(new Claim(CustomClaims.Permission, permission));
        }

        principal.AddIdentity(claimsIdentity);

        return principal;
    }
}
