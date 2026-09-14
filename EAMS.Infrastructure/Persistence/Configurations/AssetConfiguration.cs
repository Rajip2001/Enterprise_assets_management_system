using System;
using System.Collections.Generic;
using System.Text;
using EAMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EAMS.Infrastructure.Persistence.Configurations;

public class AssetConfiguration : IEntityTypeConfiguration<Asset>
{
    public void Configure(EntityTypeBuilder<Asset> builder)
    {
        builder.ToTable("Assets");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
               .IsRequired()
               .HasMaxLength(150);

        builder.Property(x => x.AssetCode)
               .IsRequired()
               .HasMaxLength(50);

        builder.HasIndex(x => x.AssetCode)
               .IsUnique();

        builder.Property(x => x.SerialNumber)
               .HasMaxLength(100);

        builder.Property(x => x.PurchasePrice)
               .HasColumnType("decimal(18,2)");

        builder.HasOne(x => x.AssetCategory)
               .WithMany(x => x.Assets)
               .HasForeignKey(x => x.AssetCategoryId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Supplier)
               .WithMany(x => x.Assets)
               .HasForeignKey(x => x.SupplierId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}
