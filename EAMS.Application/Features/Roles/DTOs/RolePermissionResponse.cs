namespace EAMS.Application.Features.Roles.DTOs;

public class RolePermissionResponse
{
    public Guid RoleId { get; set; }

    public string RoleName { get; set; } = string.Empty;

    public Guid PermissionId { get; set; }

    public string PermissionName { get; set; } = string.Empty;

    public string? PermissionDescription { get; set; }
}
