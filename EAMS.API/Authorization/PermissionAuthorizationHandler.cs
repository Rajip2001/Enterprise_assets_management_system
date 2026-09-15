using System.Security.Claims;
using EAMS.Application.Common.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace EAMS.API.Authorization;

public class PermissionAuthorizationHandler
    : AuthorizationHandler<PermissionRequirement>
{
    private readonly IPermissionService _permissionService;

    public PermissionAuthorizationHandler(
        IPermissionService permissionService)
    {
        _permissionService = permissionService;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        var userIdClaim = context.User.FindFirst(
            ClaimTypes.NameIdentifier);

        if (userIdClaim is null)
            return;

        if (!Guid.TryParse(userIdClaim.Value, out var userId))
            return;

        var hasPermission =
            await _permissionService.HasPermissionAsync(
                userId,
                requirement.Permission);

        if (hasPermission)
        {
            context.Succeed(requirement);
        }
    }
}