namespace MoveX.Domain.Entities.Operations;

public class DispatchJob
{
    public long Id { get; set; }
    public int BookingId { get; set; }
    public DispatchStatus Status { get; set; } = DispatchStatus.Pending;
    public int? AssignedDriverId { get; set; }
    public int AttemptCount { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? AssignedAt { get; set; }
    public DateTime? AcceptedAt { get; set; }
    public DateTime? ExpiredAt { get; set; }
    public string? FailureReason { get; set; }
}

public enum DispatchStatus
{
    Pending,
    Searching,
    DriverOffered,
    Accepted,
    Rejected,
    Expired,
    Cancelled,
    Completed,
    Failed
}
