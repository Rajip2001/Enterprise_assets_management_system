namespace EAMS.Application.Features.Users.DTOs;

public class UserResponse
{
    public Guid Id { get; set; }

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string UserName { get; set; } = string.Empty;

    public string? PhoneNumber { get; set; }

    public bool IsActive { get; set; }

    public Guid RoleId { get; set; }

    public string Role { get; set; } = string.Empty;

    public DateTime? LastLogin { get; set; }
}