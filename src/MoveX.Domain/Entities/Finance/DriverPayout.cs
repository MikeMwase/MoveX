using MoveX.Domain.Entities.Drivers;

namespace MoveX.Domain.Entities.Finance;

public class DriverPayout
{
    public long Id { get; set; }
    public int DriverId { get; set; }
    public decimal GrossAmount { get; set; }
    public decimal CommissionAmount { get; set; }
    public decimal Adjustments { get; set; }
    public decimal NetAmount { get; set; }
    public PayoutStatus Status { get; set; } = PayoutStatus.Pending;
    public DateTime PeriodStart { get; set; }
    public DateTime PeriodEnd { get; set; }
    public DateTime? PaidAt { get; set; }
    public string? PaymentReference { get; set; }

    public DriverProfile? Driver { get; set; }
}

public enum PayoutStatus
{
    Pending,
    Approved,
    Processing,
    Paid,
    Failed,
    Cancelled
}
