namespace MoveX.Domain.Entities.Services;

public class MovingService
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public PricingUnit PricingUnit { get; set; } = PricingUnit.Fixed;
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}

public enum PricingUnit
{
    Fixed,
    PerItem,
    PerHour,
    PerKm
}
