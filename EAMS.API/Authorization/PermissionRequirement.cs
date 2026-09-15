using Microsoft.AspNetCore.Authorization;

namespace EAMS.API.Authorization;

public sealed record PermissionRequirement(string Permission)
    : IAuthorizationRequirement;
