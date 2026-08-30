using Microsoft.EntityFrameworkCore;
using MoveX.Domain.Entities.Admin;
using MoveX.Domain.Entities.Bookings;
using MoveX.Domain.Entities.Customers;
using MoveX.Domain.Entities.Drivers;
using MoveX.Domain.Entities.Finance;
using MoveX.Domain.Entities.Pricing;
using MoveX.Domain.Entities.Services;
using MoveX.Domain.Entities.Trips;
using MoveX.Domain.Entities.Operations;
using MoveX.Infrastructure.Data.Seed;

namespace MoveX.Infrastructure.Data;

public class MoveXDbContext : DbContext
{
    public MoveXDbContext(DbContextOptions<MoveXDbContext> options) : base(options) { }

    public DbSet<AdminUser> AdminUsers => Set<AdminUser>();
    public DbSet<AdminRole> AdminRoles => Set<AdminRole>();
    public DbSet<AdminPermission> AdminPermissions => Set<AdminPermission>();
    public DbSet<AdminRolePermission> AdminRolePermissions => Set<AdminRolePermission>();
    public DbSet<CustomerProfile> Customers => Set<CustomerProfile>();
    public DbSet<CustomerAddress> CustomerAddresses => Set<CustomerAddress>();
    public DbSet<DriverProfile> Drivers => Set<DriverProfile>();
    public DbSet<Vehicle> Vehicles => Set<Vehicle>();
    public DbSet<VehicleType> VehicleTypes => Set<VehicleType>();
    public DbSet<DriverDocument> DriverDocuments => Set<DriverDocument>();
    public DbSet<Booking> Bookings => Set<Booking>();
    public DbSet<BookingAddress> BookingAddresses => Set<BookingAddress>();
    public DbSet<BookingItem> BookingItems => Set<BookingItem>();
    public DbSet<BookingService> BookingServices => Set<BookingService>();
    public DbSet<BookingStatusHistory> BookingStatusHistory => Set<BookingStatusHistory>();
    public DbSet<DispatchJob> DispatchJobs => Set<DispatchJob>();
    public DbSet<DriverAssignment> DriverAssignments => Set<DriverAssignment>();
    public DbSet<DriverLocation> DriverLocations => Set<DriverLocation>();
    public DbSet<OperationalIncident> OperationalIncidents => Set<OperationalIncident>();
    public DbSet<MovingService> MovingServices => Set<MovingService>();
    public DbSet<PricingRule> PricingRules => Set<PricingRule>();
    public DbSet<Trip> Trips => Set<Trip>();
    public DbSet<TripWaypoint> TripWaypoints => Set<TripWaypoint>();
    public DbSet<DeliveryProof> DeliveryProofs => Set<DeliveryProof>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<DriverPayout> DriverPayouts => Set<DriverPayout>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<AdminUser>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Email).HasMaxLength(320).IsRequired();
            e.HasIndex(x => x.UserId).IsUnique();
            e.HasIndex(x => x.Email).IsUnique();
            e.HasOne(x => x.Role).WithMany(x => x.Users).HasForeignKey(x => x.AdminRoleId).OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<AdminRole>(e => { e.HasKey(x => x.Id); e.Property(x => x.Name).HasMaxLength(100).IsRequired(); e.HasIndex(x => x.Name).IsUnique(); });
        modelBuilder.Entity<AdminPermission>(e => { e.HasKey(x => x.Id); e.Property(x => x.Code).HasMaxLength(150).IsRequired(); e.Property(x => x.Module).HasMaxLength(100).IsRequired(); e.HasIndex(x => x.Code).IsUnique(); });
        modelBuilder.Entity<AdminRolePermission>(e =>
        {
            e.HasKey(x => new { x.AdminRoleId, x.AdminPermissionId });
            e.HasOne(x => x.Role).WithMany(x => x.Permissions).HasForeignKey(x => x.AdminRoleId).OnDelete(DeleteBehavior.Cascade);
            e.HasOne(x => x.Permission).WithMany(x => x.RolePermissions).HasForeignKey(x => x.AdminPermissionId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<CustomerProfile>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.CustomerNumber).HasMaxLength(50).IsRequired();
            e.Property(x => x.Email).HasMaxLength(320).IsRequired();
            e.HasIndex(x => x.CustomerNumber).IsUnique();
            e.HasIndex(x => x.UserId).IsUnique();
        });
        modelBuilder.Entity<CustomerAddress>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Label).HasMaxLength(100).IsRequired();
            e.Property(x => x.City).HasMaxLength(100).IsRequired();
            e.HasIndex(x => new { x.CustomerId, x.IsDefault });
            e.HasOne(x => x.Customer).WithMany(x => x.Addresses).HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<DriverProfile>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.DriverNumber).HasMaxLength(50).IsRequired();
            e.Property(x => x.Email).HasMaxLength(320).IsRequired();
            e.Property(x => x.Rating).HasPrecision(3, 2);
            e.HasIndex(x => x.DriverNumber).IsUnique();
            e.HasIndex(x => x.UserId).IsUnique();
            e.HasIndex(x => x.Status);
        });
        modelBuilder.Entity<VehicleType>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.Name).HasMaxLength(100).IsRequired();
            e.Property(x => x.MaximumLoadKg).HasPrecision(12, 2);
            e.Property(x => x.MaximumVolumeM3).HasPrecision(12, 3);
            e.HasIndex(x => x.Name).IsUnique();
        });
        modelBuilder.Entity<Vehicle>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.RegistrationNumber).HasMaxLength(20).IsRequired();
            e.Property(x => x.MaximumLoadKg).HasPrecision(12, 2);
            e.HasIndex(x => x.RegistrationNumber).IsUnique();
            e.HasIndex(x => new { x.DriverId, x.Status });
            e.HasOne(x => x.Driver).WithMany(x => x.Vehicles).HasForeignKey(x => x.DriverId).OnDelete(DeleteBehavior.Restrict);
            e.HasOne(x => x.VehicleType).WithMany(x => x.Vehicles).HasForeignKey(x => x.VehicleTypeId).OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<DriverDocument>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.FileUrl).HasMaxLength(2048).IsRequired();
            e.HasIndex(x => new { x.DriverId, x.DocumentType, x.Status });
            e.HasOne(x => x.Driver).WithMany(x => x.Documents).HasForeignKey(x => x.DriverId).OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Booking>(e =>
        {
            e.HasKey(x => x.Id);
            e.Property(x => x.BookingNumber).HasMaxLength(50).IsRequired();
            e.Property(x => x.EstimatedDistanceKm).HasPrecision(12, 2);
            e.Property(x => x.EstimatedDurationMinutes).HasPrecision(12, 2);
            e.Property(x => x.EstimatedPrice).HasPrecision(18, 2);
            e.Property(x => x.FinalPrice).HasPrecision(18, 2);
            e.HasIndex(x => x.BookingNumber).IsUnique();
            e.HasIndex(x => new { x.Status, x.RequestedAt });
            e.HasIndex(x => new { x.CustomerId, x.CreatedAt });
            e.HasOne(x => x.Customer).WithMany().HasForeignKey(x => x.CustomerId).OnDelete(DeleteBehavior.Restrict);
            e.HasOne(x => x.VehicleType).WithMany().HasForeignKey(x => x.VehicleTypeId).OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<BookingAddress>(e =>
        {
            e.HasKey(x => x.Id); e.Property(x => x.AddressLine1).HasMaxLength(250).IsRequired(); e.Property(x => x.City).HasMaxLength(100).IsRequired();
            e.HasIndex(x => new { x.BookingId, x.Type });
            e.HasOne(x => x.Booking).WithMany(x => x.Addresses).HasForeignKey(x => x.BookingId).OnDelete(DeleteBehavior.Cascade);
        });
        modelBuilder.Entity<BookingItem>(e =>
        {
            e.HasKey(x => x.Id); e.Property(x => x.Description).HasMaxLength(250).IsRequired();
            e.Property(x => x.WeightKg).HasPrecision(12, 2); e.Property(x => x.LengthCm).HasPrecision(12, 2); e.Property(x => x.WidthCm).HasPrecision(12, 2); e.Property(x => x.HeightCm).HasPrecision(12, 2);
            e.HasOne(x => x.Booking).WithMany(x => x.Items).HasForeignKey(x => x.BookingId).OnDelete(DeleteBehavior.Cascade);
        });
        modelBuilder.Entity<BookingService>(e =>
        {
            e.HasKey(x => x.Id); e.Property(x => x.UnitPrice).HasPrecision(18, 2); e.Property(x => x.TotalPrice).HasPrecision(18, 2);
            e.HasIndex(x => new { x.BookingId, x.MovingServiceId });
            e.HasOne(x => x.Booking).WithMany(x => x.Services).HasForeignKey(x => x.BookingId).OnDelete(DeleteBehavior.Cascade);
        });
        modelBuilder.Entity<BookingStatusHistory>(e => { e.HasKey(x => x.Id); e.HasIndex(x => new { x.BookingId, x.ChangedAt }); });

        modelBuilder.Entity<DispatchJob>(e => { e.HasKey(x => x.Id); e.HasIndex(x => new { x.Status, x.CreatedAt }); e.HasIndex(x => x.BookingId); e.HasIndex(x => x.AssignedDriverId); });
        modelBuilder.Entity<DriverAssignment>(e => { e.HasKey(x => x.Id); e.HasIndex(x => new { x.BookingId, x.Status }); e.HasIndex(x => new { x.DriverId, x.Status }); });
        modelBuilder.Entity<DriverLocation>(e => { e.HasKey(x => x.Id); e.HasIndex(x => new { x.DriverId, x.RecordedAt }); e.HasIndex(x => x.RecordedAt); e.Property(x => x.Latitude).HasPrecision(10, 7); e.Property(x => x.Longitude).HasPrecision(10, 7); });
        modelBuilder.Entity<OperationalIncident>(e => { e.HasKey(x => x.Id); e.HasIndex(x => new { x.Status, x.Severity, x.CreatedAt }); e.HasIndex(x => x.BookingId); e.HasIndex(x => x.DriverId); });

        modelBuilder.Entity<MovingService>(e => { e.HasKey(x => x.Id); e.Property(x => x.Name).HasMaxLength(150).IsRequired(); e.Property(x => x.Price).HasPrecision(18, 2); e.HasIndex(x => x.Name).IsUnique(); });
        modelBuilder.Entity<PricingRule>(e =>
        {
            e.HasKey(x => x.Id); e.Property(x => x.Name).HasMaxLength(150).IsRequired();
            e.Property(x => x.BaseFare).HasPrecision(18, 2); e.Property(x => x.PerKmRate).HasPrecision(18, 2); e.Property(x => x.PerMinuteRate).HasPrecision(18, 2); e.Property(x => x.MinimumFare).HasPrecision(18, 2); e.Property(x => x.LoadingFee).HasPrecision(18, 2); e.Property(x => x.UnloadingFee).HasPrecision(18, 2); e.Property(x => x.NightSurchargePercent).HasPrecision(8, 4); e.Property(x => x.WeekendSurchargePercent).HasPrecision(8, 4);
            e.HasIndex(x => new { x.VehicleTypeId, x.IsActive, x.EffectiveFrom });
            e.HasOne(x => x.VehicleType).WithMany().HasForeignKey(x => x.VehicleTypeId).OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Trip>(e =>
        {
            e.HasKey(x => x.Id); e.Property(x => x.ActualDistanceKm).HasPrecision(12, 2); e.Property(x => x.ActualDurationMinutes).HasPrecision(12, 2);
            e.HasIndex(x => new { x.Status, x.StartedAt }); e.HasIndex(x => x.BookingId).IsUnique();
            e.HasOne(x => x.Booking).WithMany().HasForeignKey(x => x.BookingId).OnDelete(DeleteBehavior.Restrict);
            e.HasOne(x => x.Driver).WithMany().HasForeignKey(x => x.DriverId).OnDelete(DeleteBehavior.Restrict);
            e.HasOne(x => x.Vehicle).WithMany().HasForeignKey(x => x.VehicleId).OnDelete(DeleteBehavior.Restrict);
        });
        modelBuilder.Entity<TripWaypoint>(e => { e.HasKey(x => x.Id); e.HasIndex(x => new { x.TripId, x.Sequence }).IsUnique(); e.Property(x => x.Latitude).HasPrecision(10, 7); e.Property(x => x.Longitude).HasPrecision(10, 7); e.HasOne(x => x.Trip).WithMany(x => x.Waypoints).HasForeignKey(x => x.TripId).OnDelete(DeleteBehavior.Cascade); });
        modelBuilder.Entity<DeliveryProof>(e => { e.HasKey(x => x.Id); e.Property(x => x.FileUrl).HasMaxLength(2048); e.HasIndex(x => new { x.TripId, x.Type }); e.HasOne(x => x.Trip).WithMany(x => x.DeliveryProofs).HasForeignKey(x => x.TripId).OnDelete(DeleteBehavior.Cascade); });
        modelBuilder.Entity<Payment>(e => { e.HasKey(x => x.Id); e.Property(x => x.PaymentReference).HasMaxLength(100).IsRequired(); e.Property(x => x.Currency).HasMaxLength(3).IsRequired(); e.Property(x => x.Amount).HasPrecision(18, 2); e.HasIndex(x => x.PaymentReference).IsUnique(); e.HasIndex(x => new { x.BookingId, x.Status }); e.HasOne(x => x.Booking).WithMany().HasForeignKey(x => x.BookingId).OnDelete(DeleteBehavior.Restrict); });
        modelBuilder.Entity<DriverPayout>(e => { e.HasKey(x => x.Id); e.Property(x => x.GrossAmount).HasPrecision(18, 2); e.Property(x => x.CommissionAmount).HasPrecision(18, 2); e.Property(x => x.Adjustments).HasPrecision(18, 2); e.Property(x => x.NetAmount).HasPrecision(18, 2); e.HasIndex(x => new { x.DriverId, x.PeriodStart, x.PeriodEnd }); e.HasOne(x => x.Driver).WithMany().HasForeignKey(x => x.DriverId).OnDelete(DeleteBehavior.Restrict); });

        MoveXSeedData.Seed(modelBuilder);
    }
}
