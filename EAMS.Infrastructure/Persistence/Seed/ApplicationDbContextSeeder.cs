using System;
using System.Collections.Generic;
using System.Text;

using EAMS.Domain.Common;
using EAMS.Domain.Entities;
using EAMS.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace EAMS.Infrastructure.Persistence.Seed;

public static class ApplicationDbContextSeeder
{
    public static async Task SeedAsync(
        ApplicationDbContext context,
        CancellationToken cancellationToken = default)
    {
        await SeedRolesAsync(context, cancellationToken);
        await SeedPermissionsAsync(context, cancellationToken);
        await SeedRolePermissionsAsync(context, cancellationToken);
    }


    private static async Task SeedRolesAsync(
        ApplicationDbContext context,
        CancellationToken cancellationToken)
    {
        var roles = new[]
        {
            new Role
            {
                Id = Guid.NewGuid(),
                Name = Roles.SuperAdmin,
                Description = "Full system access"
            },
            new Role
            {
                Id = Guid.NewGuid(),
                Name = Roles.Admin,
                Description = "Administrative access"
            },
            new Role
            {
                Id = Guid.NewGuid(),
                Name = Roles.AssetManager,
                Description = "Asset management access"
            },
            new Role
            {
                Id = Guid.NewGuid(),
                Name = Roles.Employee,
                Description = "Basic employee access"
            }
        };

        foreach (var role in roles)
        {
            var exists = await context.Roles
                .AnyAsync(
                    x => x.Name == role.Name,
                    cancellationToken);

            if (!exists)
            {
                await context.Roles.AddAsync(
                    role,
                    cancellationToken);
            }
        }

        await context.SaveChangesAsync(cancellationToken);
    }


    private static async Task SeedPermissionsAsync(
        ApplicationDbContext context,
        CancellationToken cancellationToken)
    {
        var permissions = new[]
        {
            new Permission
            {
                Id = Guid.NewGuid(),
                Name = Permissions.AssetCreate,
                Description = "Create assets"
            },
            new Permission
            {
                Id = Guid.NewGuid(),
                Name = Permissions.AssetRead,
                Description = "View assets"
            },
            new Permission
            {
                Id = Guid.NewGuid(),
                Name = Permissions.AssetUpdate,
                Description = "Update assets"
            },
            new Permission
            {
                Id = Guid.NewGuid(),
                Name = Permissions.AssetDelete,
                Description = "Delete assets"
            },


            new Permission
            {
                Id = Guid.NewGuid(),
                Name = Permissions.UserCreate,
                Description = "Create users"
            },
            new Permission
            {
                Id = Guid.NewGuid(),
                Name = Permissions.UserRead,
                Description = "View users"
            },
            new Permission
            {
                Id = Guid.NewGuid(),
                Name = Permissions.UserUpdate,
                Description = "Update users"
            },
            new Permission
            {
                Id = Guid.NewGuid(),
                Name = Permissions.UserDelete,
                Description = "Delete users"
            },


            new Permission
            {
                Id = Guid.NewGuid(),
                Name = Permissions.RoleCreate,
                Description = "Create roles"
            },
            new Permission
            {
                Id = Guid.NewGuid(),
                Name = Permissions.RoleRead,
                Description = "View roles"
            },
            new Permission
            {
                Id = Guid.NewGuid(),
                Name = Permissions.RoleUpdate,
                Description = "Update roles"
            },
            new Permission
            {
                Id = Guid.NewGuid(),
                Name = Permissions.RoleDelete,
                Description = "Delete roles"
            },


            new Permission
            {
                Id = Guid.NewGuid(),
                Name = Permissions.DepartmentCreate,
                Description = "Create departments"
            },
            new Permission
            {
                Id = Guid.NewGuid(),
                Name = Permissions.DepartmentRead,
                Description = "View departments"
            },
            new Permission
            {
                Id = Guid.NewGuid(),
                Name = Permissions.DepartmentUpdate,
                Description = "Update departments"
            },
            new Permission
            {
                Id = Guid.NewGuid(),
                Name = Permissions.DepartmentDelete,
                Description = "Delete departments"
            },


            new Permission
            {
                Id = Guid.NewGuid(),
                Name = Permissions.SupplierCreate,
                Description = "Create suppliers"
            },
            new Permission
            {
                Id = Guid.NewGuid(),
                Name = Permissions.SupplierRead,
                Description = "View suppliers"
            },
            new Permission
            {
                Id = Guid.NewGuid(),
                Name = Permissions.SupplierUpdate,
                Description = "Update suppliers"
            },
            new Permission
            {
                Id = Guid.NewGuid(),
                Name = Permissions.SupplierDelete,
                Description = "Delete suppliers"
            }
        };

        foreach (var permission in permissions)
        {
            var exists = await context.Permissions
                .AnyAsync(
                    x => x.Name == permission.Name,
                    cancellationToken);

            if (!exists)
            {
                await context.Permissions.AddAsync(
                    permission,
                    cancellationToken);
            }
        }

        await context.SaveChangesAsync(cancellationToken);
    }


