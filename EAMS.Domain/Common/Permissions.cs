using System;
using System.Collections.Generic;
using System.Text;

namespace EAMS.Domain.Common;

public static class Permissions
{
    // Asset Permissions
    public const string AssetCreate = "Asset.Create";
    public const string AssetRead = "Asset.Read";
    public const string AssetUpdate = "Asset.Update";
    public const string AssetDelete = "Asset.Delete";


    // User Permissions
    public const string UserCreate = "User.Create";
    public const string UserRead = "User.Read";
    public const string UserUpdate = "User.Update";
    public const string UserDelete = "User.Delete";


    // Role Permissions
    public const string RoleCreate = "Role.Create";
    public const string RoleRead = "Role.Read";
    public const string RoleUpdate = "Role.Update";
    public const string RoleDelete = "Role.Delete";


    // Department Permissions
    public const string DepartmentCreate = "Department.Create";
    public const string DepartmentRead = "Department.Read";
    public const string DepartmentUpdate = "Department.Update";
    public const string DepartmentDelete = "Department.Delete";


    // Supplier Permissions
    public const string SupplierCreate = "Supplier.Create";
    public const string SupplierRead = "Supplier.Read";
    public const string SupplierUpdate = "Supplier.Update";
    public const string SupplierDelete = "Supplier.Delete";
}
