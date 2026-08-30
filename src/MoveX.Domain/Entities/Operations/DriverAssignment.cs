namespace MoveX.Domain.Entities.Operations;

public class DriverAssignment
{
    public long Id { get; set; }
    public int BookingId { get; set; }
    public int DriverId { get; set; }
    public AssignmentStatus Status { get; set; } = AssignmentStatus.Offered;
    public bool IsPrimaryDriver { get; set; }
    public DateTime OfferedAt { get; set; } = DateTime.UtcNow;
    public DateTime? AcceptedAt { get; set; }
    public DateTime? RejectedAt { get; set; }
    public string? RejectionReason { get; set; }
}

public enum AssignmentStatus
{
    Offered,
    Accepted,
    Rejected,
    Expired,
    Cancelled,
    Completed
}
