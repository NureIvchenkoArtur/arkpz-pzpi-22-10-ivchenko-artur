using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace AutosportTelemetry.Models;

[Table("lap")]
public class Lap
{
    [Column("id")]
    [Required]
    public int Id { get; set; }

    [Column("race_id")]
    [Required]
    public int RaceId { get; set; }
    public Race Race { get; set; }

    [Column("sensor_id")]
    [Required]
    public int SensorId { get; set; }
    public Sensor Sensor { get; set; }

    [Column("lap_time")]
    [Required]
    public decimal? LapTime { get; set; }

    [Column("braking_points", TypeName = "jsonb")]
    public List<Coordinate>? BrakingPoints { get; set; }

    [Column("acceleration_points", TypeName = "jsonb")]
    public List<Coordinate>? AccelerationPoints { get; set; }

    [Column("created_at")]
    public DateTime? CreatedAt { get; set; }

    [Column("lap_number")]
    [Required]
    public int LapNumber { get; set; } = 1;
}
