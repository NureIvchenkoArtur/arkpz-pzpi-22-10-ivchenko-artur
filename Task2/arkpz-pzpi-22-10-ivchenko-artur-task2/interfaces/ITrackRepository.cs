using AutosportTelemetry.Models;

namespace AutosportTelemetry.Interfaces;

public interface ITrackRepository
{
    Task<IEnumerable<Track>> GetAllAsync();
    Task<Track> GetByIdAsync(int id);
    Task CreateAsync(Track track);
    Task UpdateAsync(Track track);
    Task DeleteAsync(int id);
}
