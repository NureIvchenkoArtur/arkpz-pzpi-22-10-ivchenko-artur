using AutosportTelemetry.Models;
using AutosportTelemetry.Config;
using AutosportTelemetry.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AutosportTelemetry.Repositories;

public class SensorRepository : ISensorRepository
{
    private readonly TelemetryDbContext _context;

    public SensorRepository(TelemetryDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Sensor>> GetAllAsync()
    {
        return await _context.Sensors.ToListAsync();
    }

    public async Task<Sensor> GetByIdAsync(int id)
    {
        return await _context.Sensors
            .Include(s => s.Laps)
            .Include(s => s.Track)
            .Include(s => s.User)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task CreateAsync(Sensor sensor)
    {
        await _context.Sensors.AddAsync(sensor);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Sensor sensor)
    {
        _context.Sensors.Update(sensor);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateRangeAsync(IEnumerable<Sensor> sensors)
    {
        _context.Sensors.UpdateRange(sensors);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var sensor = await GetByIdAsync(id);
        if (sensor != null)
        {
            _context.Sensors.Remove(sensor);
            await _context.SaveChangesAsync();
        }
    }
}
