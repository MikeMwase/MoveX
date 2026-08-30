namespace MoveX.Domain.Entities.Operations;

public class DriverLocation
{
    public long Id { get; set; }
    public int DriverId { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double? SpeedKph { get; set; }
    public double? HeadingDegrees { get; set; }
    public double? AccuracyMeters { get; set; }
    public DateTime RecordedAt { get; set; } = DateTime.UtcNow;
}
