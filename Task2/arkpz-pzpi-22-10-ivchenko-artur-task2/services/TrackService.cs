using AutosportTelemetry.DTO;
using AutosportTelemetry.Interfaces;
using AutosportTelemetry.Models;
using AutoMapper;
using System.Runtime.ConstrainedExecution;

namespace AutosportTelemetry.Services;

public class TrackService : ITrackService
{
    private readonly ITrackRepository _trackRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public TrackService(ITrackRepository trackRepository, IUserRepository userRepository, IMapper mapper)
    {
        _trackRepository = trackRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TrackDTO>> GetAllTracksAsync()
    {
        var tracks = await _trackRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<TrackDTO>>(tracks);
    }

    public async Task<TrackDTO> GetTrackByIdAsync(int id)
    {
        var track = await _trackRepository.GetByIdAsync(id);
        return _mapper.Map<TrackDTO>(track);
    }

    public async Task<TrackDTO> CreateTrackAsync(CreateTrackDTO createTrackDto)
    {
        var track = _mapper.Map<Track>(createTrackDto);
        track.CreatedAt = DateTime.UtcNow;
        await _trackRepository.CreateAsync(track);
        return _mapper.Map<TrackDTO>(track);
    }

    public async Task<TrackDTO> UpdateTrackAsync(int id, CreateTrackDTO updateTrackDto)
    {
        var existingTrack = await _trackRepository.GetByIdAsync(id);
        if (existingTrack != null)
        {
            _mapper.Map(updateTrackDto, existingTrack);
            await _trackRepository.UpdateAsync(existingTrack);
        }
        return _mapper.Map<TrackDTO>(existingTrack);
    }

    public async Task DeleteTrackAsync(int id)
    {
        await _trackRepository.DeleteAsync(id);
    }
}
