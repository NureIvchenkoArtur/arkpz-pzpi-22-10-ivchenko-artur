using AutosportTelemetry.Models;

namespace AutosportTelemetry.DTO;

public class TrackDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CreatedById { get; set; }
    public List<Coordinate> StartFinishCoordinates { get; set; }
    public List<Coordinate>? PathCoordinates { get; set; }
    public DateTime CreatedAt { get; set; }
}
