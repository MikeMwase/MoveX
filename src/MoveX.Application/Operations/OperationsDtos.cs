namespace MoveX.Application.Operations;

public record OperationsDashboardDto(int ActiveBookings, int PendingDispatches, int OnlineDrivers, int OpenIncidents, decimal TodayRevenue);

public record DispatchCandidateDto(int DriverId, string DriverNumber, string DriverName, string VehicleRegistration, double? Latitude, double? Longitude, decimal Rating);
