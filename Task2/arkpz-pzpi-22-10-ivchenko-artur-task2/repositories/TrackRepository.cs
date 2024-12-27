﻿using AutosportTelemetry.Config;
using AutosportTelemetry.Interfaces;
using AutosportTelemetry.Models;
using Microsoft.EntityFrameworkCore;

namespace AutosportTelemetry.Repositories;

public class TrackRepository : ITrackRepository
{
    private readonly TelemetryDbContext _context;

    public TrackRepository(TelemetryDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Track>> GetAllAsync()
    {
        return await _context.Tracks.ToListAsync();
    }

    public async Task<Track> GetByIdAsync(int id)
    {
        return await _context.Tracks
            .Include(t => t.Sensors)
            .Include(t => t.Races)
            .Include(t => t.User)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task CreateAsync(Track track)
    {
        await _context.Tracks.AddAsync(track);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Track track)
    {
        _context.Tracks.Update(track);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var track = await GetByIdAsync(id);
        if (track != null)
        {
            _context.Tracks.Remove(track);
            await _context.SaveChangesAsync();
        }
    }
}
