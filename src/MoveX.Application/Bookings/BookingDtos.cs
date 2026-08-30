namespace MoveX.Application.Bookings;

public record BookingListItemDto(int Id, string BookingNumber, string CustomerName, string Status, DateTime RequestedAt, DateTime? ScheduledPickupAt, decimal EstimatedPrice, decimal FinalPrice);
public record BookingDetailsDto(int Id, string BookingNumber, int CustomerId, string Status, DateTime RequestedAt, DateTime? ScheduledPickupAt, decimal EstimatedDistanceKm, decimal EstimatedDurationMinutes, decimal EstimatedPrice, decimal FinalPrice);
