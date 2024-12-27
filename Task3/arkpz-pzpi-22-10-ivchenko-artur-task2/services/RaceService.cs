using AutoMapper;
using AutosportTelemetry.DTO;
using AutosportTelemetry.Interfaces;
using AutosportTelemetry.Models;
using AutosportTelemetry.Repositories;

namespace AutosportTelemetry.Services;

public class RaceService : IRaceService
{
    private readonly IRaceRepository _raceRepository;
    private readonly IUserRepository _userRepository;
    private readonly ISensorRepository _sensorRepository;
    private readonly IMapper _mapper;

    public RaceService(IRaceRepository raceRepository, IUserRepository userRepository, ISensorRepository sensorRepository,  IMapper mapper)
    {
        _raceRepository = raceRepository;
        _userRepository = userRepository;
        _sensorRepository = sensorRepository;
        _mapper = mapper;
    }

    public async Task<RaceReportDTO> GenerateReportAsync(int id)
    {
        var race = await _raceRepository.GetRaceWithDetailsAsync(id);
        if (race == null)
            throw new Exception("Гонка не знайдена.");

        var raceReport = new RaceReportDTO
        {
            RaceId = race.Id,
            RaceDate = race.StartTime,
            TrackName = race.Track.Name,
            LapCount = race.LapCount,
            TotalRaceTime = race.EndTime.HasValue && race.StartTime.HasValue
                ? (decimal)(race.EndTime.Value - race.StartTime.Value).TotalSeconds
                : null
        };

        var sensorReports = race.Laps
            .GroupBy(l => l.Sensor)
            .Select(g => new SensorReportDTO
            {
                SensorName = g.Key.Name,
                CompletedLaps = g.Count(),
                BestLapTime = g.Min(l => l.LapTime)
            })
            .OrderBy(r => r.BestLapTime) 
            .ToList();

        raceReport.Sensors = sensorReports;

        return raceReport;
    }

    public async Task<RaceDTO> StartRaceAsync(int id)
    {
        var race = await _raceRepository.GetByIdAsync(id);
        if (race == null)
            throw new Exception("Заїзд не знайден.");

        if (race.Status != "planned")
            throw new InvalidOperationException("Заїзд уже стартував або завершений.");

        race.StartTime = DateTime.UtcNow;
        race.Status = "ongoing";

        var user = await _userRepository.GetByIdAsync(race.CreatedById);
        var sensors = user.Sensors;

        if (sensors != null && sensors.Any())
        {
            foreach (var sensor in sensors)
            {
                sensor.CurrentTrackId = race.TrackId; 
                sensor.Status = true;             
            }
            await _sensorRepository.UpdateRangeAsync(sensors);
        }

        await _raceRepository.UpdateAsync(race);

        return _mapper.Map<RaceDTO>(race);
    }

    public async Task<RaceDTO> FinishRaceAsync(int id)
    {
        var race = await _raceRepository.GetByIdAsync(id);
        if (race == null)
            throw new Exception("Заїзд не знайден.");

        if (race.Status != "ongoing")
            throw new InvalidOperationException("Заїзд не в процесі.");

        race.EndTime = DateTime.UtcNow;
        race.Status = "finished";

        var user = await _userRepository.GetByIdAsync(race.CreatedById);
        var sensors = user.Sensors;

        if (sensors != null && sensors.Any())
        {
            foreach (var sensor in sensors)
            {
                sensor.Status = false;
            }
            await _sensorRepository.UpdateRangeAsync(sensors);
        }

        await _raceRepository.UpdateAsync(race);

        return _mapper.Map<RaceDTO>(race);
    }

    public async Task<IEnumerable<RaceDTO>> GetAllRacesAsync()
    {
        var race = await _raceRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<RaceDTO>>(race);
    }

    public async Task<RaceDTO> GetRaceByIdAsync(int id)
    {
        var race = await _raceRepository.GetByIdAsync(id);
        return _mapper.Map<RaceDTO>(race);
    }

    public async Task<RaceDTO> CreateRaceAsync(CreateRaceDTO createRaceDto)
    {
        var race = _mapper.Map<Race>(createRaceDto);
        race.CreatedAt = DateTime.UtcNow;
        await _raceRepository.CreateAsync(race);
        return _mapper.Map<RaceDTO>(race);
    }

    public async Task<RaceDTO> UpdateRaceAsync(int id, RaceDTO raceDto)
    {
        var existingRace = await _raceRepository.GetByIdAsync(id);
        if (existingRace != null)
        {
            _mapper.Map(raceDto, existingRace);
            await _raceRepository.UpdateAsync(existingRace);
        }
        return _mapper.Map<RaceDTO>(existingRace);
    }

    public async Task DeleteRaceAsync(int id)
    {
        await _raceRepository.DeleteAsync(id);
    }
}
