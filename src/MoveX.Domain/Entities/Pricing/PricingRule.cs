using MoveX.Domain.Entities.Drivers;

namespace MoveX.Domain.Entities.Pricing;

public class PricingRule
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int VehicleTypeId { get; set; }
    public decimal BaseFare { get; set; }
    public decimal PerKmRate { get; set; }
    public decimal PerMinuteRate { get; set; }
    public decimal MinimumFare { get; set; }
    public decimal LoadingFee { get; set; }
    public decimal UnloadingFee { get; set; }
    public decimal NightSurchargePercent { get; set; }
    public decimal WeekendSurchargePercent { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime EffectiveFrom { get; set; } = DateTime.UtcNow;
    public DateTime? EffectiveTo { get; set; }

    public VehicleType? VehicleType { get; set; }
}
