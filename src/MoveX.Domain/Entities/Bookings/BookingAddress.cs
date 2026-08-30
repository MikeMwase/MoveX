namespace MoveX.Domain.Entities.Bookings;

public class BookingAddress
{
    public long Id { get; set; }
    public int BookingId { get; set; }
    public BookingAddressType Type { get; set; }
    public string AddressLine1 { get; set; } = string.Empty;
    public string? AddressLine2 { get; set; }
    public string City { get; set; } = string.Empty;
    public string? Province { get; set; }
    public string? PostalCode { get; set; }
    public string Country { get; set; } = "South Africa";
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string? ContactName { get; set; }
    public string? ContactPhone { get; set; }
    public string? AccessInstructions { get; set; }

    public Booking? Booking { get; set; }
}

public enum BookingAddressType
{
    Pickup,
    Destination,
    Waypoint
}
