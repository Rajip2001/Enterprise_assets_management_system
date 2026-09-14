using System;
using System.Collections.Generic;
using System.Text;
using EAMS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EAMS.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Asset> Assets { get; }

    DbSet<AssetCategory> AssetCategories { get; }

    DbSet<Department> Departments { get; }

    DbSet<Supplier> Suppliers { get; }

    // Authentication Module
    DbSet<User> Users { get; }

    DbSet<Role> Roles { get; }

    DbSet<Permission> Permissions { get; }

    DbSet<RolePermission> RolePermissions { get; }

    DbSet<RefreshToken> RefreshTokens { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
