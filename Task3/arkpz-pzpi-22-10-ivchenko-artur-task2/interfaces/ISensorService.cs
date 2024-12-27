using AutosportTelemetry.DTO;
using AutosportTelemetry.Models;

namespace AutosportTelemetry.Interfaces;

public interface ISensorService
{
    Task<SensorDTO> RegisterUserForSensorAsync(int id, int userId);
    Task<IEnumerable<SensorDTO>> GetAllSensorsAsync();
    Task<SensorDTO> GetSensorByIdAsync(int id);
    Task<SensorDTO> CreateSensorAsync(CreateSensorDTO createUserDto);
    Task<SensorDTO> UpdateSensorAsync(int id, UpdateSensorDTO createUserDto);
    Task DeleteSensorAsync(int id);
}
