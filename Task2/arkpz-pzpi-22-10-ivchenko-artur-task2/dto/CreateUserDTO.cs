using System.Text.Json.Serialization;

namespace AutosportTelemetry.DTO;

[JsonSerializable(typeof(CreateUserDTO))]
public class CreateUserDTO
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
