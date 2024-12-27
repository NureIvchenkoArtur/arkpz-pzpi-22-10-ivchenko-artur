namespace AutosportTelemetry.DTO;

public class RaceDTO
{
    public int Id { get; set; }
    public int TrackId { get; set; }
    public int LapCount { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public int CreatedById { get; set; }
    public string Status { get; set; } = "planned";
}
