using Microsoft.EntityFrameworkCore;
using MoveX.Application.Operations;
using MoveX.Domain.Entities.Operations;
using MoveX.Domain.Entities.Drivers;
using MoveX.Infrastructure.Data;

namespace MoveX.Infrastructure.Services;

public class OperationsCommandService(MoveXDbContext db) : IOperationsCommandService
{
    public async Task<AssignDriverResult> AssignDriverAsync(AssignDriverCommand command, CancellationToken cancellationToken = default)
    {
        await using var transaction = await db.Database.BeginTransactionAsync(cancellationToken);

        var booking = await db.Bookings.SingleOrDefaultAsync(x => x.Id == command.BookingId, cancellationToken);
        if (booking is null) return new(false, Error: "Booking not found.");

        var driver = await db.Drivers.Include(x => x.Vehicles).SingleOrDefaultAsync(x => x.Id == command.DriverId, cancellationToken);
        if (driver is null) return new(false, Error: "Driver not found.");
        if (driver.Status is not (DriverStatus.Online or DriverStatus.Approved)) return new(false, Error: "Driver is not available for assignment.");
        if (!driver.Vehicles.Any(x => x.Status == VehicleStatus.Active)) return new(false, Error: "Driver has no active vehicle.");

        var existing = await db.DriverAssignments.AnyAsync(x => x.BookingId == command.BookingId && x.DriverId == command.DriverId && x.Status != AssignmentStatus.Rejected && x.Status != AssignmentStatus.Expired && x.Status != AssignmentStatus.Cancelled, cancellationToken);
        if (existing) return new(false, Error: "Driver is already assigned to this booking.");

        var now = DateTime.UtcNow;
        var assignment = new DriverAssignment
        {
            BookingId = command.BookingId,
            DriverId = command.DriverId,
            IsPrimary = command.IsPrimaryDriver,
            Status = AssignmentStatus.Assigned,
            AssignedAt = now
        };
        db.DriverAssignments.Add(assignment);

        var previousStatus = booking.Status;
        booking.Status = BookingStatus.DriverAssigned;
        db.BookingStatusHistory.Add(new BookingStatusHistory
        {
            BookingId = booking.Id,
            PreviousStatus = previousStatus,
            Status = booking.Status,
            Reason = $"Driver {driver.DriverNumber} assigned by operations."
        });

        await db.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);
        return new(true, assignment.Id);
    }

    public async Task<ChangeBookingStatusResult> ChangeBookingStatusAsync(ChangeBookingStatusCommand command, CancellationToken cancellationToken = default)
    {
        if (!Enum.TryParse<BookingStatus>(command.Status, true, out var newStatus))
            return new(false, Error: "Invalid booking status.");

        var booking = await db.Bookings.SingleOrDefaultAsync(x => x.Id == command.BookingId, cancellationToken);
        if (booking is null) return new(false, Error: "Booking not found.");

        if (booking.Status == newStatus) return new(true, newStatus.ToString());

        var previousStatus = booking.Status;
        booking.Status = newStatus;
        if (newStatus == BookingStatus.DeliveryCompleted || newStatus == BookingStatus.PaymentCompleted || newStatus == BookingStatus.Rated)
            booking.CompletedAt ??= DateTime.UtcNow;
        if (newStatus == BookingStatus.Cancelled)
            booking.CancellationReason = command.Reason;

        db.BookingStatusHistory.Add(new BookingStatusHistory
        {
            BookingId = booking.Id,
            PreviousStatus = previousStatus,
            Status = newStatus,
            Reason = command.Reason
        });

        await db.SaveChangesAsync(cancellationToken);
        return new(true, newStatus.ToString());
    }
}
