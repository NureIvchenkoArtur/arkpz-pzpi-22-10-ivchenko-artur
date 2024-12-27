using AutosportTelemetry.Models;

namespace AutosportTelemetry.DTO;

public class CreateTrackDTO
{
    public string Name { get; set; }
    public int CreatedById { get; set; }
    public List<Coordinate> StartFinishCoordinates { get; set; }
    public List<Coordinate>? PathCoordinates { get; set; }
}
