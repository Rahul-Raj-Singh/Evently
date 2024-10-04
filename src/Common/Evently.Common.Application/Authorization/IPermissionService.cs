using System;
using Evently.Common.Domain;

namespace Evently.Common.Application.Authorization;

public interface IPermissionService
{
    Task<Result<PermissionsResponse>> GetUserPermissionsAsync(string identityId);
}

public sealed record PermissionsResponse(Guid UserId, HashSet<string> Permissions);