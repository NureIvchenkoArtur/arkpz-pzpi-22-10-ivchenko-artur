using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AutosportTelemetry.Models;

[Table("user_data")]
public class User
{
    [Column("id")]
    [Required]
    public int Id { get; set; }

    [Column("name")]
    [Required]
    public string Name { get; set; }

    [Column("email")]
    [Required]
    public string Email { get; set; }

    [Column("password_hash")]
    [Required]
    public string PasswordHash { get; set; }

    [Column("registered_at")]
    [Required]
    public DateTime RegisteredTime { get; set; }

    [Column("role")]
    [Required]
    public string Role { get; set; } = "user";

    public ICollection<Sensor> Sensors { get; set; }
    public ICollection<Race> Races { get; set; }
    public ICollection<Track> Tracks { get; set; }
}
