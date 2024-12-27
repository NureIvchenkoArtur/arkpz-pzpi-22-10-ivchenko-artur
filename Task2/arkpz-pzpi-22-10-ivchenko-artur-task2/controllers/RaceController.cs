using AutosportTelemetry.DTO;
using AutosportTelemetry.Interfaces;
using AutosportTelemetry.Services;
using Microsoft.AspNetCore.Mvc;

namespace AutosportTelemetry.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class RaceController : ControllerBase
{
    private readonly IRaceService _raceService;

    public RaceController(IRaceService raceService)
    {
        _raceService = raceService;
    }

    [HttpGet("{id}/GetReport")]
    public async Task<IActionResult> GetReport(int id)
    {
        try
        {
            var report = await _raceService.GenerateReportAsync(id);
            return Ok(report);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}/start")]
    public async Task<IActionResult> StartRace(int id)
    {
        try
        {
            var race = await _raceService.StartRaceAsync(id);
            return Ok(race);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}/finish")]
    public async Task<IActionResult> FinishRace(int id)
    {
        try
        {
            var race = await _raceService.FinishRaceAsync(id);
            return Ok(race);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var races = await _raceService.GetAllRacesAsync();
        return Ok(races);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var race = await _raceService.GetRaceByIdAsync(id);
        if (race == null)
            return NotFound();

        return Ok(race);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateRaceDTO createRaceDto)
    {
        try
        {
            var race = await _raceService.CreateRaceAsync(createRaceDto);
            if (race == null)
                return NotFound();
            return CreatedAtAction(nameof(GetById), new { id = race.Id }, race);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, RaceDTO raceDto)
    {
        var track = await _raceService.UpdateRaceAsync(id, raceDto);
        return Ok(track);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _raceService.DeleteRaceAsync(id);
        return NoContent();
    }
}

