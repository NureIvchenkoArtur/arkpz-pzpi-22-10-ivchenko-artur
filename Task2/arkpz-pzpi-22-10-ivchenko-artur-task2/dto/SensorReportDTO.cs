namespace AutosportTelemetry.DTO;

public class SensorReportDTO
{
    public string SensorName { get; set; }
    public int CompletedLaps { get; set; }
    public decimal? BestLapTime { get; set; }
}
