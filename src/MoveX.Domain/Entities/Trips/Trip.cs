using MoveX.Domain.Entities.Bookings;
using MoveX.Domain.Entities.Drivers;

namespace MoveX.Domain.Entities.Trips;

public class Trip
{
    public long Id { get; set; }
    public int BookingId { get; set; }
    public int DriverId { get; set; }
    public int? VehicleId { get; set; }
    public TripStatus Status { get; set; } = TripStatus.Assigned;
    public DateTime? StartedAt { get; set; }
    public DateTime? ArrivedAtPickupAt { get; set; }
    public DateTime? LoadingStartedAt { get; set; }
    public DateTime? LoadingCompletedAt { get; set; }
    public DateTime? ArrivedAtDestinationAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public decimal? ActualDistanceKm { get; set; }
    public decimal? ActualDurationMinutes { get; set; }

    public Booking? Booking { get; set; }
    public DriverProfile? Driver { get; set; }
    public Vehicle? Vehicle { get; set; }
    public ICollection<TripWaypoint> Waypoints { get; set; } = new List<TripWaypoint>();
    public ICollection<DeliveryProof> DeliveryProofs { get; set; } = new List<DeliveryProof>();
}

public enum TripStatus
{
    Assigned,
    EnRouteToPickup,
    ArrivedAtPickup,
    Loading,
    LoadingComplete,
    InTransit,
    ArrivedAtDestination,
    Unloading,
    Completed,
    Cancelled
}
