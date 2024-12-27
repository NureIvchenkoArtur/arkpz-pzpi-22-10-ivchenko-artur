namespace AutosportTelemetry.DTO;

public class RaceReportDTO
{
    public int RaceId { get; set; }
    public DateTime? RaceDate { get; set; }
    public string TrackName { get; set; }
    public int LapCount { get; set; }
    public decimal? TotalRaceTime { get; set; }
    public List<SensorReportDTO> Sensors { get; set; } = new List<SensorReportDTO>();
}
