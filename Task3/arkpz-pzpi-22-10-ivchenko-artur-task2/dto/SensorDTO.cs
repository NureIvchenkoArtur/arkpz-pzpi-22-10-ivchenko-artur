namespace AutosportTelemetry.DTO;

public class SensorDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Model { get; set; }
    public bool Status { get; set; } 
    public DateTime RegisteredTime { get; set; }
    public int? CurrentTrackId { get; set; }
    public int? UserId { get; set; } 
}
