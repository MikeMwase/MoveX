namespace MoveX.Domain.Entities.Trips;

public class DeliveryProof
{
    public long Id { get; set; }
    public long TripId { get; set; }
    public DeliveryProofType Type { get; set; }
    public string? FileUrl { get; set; }
    public string? OtpCodeHash { get; set; }
    public string? RecipientName { get; set; }
    public string? Notes { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Trip? Trip { get; set; }
}

public enum DeliveryProofType
{
    Photo,
    Signature,
    Otp,
    RecipientConfirmation
}
