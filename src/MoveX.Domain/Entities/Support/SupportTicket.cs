namespace MoveX.Domain.Entities.Support;

public class SupportTicket
{
    public long Id { get; set; }
    public string TicketNumber { get; set; } = string.Empty;
    public int CustomerId { get; set; }
    public int? BookingId { get; set; }
    public string Subject { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public SupportPriority Priority { get; set; } = SupportPriority.Normal;
    public SupportTicketStatus Status { get; set; } = SupportTicketStatus.Open;
    public string? AssignedAdminUserId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ClosedAt { get; set; }
}

public enum SupportPriority
{
    Low,
    Normal,
    High,
    Urgent
}

public enum SupportTicketStatus
{
    Open,
    InProgress,
    WaitingForCustomer,
    Escalated,
    Resolved,
    Closed
}
