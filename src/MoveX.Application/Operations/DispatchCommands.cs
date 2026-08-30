namespace MoveX.Application.Operations;

public record AssignDriverCommand(int BookingId, int DriverId, bool IsPrimaryDriver = true);
public record ChangeBookingStatusCommand(int BookingId, string Status, string? Reason = null);
