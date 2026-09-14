using System;
using System.Collections.Generic;
using System.Text;

namespace EAMS.Domain.Enums;

public enum AssetStatus
{
    Available = 1,
    Assigned = 2,
    Maintenance = 3,
    Retired = 4,
    Lost = 5
}
