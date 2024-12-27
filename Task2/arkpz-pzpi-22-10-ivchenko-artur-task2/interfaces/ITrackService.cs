using AutosportTelemetry.DTO;
using AutosportTelemetry.Models;

namespace AutosportTelemetry.Interfaces;

public interface ITrackService
{
    Task<IEnumerable<TrackDTO>> GetAllTracksAsync();
    Task<TrackDTO> GetTrackByIdAsync(int id);
    Task<TrackDTO> CreateTrackAsync(CreateTrackDTO raceSensor);
    Task<TrackDTO> UpdateTrackAsync(int id, CreateTrackDTO raceSensor);
    Task DeleteTrackAsync(int id);
}
