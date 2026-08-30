namespace MoveX.Domain.Entities.Bookings;

public class BookingService
{
    public long Id { get; set; }
    public int BookingId { get; set; }
    public int MovingServiceId { get; set; }
    public int Quantity { get; set; } = 1;
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }

    public Booking? Booking { get; set; }
}
