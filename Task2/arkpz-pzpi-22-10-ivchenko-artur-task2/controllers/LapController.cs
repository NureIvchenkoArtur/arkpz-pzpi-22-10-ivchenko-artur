using AutosportTelemetry.DTO;
using AutosportTelemetry.Interfaces;
using AutosportTelemetry.Services;
using Microsoft.AspNetCore.Mvc;

namespace AutosportTelemetry.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class LapController : ControllerBase
{
    private readonly ILapService _lapService;

    public LapController(ILapService lapService)
    {
        _lapService = lapService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var laps = await _lapService.GetAllLapsAsync();
        return Ok(laps);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var lap = await _lapService.GetLapByIdAsync(id);
        if (lap == null)
            return NotFound();

        return Ok(lap);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateLapDTO createLapDto)
    {
        var lap = await _lapService.CreateLapAsync(createLapDto);
        if (lap == null)
            return NotFound();
        return CreatedAtAction(nameof(GetById), new { id = lap.Id }, lap);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, LapDTO lapDto)
    {
        var lap = await _lapService.UpdateLapAsync(id, lapDto);
        return Ok(lap);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _lapService.DeleteLapAsync(id);
        return NoContent();
    }
}
