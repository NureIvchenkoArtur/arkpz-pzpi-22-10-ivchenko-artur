using AutosportTelemetry.DTO;
using AutosportTelemetry.Interfaces;
using AutosportTelemetry.Models;
using AutoMapper;
using AutosportTelemetry.Repositories;

namespace AutosportTelemetry.Services;

public class SensorService : ISensorService
{
    private readonly ISensorRepository _sensorRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public SensorService(ISensorRepository sensorRepository,IUserRepository userRepository, IMapper mapper)
    {
        _sensorRepository = sensorRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<SensorDTO> RegisterUserForSensorAsync(int id, int userId)
    {
        var sensor = await _sensorRepository.GetByIdAsync(id);
        if (sensor == null)
            throw new Exception("Датчик не знайден.");

        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            throw new Exception("Користувач не знайден.");

        sensor.UserId = user.Id;
        await _sensorRepository.UpdateAsync(sensor);

        return _mapper.Map<SensorDTO>(sensor);
    }

    public async Task<IEnumerable<SensorDTO>> GetAllSensorsAsync()
    {
        var sensors = await _sensorRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<SensorDTO>>(sensors);
    }

    public async Task<SensorDTO> GetSensorByIdAsync(int id)
    {
        var sensor = await _sensorRepository.GetByIdAsync(id);
        return _mapper.Map<SensorDTO>(sensor);
    }

    public async Task<SensorDTO> CreateSensorAsync(CreateSensorDTO createSensorDto)
    {
        var sensor = _mapper.Map<Sensor>(createSensorDto);
        sensor.RegisteredTime = DateTime.UtcNow;
        await _sensorRepository.CreateAsync(sensor);
        return _mapper.Map<SensorDTO>(sensor);
    }

    public async Task<SensorDTO> UpdateSensorAsync(int id, UpdateSensorDTO updateSensorDto)
    {
        var existingSensor = await _sensorRepository.GetByIdAsync(id);
        if (existingSensor != null)
        {
            _mapper.Map(updateSensorDto, existingSensor);
            await _sensorRepository.UpdateAsync(existingSensor);
        }
        return _mapper.Map<SensorDTO>(existingSensor);
    }

    public async Task DeleteSensorAsync(int id)
    {
        await _sensorRepository.DeleteAsync(id);
    }
}
