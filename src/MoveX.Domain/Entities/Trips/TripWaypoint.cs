namespace MoveX.Domain.Entities.Trips;

public class TripWaypoint
{
    public long Id { get; set; }
    public long TripId { get; set; }
    public int Sequence { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public string? Address { get; set; }
    public DateTime? ArrivedAt { get; set; }
    public DateTime? DepartedAt { get; set; }

    public Trip? Trip { get; set; }
}
