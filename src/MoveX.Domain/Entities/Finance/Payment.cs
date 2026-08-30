using MoveX.Domain.Entities.Bookings;

namespace MoveX.Domain.Entities.Finance;

public class Payment
{
    public long Id { get; set; }
    public int BookingId { get; set; }
    public string PaymentReference { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public string Currency { get; set; } = "ZAR";
    public PaymentMethod Method { get; set; }
    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
    public string? ProviderReference { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? CompletedAt { get; set; }

    public Booking? Booking { get; set; }
}

public enum PaymentMethod
{
    Card,
    InstantEft,
    BankTransfer,
    Cash,
    Wallet,
    Other
}

public enum PaymentStatus
{
    Pending,
    Processing,
    Completed,
    Failed,
    Cancelled,
    Refunded,
    PartiallyRefunded
}
