namespace MoveX.Application.Bookings;

public interface IBookingService
{
    Task<IReadOnlyList<BookingListItemDto>> GetRecentAsync(int take = 50, CancellationToken cancellationToken = default);
    Task<BookingDetailsDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
}
