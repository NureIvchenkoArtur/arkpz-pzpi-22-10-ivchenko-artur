using AutoMapper;
using AutosportTelemetry.DTO;
using AutosportTelemetry.Interfaces;
using AutosportTelemetry.Models;
using AutosportTelemetry.Repositories;

namespace AutosportTelemetry.Services;

public class LapService : ILapService
{
    private readonly ILapRepository _lapRepository;
    private readonly IMapper _mapper;

    public LapService(ILapRepository lapRepository, IMapper mapper)
    {
        _lapRepository = lapRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<LapDTO>> GetAllLapsAsync()
    {
        var laps = await _lapRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<LapDTO>>(laps);
    }

    public async Task<LapDTO> GetLapByIdAsync(int id)
    {
        var lap = await _lapRepository.GetByIdAsync(id);
        return _mapper.Map<LapDTO>(lap);
    }

    public async Task<LapDTO> CreateLapAsync(CreateLapDTO createLapDto)
    {
        var lap = _mapper.Map<Lap>(createLapDto);
        lap.CreatedAt = DateTime.UtcNow;
        await _lapRepository.CreateAsync(lap);
        return _mapper.Map<LapDTO>(lap);
    }

    public async Task<LapDTO> UpdateLapAsync(int id, LapDTO lapDto)
    {
        var existingLap = await _lapRepository.GetByIdAsync(id);
        if (existingLap != null)
        {
            _mapper.Map(lapDto, existingLap);
            await _lapRepository.UpdateAsync(existingLap);
        }
        return _mapper.Map<LapDTO>(existingLap);
    }

    public async Task DeleteLapAsync(int id)
    {
        await _lapRepository.DeleteAsync(id);
    }
}
