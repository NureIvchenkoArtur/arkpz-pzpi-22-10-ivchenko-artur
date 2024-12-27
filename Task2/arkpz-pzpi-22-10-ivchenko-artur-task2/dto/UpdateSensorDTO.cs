namespace AutosportTelemetry.DTO;

public class UpdateSensorDTO
{
    public string Name { get; set; }
    public string Model { get; set; }
    public int? CurrentTrackId { get; set; }
    public int? UserId { get; set; }
}
