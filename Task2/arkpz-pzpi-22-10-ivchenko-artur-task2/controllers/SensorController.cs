using AutosportTelemetry.DTO;
using AutosportTelemetry.Interfaces;
using AutosportTelemetry.Models;
using AutosportTelemetry.Services;
using Microsoft.AspNetCore.Mvc;

namespace AutosportTelemetry.Controllers;


[ApiController]
[Route("api/v1/[controller]")]
public class SensorController : ControllerBase
{
    private readonly ISensorService _sensorService;

    public SensorController(ISensorService sensorService)
    {
        _sensorService = sensorService;
    }

    [HttpPut("{id}/register")]
    public async Task<IActionResult> RegisterUserForSensor(int id, RegisterSensorDTO registerSensorDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var sensor = await _sensorService.RegisterUserForSensorAsync(id, registerSensorDto.UserId);
            return Ok(sensor);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var sensors = await _sensorService.GetAllSensorsAsync();
        return Ok(sensors);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var sensor = await _sensorService.GetSensorByIdAsync(id);
        if (sensor == null)
            return NotFound();

        return Ok(sensor);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateSensorDTO createSensorDto)
    {
        var sensor = await _sensorService.CreateSensorAsync(createSensorDto);
        if (sensor == null)
            return NotFound();
        return CreatedAtAction(nameof(GetById), new { id = sensor.Id }, sensor);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UpdateSensorDTO updateSensorDto)
    {
        var sensor = await _sensorService.UpdateSensorAsync(id, updateSensorDto);
        return CreatedAtAction(nameof(GetById), new { id = sensor.Id }, sensor);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _sensorService.DeleteSensorAsync(id);
        return NoContent();
    }
}

