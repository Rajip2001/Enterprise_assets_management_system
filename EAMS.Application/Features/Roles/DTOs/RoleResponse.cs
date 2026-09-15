namespace EAMS.Application.Features.Roles.DTOs;

public class RoleResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public int UserCount { get; set; }
}
