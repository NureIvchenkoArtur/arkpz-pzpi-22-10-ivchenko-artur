using AutosportTelemetry.DTO;

namespace AutosportTelemetry.Interfaces;

public interface ILapService
{
    Task<IEnumerable<LapDTO>> GetAllLapsAsync();
    Task<LapDTO> GetLapByIdAsync(int id);
    Task<LapDTO> CreateLapAsync(CreateLapDTO createLapDto);
    Task<LapDTO> UpdateLapAsync(int id, LapDTO createLapDto);
    Task DeleteLapAsync(int id);
}
