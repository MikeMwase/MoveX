namespace MoveX.Domain.Entities.Bookings;

public class BookingItem
{
    public long Id { get; set; }
    public int BookingId { get; set; }
    public string Description { get; set; } = string.Empty;
    public int Quantity { get; set; } = 1;
    public decimal? WeightKg { get; set; }
    public decimal? LengthCm { get; set; }
    public decimal? WidthCm { get; set; }
    public decimal? HeightCm { get; set; }
    public bool IsFragile { get; set; }
    public bool RequiresDisassembly { get; set; }
    public bool RequiresAssembly { get; set; }

    public Booking? Booking { get; set; }
}
