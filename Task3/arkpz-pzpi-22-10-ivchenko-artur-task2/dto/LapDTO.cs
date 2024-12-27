using AutosportTelemetry.Models;

namespace AutosportTelemetry.DTO;

public class LapDTO
{
    public int Id { get; set; }
    public int RaceId { get; set; }
    public int SensorId { get; set; }
    public decimal? LapTime { get; set; }
    public List<Coordinate>? BrakingPoints { get; set; }
    public List<Coordinate>? AccelerationPoints { get; set; }
    public DateTime? CreatedAt { get; set; }
    public int LapNumber { get; set; } = 1;
}
