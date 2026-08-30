using Microsoft.EntityFrameworkCore;
using MoveX.Application.Bookings;
using MoveX.Infrastructure.Data;

namespace MoveX.Infrastructure.Services;

public class BookingService(MoveXDbContext db) : IBookingService
{
    public async Task<IReadOnlyList<BookingListItemDto>> GetRecentAsync(int take = 50, CancellationToken cancellationToken = default)
    {
        take = Math.Clamp(take, 1, 200);
        return await db.Bookings.AsNoTracking()
            .OrderByDescending(x => x.RequestedAt)
            .Take(take)
            .Select(x => new BookingListItemDto(x.Id, x.BookingNumber, x.Customer == null ? "Unknown" : x.Customer.FirstName + " " + x.Customer.LastName, x.Status.ToString(), x.RequestedAt, x.ScheduledPickupAt, x.EstimatedPrice, x.FinalPrice))
            .ToListAsync(cancellationToken);
    }

    public async Task<BookingDetailsDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await db.Bookings.AsNoTracking()
            .Where(x => x.Id == id)
            .Select(x => new BookingDetailsDto(x.Id, x.BookingNumber, x.CustomerId, x.Status.ToString(), x.RequestedAt, x.ScheduledPickupAt, x.EstimatedDistanceKm, x.EstimatedDurationMinutes, x.EstimatedPrice, x.FinalPrice))
            .SingleOrDefaultAsync(cancellationToken);
    }
}
