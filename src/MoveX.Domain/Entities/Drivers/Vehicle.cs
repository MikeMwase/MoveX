namespace MoveX.Domain.Entities.Drivers;

public class Vehicle
{
    public int Id { get; set; }
    public int DriverId { get; set; }
    public int VehicleTypeId { get; set; }
    public string RegistrationNumber { get; set; } = string.Empty;
    public string Make { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public string? Colour { get; set; }
    public decimal MaximumLoadKg { get; set; }
    public bool HasTrailer { get; set; }
    public VehicleStatus Status { get; set; } = VehicleStatus.PendingVerification;
    public bool IsVerified { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DriverProfile? Driver { get; set; }
    public VehicleType? VehicleType { get; set; }
}

public enum VehicleStatus
{
    PendingVerification,
    Approved,
    Active,
    Inactive,
    Suspended,
    Rejected
}
