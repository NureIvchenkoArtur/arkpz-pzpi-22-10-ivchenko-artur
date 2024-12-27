using AutosportTelemetry.DTO;
using AutosportTelemetry.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AutosportTelemetry.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class TrackController : ControllerBase
{
    private readonly ITrackService _trackService;

    public TrackController(ITrackService trackService)
    {
        _trackService = trackService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tracks = await _trackService.GetAllTracksAsync();
        return Ok(tracks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var track = await _trackService.GetTrackByIdAsync(id);
        if (track == null)
            return NotFound();

        return Ok(track);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateTrackDTO createTrackDto)
    {
        try
        {
            var track = await _trackService.CreateTrackAsync(createTrackDto);
            if (track == null)
                return NotFound();
            return CreatedAtAction(nameof(GetById), new { id = track.Id }, track);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CreateTrackDTO createTrackDto)
    {
        var track = await _trackService.UpdateTrackAsync(id, createTrackDto);
        return Ok(track);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _trackService.DeleteTrackAsync(id);
        return NoContent();
    }
}
