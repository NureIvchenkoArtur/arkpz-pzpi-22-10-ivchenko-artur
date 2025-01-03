﻿namespace AutosportTelemetry.DTO;

public class UserDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public DateTime RegisteredTime { get; set; }
    public string Role { get; set; }
}
