using AutosportTelemetry.Models;

namespace AutosportTelemetry.Interfaces;

public interface ISensorRepository
{
    Task<IEnumerable<Sensor>> GetAllAsync();
    Task<Sensor> GetByIdAsync(int id);
    Task CreateAsync(Sensor sensor);
    Task UpdateAsync(Sensor sensor);
    Task UpdateRangeAsync(IEnumerable<Sensor> sensors);
    Task DeleteAsync(int id);
}

