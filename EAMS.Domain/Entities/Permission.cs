using System;
using System.Collections.Generic;
using System.Text;
using EAMS.Domain.Common;

namespace EAMS.Domain.Entities;

public class Permission : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public ICollection<RolePermission> RolePermissions { get; set; }
        = new List<RolePermission>();
}
