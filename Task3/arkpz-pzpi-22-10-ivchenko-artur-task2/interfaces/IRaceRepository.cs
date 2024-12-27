using AutosportTelemetry.Models;

namespace AutosportTelemetry.Interfaces;

public interface IRaceRepository
{
    Task<Race> GetRaceWithDetailsAsync(int id);
    Task<IEnumerable<Race>> GetAllAsync();
    Task<Race> GetByIdAsync(int id);
    Task CreateAsync(Race race);
    Task UpdateAsync(Race race);
    Task DeleteAsync(int id);
}
