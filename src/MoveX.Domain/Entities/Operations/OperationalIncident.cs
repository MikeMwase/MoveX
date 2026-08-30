namespace MoveX.Domain.Entities.Operations;

public class OperationalIncident
{
    public long Id { get; set; }
    public int? BookingId { get; set; }
    public int? DriverId { get; set; }
    public IncidentType Type { get; set; }
    public IncidentSeverity Severity { get; set; }
    public string Description { get; set; } = string.Empty;
    public IncidentStatus Status { get; set; } = IncidentStatus.Open;
    public string? LocationDescription { get; set; }
    public int? AssignedAdminId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ResolvedAt { get; set; }
    public string? Resolution { get; set; }
}

public enum IncidentType
{
    DriverAccident,
    VehicleBreakdown,
    CustomerUnavailable,
    DamagedProperty,
    MissingItem,
    DriverMisconduct,
    CustomerMisconduct,
    PaymentProblem,
    RouteProblem,
    Emergency,
    Other
}

public enum IncidentSeverity
{
    Low,
    Medium,
    High,
    Critical
}

public enum IncidentStatus
{
    Open,
    Investigating,
    Escalated,
    Resolved,
    Closed
}
