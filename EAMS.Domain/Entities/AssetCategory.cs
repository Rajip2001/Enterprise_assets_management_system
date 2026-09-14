using System;
using System.Collections.Generic;
using System.Text;
using EAMS.Domain.Common;

namespace EAMS.Domain.Entities;

public class AssetCategory : BaseSoftDeleteEntity
{
    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }

    // Navigation Property
    public ICollection<Asset> Assets { get; set; } = new List<Asset>();
}