    private static async Task SeedRolePermissionsAsync(
        ApplicationDbContext context,
        CancellationToken cancellationToken)
    {
        var superAdminRole = await context.Roles
            .FirstAsync(
                x => x.Name == Roles.SuperAdmin,
                cancellationToken);

        var adminRole = await context.Roles
            .FirstAsync(
                x => x.Name == Roles.Admin,
                cancellationToken);

        var assetManagerRole = await context.Roles
            .FirstAsync(
                x => x.Name == Roles.AssetManager,
                cancellationToken);

        var employeeRole = await context.Roles
            .FirstAsync(
                x => x.Name == Roles.Employee,
                cancellationToken);


        var permissions = await context.Permissions
            .ToListAsync(cancellationToken);


        // SuperAdmin gets ALL permissions
        foreach (var permission in permissions)
        {
            await AddRolePermissionIfNotExists(
                context,
                superAdminRole.Id,
                permission.Id,
                cancellationToken);
        }


        // Admin gets most permissions except Role management
        var adminPermissions = new[]
        {
            Permissions.AssetCreate,
            Permissions.AssetRead,
            Permissions.AssetUpdate,
            Permissions.AssetDelete,

            Permissions.UserCreate,
            Permissions.UserRead,
            Permissions.UserUpdate,
            Permissions.UserDelete,

            Permissions.DepartmentCreate,
            Permissions.DepartmentRead,
            Permissions.DepartmentUpdate,
            Permissions.DepartmentDelete,

            Permissions.SupplierCreate,
            Permissions.SupplierRead,
            Permissions.SupplierUpdate,
            Permissions.SupplierDelete
        };

        foreach (var permissionName in adminPermissions)
        {
            var permission = permissions
                .First(x => x.Name == permissionName);

            await AddRolePermissionIfNotExists(
                context,
                adminRole.Id,
                permission.Id,
                cancellationToken);
        }


        // AssetManager gets Asset permissions
        var assetManagerPermissions = new[]
        {
            Permissions.AssetCreate,
            Permissions.AssetRead,
            Permissions.AssetUpdate,
            Permissions.AssetDelete,

            Permissions.SupplierRead,
            Permissions.DepartmentRead
        };

        foreach (var permissionName in assetManagerPermissions)
        {
            var permission = permissions
                .First(x => x.Name == permissionName);

            await AddRolePermissionIfNotExists(
                context,
                assetManagerRole.Id,
                permission.Id,
                cancellationToken);
        }


        // Employee can only view assets
        var employeePermission = permissions
            .First(x => x.Name == Permissions.AssetRead);

        await AddRolePermissionIfNotExists(
            context,
            employeeRole.Id,
            employeePermission.Id,
            cancellationToken);


        await context.SaveChangesAsync(cancellationToken);
    }


    private static async Task AddRolePermissionIfNotExists(
        ApplicationDbContext context,
        Guid roleId,
        Guid permissionId,
        CancellationToken cancellationToken)
    {
        var exists = await context.RolePermissions.AnyAsync(
            x =>
                x.RoleId == roleId &&
                x.PermissionId == permissionId,
            cancellationToken);

        if (!exists)
        {
            await context.RolePermissions.AddAsync(
                new RolePermission
                {
                    Id = Guid.NewGuid(),
                    RoleId = roleId,
                    PermissionId = permissionId
                },
                cancellationToken);
        }
    }
}
