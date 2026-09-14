using System;
using System.Collections.Generic;
using System.Text;

using EAMS.Domain.Common;

namespace EAMS.Domain.Entities;

public class User : BaseSoftDeleteEntity
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string UserName { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public string? PhoneNumber { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime? LastLogin { get; set; }

    public Guid RoleId { get; set; }

    public Role Role { get; set; } = null!;

    public string PasswordSalt { get; set; } = string.Empty;

    public int FailedLoginAttempts { get; set; }

    public DateTime? LockoutEnd { get; set; }

    public bool EmailVerified { get; set; }

    public string? EmailVerificationToken { get; set; }

    public string? PasswordResetToken { get; set; }

    public DateTime? PasswordResetTokenExpiry { get; set; }

    public ICollection<RefreshToken> RefreshTokens { get; set; }
        = new List<RefreshToken>();
}
