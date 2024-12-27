using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AutosportTelemetry.Models;

[Table("track")]
public class Track
{
    [Column("id")]
    [Required]
    public int Id { get; set; }

    [Column("name")]
    [Required]
    public string Name { get; set; }

    [Column("user_data_id")]
    [Required]
    public int CreatedById { get; set; }

    public User User { get; set; }

    [Column("start_finish_points", TypeName = "jsonb")]
    [Required]
    public List<Coordinate> StartFinishCoordinates { get; set; }

    [Column("trajectory_points", TypeName = "jsonb")]
    public List<Coordinate>? PathCoordinates { get; set; }


    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    public ICollection<Sensor> Sensors { get; set; }
    public ICollection<Race> Races { get; set; }
}
