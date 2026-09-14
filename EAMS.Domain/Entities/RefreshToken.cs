using System;
using System.Collections.Generic;
using System.Text;

using EAMS.Domain.Common;

namespace EAMS.Domain.Entities;

public class RefreshToken : BaseEntity
{
    public string Token { get; set; } = string.Empty;

    public DateTime ExpiresAt { get; set; }

    public bool IsRevoked { get; set; }

    public Guid UserId { get; set; }

    public User User { get; set; } = null!;
}
