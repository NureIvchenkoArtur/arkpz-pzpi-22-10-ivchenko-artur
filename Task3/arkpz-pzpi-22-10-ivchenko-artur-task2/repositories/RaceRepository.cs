using AutosportTelemetry.Config;
using AutosportTelemetry.Interfaces;
using AutosportTelemetry.Models;
using Microsoft.EntityFrameworkCore;

namespace AutosportTelemetry.Repositories;

public class RaceRepository : IRaceRepository
{
    private readonly TelemetryDbContext _context;

    public RaceRepository(TelemetryDbContext context)
    {
        _context = context;
    }

    public async Task<Race> GetRaceWithDetailsAsync(int id)
    {
        return await _context.Races
            .Include(r => r.Track)
            .Include(r => r.Laps)
                .ThenInclude(l => l.Sensor)
            .FirstOrDefaultAsync(r => r.Id == id);
    }


    public async Task<IEnumerable<Race>> GetAllAsync()
    {
        return await _context.Races.ToListAsync();
    }

    public async Task<Race> GetByIdAsync(int id)
    {
        return await _context.Races
            .Include(r => r.Laps)
            .Include(r => r.User)
            .Include(r => r.Track)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task CreateAsync(Race race)
    {
        await _context.Races.AddAsync(race);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Race race)
    {
        _context.Races.Update(race);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var race = await GetByIdAsync(id);
        if (race != null)
        {
            _context.Races.Remove(race);
            await _context.SaveChangesAsync();
        }
    }
}
