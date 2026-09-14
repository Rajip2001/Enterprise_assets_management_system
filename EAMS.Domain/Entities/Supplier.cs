using System;
using System.Collections.Generic;
using System.Text;
using EAMS.Domain.Common;

namespace EAMS.Domain.Entities;

public class Supplier : BaseSoftDeleteEntity
{
    public string Name { get; set; } = string.Empty;

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public ICollection<Asset> Assets { get; set; } = new List<Asset>();
}
