using MoveX.Domain.Entities.Customers;
using MoveX.Domain.Entities.Drivers;
using MoveX.Domain.Entities.Operations;

namespace MoveX.Domain.Entities.Bookings;

public class Booking
{
    public int Id { get; set; }
    public string BookingNumber { get; set; } = string.Empty;
    public int CustomerId { get; set; }
    public BookingStatus Status { get; set; } = BookingStatus.Draft;
    public BookingType Type { get; set; } = BookingType.Immediate;
    public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ScheduledPickupAt { get; set; }
    public int? VehicleTypeId { get; set; }
    public decimal EstimatedDistanceKm { get; set; }
    public decimal EstimatedDurationMinutes { get; set; }
    public decimal EstimatedPrice { get; set; }
    public decimal FinalPrice { get; set; }
    public string? CancellationReason { get; set; }
    public DateTime? CompletedAt { get; set; }

    public CustomerProfile? Customer { get; set; }
    public VehicleType? VehicleType { get; set; }
    public ICollection<BookingAddress> Addresses { get; set; } = new List<BookingAddress>();
    public ICollection<BookingItem> Items { get; set; } = new List<BookingItem>();
    public ICollection<BookingService> Services { get; set; } = new List<BookingService>();
    public ICollection<DriverAssignment> DriverAssignments { get; set; } = new List<DriverAssignment>();
    public ICollection<BookingStatusHistory> StatusHistory { get; set; } = new List<BookingStatusHistory>();
}

public enum BookingType
{
    Immediate,
    Scheduled
}
