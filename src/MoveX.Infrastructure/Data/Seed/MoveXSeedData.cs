using Microsoft.EntityFrameworkCore;
using MoveX.Domain.Entities.Admin;
using MoveX.Domain.Entities.Drivers;
using MoveX.Domain.Entities.Services;

namespace MoveX.Infrastructure.Data.Seed;

public static class MoveXSeedData
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        var roles = new[]
        {
            new AdminRole { Id = 1, Name = "SuperAdmin", Description = "Full system access" },
            new AdminRole { Id = 2, Name = "OperationsManager", Description = "Operations and dispatch management" },
            new AdminRole { Id = 3, Name = "Dispatcher", Description = "Dispatch and live trip operations" },
            new AdminRole { Id = 4, Name = "DriverManager", Description = "Driver and vehicle management" },
            new AdminRole { Id = 5, Name = "CustomerSupport", Description = "Customer support and incidents" },
            new AdminRole { Id = 6, Name = "FinanceManager", Description = "Payments and driver payouts" },
            new AdminRole { Id = 7, Name = "ComplianceOfficer", Description = "Driver and vehicle compliance" },
            new AdminRole { Id = 8, Name = "ReadOnly", Description = "Read-only operational access" }
        };

        var permissionDefinitions = new[]
        {
            (1, "View Bookings", "BOOKING_VIEW", "Bookings"), (2, "Create Bookings", "BOOKING_CREATE", "Bookings"),
            (3, "Edit Bookings", "BOOKING_EDIT", "Bookings"), (4, "Cancel Bookings", "BOOKING_CANCEL", "Bookings"),
            (5, "Assign Drivers", "BOOKING_ASSIGN_DRIVER", "Bookings"), (6, "View Drivers", "DRIVER_VIEW", "Drivers"),
            (7, "Approve Drivers", "DRIVER_APPROVE", "Drivers"), (8, "Suspend Drivers", "DRIVER_SUSPEND", "Drivers"),
            (9, "View Vehicles", "VEHICLE_VIEW", "Vehicles"), (10, "Approve Vehicles", "VEHICLE_APPROVE", "Vehicles"),
            (11, "Suspend Vehicles", "VEHICLE_SUSPEND", "Vehicles"), (12, "View Payments", "PAYMENT_VIEW", "Finance"),
            (13, "Refund Payments", "PAYMENT_REFUND", "Finance"), (14, "Approve Payouts", "PAYOUT_APPROVE", "Finance"),
            (15, "View Pricing", "PRICING_VIEW", "Pricing"), (16, "Manage Pricing", "PRICING_MANAGE", "Pricing"),
            (17, "View Reports", "REPORT_VIEW", "Reports"), (18, "Export Reports", "REPORT_EXPORT", "Reports"),
            (19, "Manage Admin Users", "ADMIN_USER_MANAGE", "Administration"), (20, "Manage Roles", "ADMIN_ROLE_MANAGE", "Administration"),
            (21, "View Audit Logs", "AUDIT_VIEW", "Administration")
        };

        modelBuilder.Entity<AdminRole>().HasData(roles);
        modelBuilder.Entity<AdminPermission>().HasData(permissionDefinitions.Select(x => new AdminPermission
        {
            Id = x.Item1, Name = x.Item2, Code = x.Item3, Module = x.Item4, IsActive = true
        }));

        var allPermissionIds = permissionDefinitions.Select(x => x.Item1).ToArray();
        modelBuilder.Entity<AdminRolePermission>().HasData(
            allPermissionIds.Select(permissionId => new AdminRolePermission { AdminRoleId = 1, AdminPermissionId = permissionId })
            .Concat(new[]
            {
                1, 5, 6, 9, 12, 15, 17
            }.Select(permissionId => new AdminRolePermission { AdminRoleId = 2, AdminPermissionId = permissionId }))
            .Concat(new[]
            {
                1, 5
            }.Select(permissionId => new AdminRolePermission { AdminRoleId = 3, AdminPermissionId = permissionId }))
            .Concat(new[]
            {
                6, 7, 8, 9, 10, 11, 17
            }.Select(permissionId => new AdminRolePermission { AdminRoleId = 4, AdminPermissionId = permissionId }))
            .Concat(new[]
            {
                1, 6, 17
            }.Select(permissionId => new AdminRolePermission { AdminRoleId = 5, AdminPermissionId = permissionId }))
            .Concat(new[]
            {
                12, 13, 14, 17, 18
            }.Select(permissionId => new AdminRolePermission { AdminRoleId = 6, AdminPermissionId = permissionId }))
            .Concat(new[]
            {
                6, 7, 8, 9, 10, 11, 17, 21
            }.Select(permissionId => new AdminRolePermission { AdminRoleId = 7, AdminPermissionId = permissionId }))
            .Concat(new[]
            {
                1, 6, 9, 12, 15, 17
            }.Select(permissionId => new AdminRolePermission { AdminRoleId = 8, AdminPermissionId = permissionId })));

        modelBuilder.Entity<VehicleType>().HasData(
            new VehicleType { Id = 1, Name = "Bakkie", Description = "Pickup/bakkie for smaller moves", MaximumLoadKg = 1000 },
            new VehicleType { Id = 2, Name = "Panel Van", Description = "Enclosed van for smaller moves", MaximumLoadKg = 1200 },
            new VehicleType { Id = 3, Name = "1 Ton Truck", MaximumLoadKg = 1000 },
            new VehicleType { Id = 4, Name = "2 Ton Truck", MaximumLoadKg = 2000 },
            new VehicleType { Id = 5, Name = "4 Ton Truck", MaximumLoadKg = 4000 },
            new VehicleType { Id = 6, Name = "8 Ton Truck", MaximumLoadKg = 8000 },
            new VehicleType { Id = 7, Name = "Trailer", MaximumLoadKg = 2000 }
        );

        modelBuilder.Entity<MovingService>().HasData(
            new MovingService { Id = 1, Name = "Transport Only", Description = "Vehicle and driver transport", Price = 0, PricingUnit = PricingUnit.Fixed },
            new MovingService { Id = 2, Name = "Loading Assistance", Description = "Mover assistance with loading", Price = 250, PricingUnit = PricingUnit.PerHour },
            new MovingService { Id = 3, Name = "Unloading Assistance", Description = "Mover assistance with unloading", Price = 250, PricingUnit = PricingUnit.PerHour },
            new MovingService { Id = 4, Name = "Packing Service", Description = "Packing assistance", Price = 350, PricingUnit = PricingUnit.PerHour },
            new MovingService { Id = 5, Name = "Furniture Assembly", Description = "Furniture disassembly/assembly", Price = 300, PricingUnit = PricingUnit.PerHour },
            new MovingService { Id = 6, Name = "Fragile Item Handling", Description = "Special handling for fragile goods", Price = 150, PricingUnit = PricingUnit.Fixed }
        );
    }
}
