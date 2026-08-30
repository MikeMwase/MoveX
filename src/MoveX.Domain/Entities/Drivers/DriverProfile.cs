namespace MoveX.Domain.Entities.Drivers;

public class DriverProfile
{
    public int Id { get; set; }
    public string UserId { get; set; } = string.Empty;
    public string DriverNumber { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public DriverStatus Status { get; set; } = DriverStatus.PendingVerification;
    public bool IsVerified { get; set; }
    public DateTime? ApprovedAt { get; set; }
    public DateTime? SuspendedAt { get; set; }
    public string? SuspensionReason { get; set; }
    public decimal Rating { get; set; }
    public int CompletedTrips { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
    public ICollection<DriverDocument> Documents { get; set; } = new List<DriverDocument>();
}

public enum DriverStatus
{
    PendingVerification,
    PendingApproval,
    Approved,
    Online,
    Offline,
    Busy,
    Suspended,
    Rejected,
    Deactivated
}
