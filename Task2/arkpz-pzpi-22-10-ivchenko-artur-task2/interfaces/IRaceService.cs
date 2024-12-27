using AutosportTelemetry.DTO;

namespace AutosportTelemetry.Interfaces;

public interface IRaceService
{
    Task<RaceReportDTO> GenerateReportAsync(int id);
    Task<RaceDTO> StartRaceAsync(int id);
    Task<RaceDTO> FinishRaceAsync(int id);
    Task<IEnumerable<RaceDTO>> GetAllRacesAsync();
    Task<RaceDTO> GetRaceByIdAsync(int id);
    Task<RaceDTO> CreateRaceAsync(CreateRaceDTO createRaceDto);
    Task<RaceDTO> UpdateRaceAsync(int id, RaceDTO createRaceDto);
    Task DeleteRaceAsync(int id);
}
