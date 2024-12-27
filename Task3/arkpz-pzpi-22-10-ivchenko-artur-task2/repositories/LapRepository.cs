using AutosportTelemetry.Config;
using AutosportTelemetry.Interfaces;
using AutosportTelemetry.Models;
using Microsoft.EntityFrameworkCore;

namespace AutosportTelemetry.Repositories;

public class LapRepository  : ILapRepository
{
    private readonly TelemetryDbContext _context;

    public LapRepository(TelemetryDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Lap>> GetAllAsync()
    {
        return await _context.Laps.ToListAsync();
    }

    public async Task<Lap> GetByIdAsync(int id)
    {
        return await _context.Laps
            .Include(u => u.Sensor)
            .Include(u => u.Race)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task CreateAsync(Lap lap)
    {
        await _context.Laps.AddAsync(lap);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Lap lap)
    {
        _context.Laps.Update(lap);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var lap = await GetByIdAsync(id);
        if (lap != null)
        {
            _context.Laps.Remove(lap);
            await _context.SaveChangesAsync();
        }
    }
}
