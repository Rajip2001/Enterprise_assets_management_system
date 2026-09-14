using System;
using System.Collections.Generic;
using System.Text;

using EAMS.Domain.Common;

namespace EAMS.Domain.Entities;

public class Role : BaseSoftDeleteEntity
{
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    public ICollection<RolePermission> RolePermissions { get; set; }
        = new List<RolePermission>();

    public ICollection<User> Users { get; set; }
        = new List<User>();
}
