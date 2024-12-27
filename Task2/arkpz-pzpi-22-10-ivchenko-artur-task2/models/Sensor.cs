using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutosportTelemetry.Models;

[Table("sensor")]
public class Sensor
{
    [Column("id")]
    [Required]
    public int Id { get; set; }

    [Column("name")]
    [Required]
    public string Name { get; set; } = "guest";

    [Column("user_data_id")]
    public int? UserId { get; set; }
    public User User { get; set; }

    [Column("model")]
    [Required]
    public string Model { get; set; } = "v1";

    [Column("is_active")]
    [Required]
    public bool Status { get; set; } = false;

    [Column("current_track_id")]
    public int? CurrentTrackId { get; set; }
    public Track Track { get; set; }

    [Column("registered_at")]
    public DateTime RegisteredTime { get; set; }

    public ICollection<Lap> Laps { get; set; }
}
