namespace MoveX.Domain.Entities.Drivers;

public class VehicleType
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public decimal MaximumLoadKg { get; set; }
    public decimal? MaximumVolumeM3 { get; set; }
    public bool IsActive { get; set; } = true;

    public ICollection<Vehicle> Vehicles { get; set; } = new List<Vehicle>();
}
