using Microsoft.EntityFrameworkCore;
using MoveX.Application.Operations;
using MoveX.Infrastructure.Data;
using MoveX.Domain.Entities.Drivers;
using MoveX.Domain.Entities.Operations;
using MoveX.Domain.Entities.Finance;

namespace MoveX.Infrastructure.Services;

public class OperationsService(MoveXDbContext db) : IOperationsService
{
    public async Task<OperationsDashboardDto> GetDashboardAsync(CancellationToken cancellationToken = default)
    {
        var activeBookings = await db.Bookings.CountAsync(x => x.Status != BookingStatus.DeliveryCompleted && x.Status != BookingStatus.PaymentCompleted && x.Status != BookingStatus.Rated && x.Status != BookingStatus.Cancelled, cancellationToken);
        var pendingDispatches = await db.DispatchJobs.CountAsync(x => x.Status == DispatchStatus.Pending || x.Status == DispatchStatus.Searching || x.Status == DispatchStatus.DriverOffered, cancellationToken);
        var onlineDrivers = await db.Drivers.CountAsync(x => x.Status == DriverStatus.Online, cancellationToken);
        var openIncidents = await db.OperationalIncidents.CountAsync(x => x.Status != IncidentStatus.Resolved && x.Status != IncidentStatus.Closed, cancellationToken);
        var start = DateTime.UtcNow.Date;
        var todayRevenue = await db.Payments.Where(x => x.Status == PaymentStatus.Completed && x.CompletedAt >= start).SumAsync(x => (decimal?)x.Amount, cancellationToken) ?? 0m;
        return new OperationsDashboardDto(activeBookings, pendingDispatches, onlineDrivers, openIncidents, todayRevenue);
    }

    public async Task<IReadOnlyList<DispatchCandidateDto>> GetDispatchCandidatesAsync(int bookingId, CancellationToken cancellationToken = default)
    {
        var bookingExists = await db.Bookings.AnyAsync(x => x.Id == bookingId, cancellationToken);
        if (!bookingExists) return [];

        var assigned = await db.DriverAssignments.Where(x => x.BookingId == bookingId && x.Status != AssignmentStatus.Rejected && x.Status != AssignmentStatus.Expired && x.Status != AssignmentStatus.Cancelled).Select(x => x.DriverId).ToListAsync(cancellationToken);

        return await db.Drivers.AsNoTracking()
            .Where(x => x.Status == DriverStatus.Online && !assigned.Contains(x.Id))
            .Select(x => new DispatchCandidateDto(x.Id, x.DriverNumber, x.FirstName + " " + x.LastName, x.Vehicles.Where(v => v.Status == VehicleStatus.Active).Select(v => v.RegistrationNumber).FirstOrDefault() ?? "No vehicle", db.DriverLocations.Where(l => l.DriverId == x.Id).OrderByDescending(l => l.RecordedAt).Select(l => (double?)l.Latitude).FirstOrDefault(), db.DriverLocations.Where(l => l.DriverId == x.Id).OrderByDescending(l => l.RecordedAt).Select(l => (double?)l.Longitude).FirstOrDefault(), x.Rating))
            .Take(50)
            .ToListAsync(cancellationToken);
    }
}
