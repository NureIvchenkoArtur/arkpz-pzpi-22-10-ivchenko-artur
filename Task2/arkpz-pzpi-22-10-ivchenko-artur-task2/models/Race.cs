using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutosportTelemetry.Models;

[Table("race")]
public class Race
{
    [Column("id")]
    [Required]
    public int Id { get; set; }

    [Column("track_id")]
    [Required]
    public int TrackId { get; set; }
    public Track Track { get; set; }

    [Column("lap_count")]
    public int LapCount { get; set; }

    [Column("started_at")]
    public DateTime? StartTime { get; set; }

    [Column("finished_at")]
    public DateTime? EndTime { get; set; }

    [Column("user_data_id")]
    [Required]
    public int CreatedById { get; set; }
    public User User { get; set; }

    [Column("status")]
    [Required]
    public string Status { get; set; } = "planned";

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    public ICollection<Lap> Laps { get; set; }
}

