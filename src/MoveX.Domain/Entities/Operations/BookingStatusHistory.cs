namespace MoveX.Domain.Entities.Operations;

public class BookingStatusHistory
{
    public long Id { get; set; }
    public int BookingId { get; set; }
    public BookingStatus Status { get; set; }
    public BookingStatus? PreviousStatus { get; set; }
    public string? ChangedByUserId { get; set; }
    public string? Reason { get; set; }
    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;
}

public enum BookingStatus
{
    Draft,
    QuoteRequested,
    QuoteAccepted,
    PaymentPending,
    Confirmed,
    SearchingForDriver,
    DriverAssigned,
    DriverEnRouteToPickup,
    ArrivedAtPickup,
    Loading,
    LoadingComplete,
    TripStarted,
    EnRouteToDestination,
    ArrivedAtDestination,
    Unloading,
    DeliveryCompleted,
    PaymentCompleted,
    Rated,
    Cancelled,
    Disputed
}
