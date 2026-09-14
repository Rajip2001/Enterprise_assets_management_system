using System;
using System.Collections.Generic;
using System.Text;
using EAMS.Domain.Common;
using EAMS.Domain.Enums;

namespace EAMS.Domain.Entities;

public class Asset : BaseSoftDeleteEntity
{
    public string Name { get; set; } = string.Empty;

    public string AssetCode { get; set; } = string.Empty;

    public string SerialNumber { get; set; } = string.Empty;

    public decimal PurchasePrice { get; set; }

    public DateTime PurchaseDate { get; set; }

    public DateTime? WarrantyExpiryDate { get; set; }

    public AssetStatus Status { get; set; }

    public AssetCondition Condition { get; set; }

    // Foreign Keys
    public Guid AssetCategoryId { get; set; }

    public Guid SupplierId { get; set; }

    // Navigation Properties
    public AssetCategory AssetCategory { get; set; } = null!;

    public Supplier Supplier { get; set; } = null!;
}
