using AutosportTelemetry.Models;

namespace AutosportTelemetry.Interfaces;

public interface ILapRepository
{
    Task<IEnumerable<Lap>> GetAllAsync();
    Task<Lap> GetByIdAsync(int id);
    Task CreateAsync(Lap lap);
    Task UpdateAsync(Lap lap);
    Task DeleteAsync(int id);
}
