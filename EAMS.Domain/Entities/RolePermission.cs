using System;
using System.Collections.Generic;
using System.Text;

using EAMS.Domain.Common;

namespace EAMS.Domain.Entities;

public class RolePermission : BaseEntity
{
    public Guid RoleId { get; set; }

    public Guid PermissionId { get; set; }

    public Role Role { get; set; } = null!;

    public Permission Permission { get; set; } = null!;
}