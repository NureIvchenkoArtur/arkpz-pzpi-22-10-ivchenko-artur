﻿using AutosportTelemetry.Models;
using AutosportTelemetry.Config;
using AutosportTelemetry.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace AutosportTelemetry.Repositories;

public class UserRepository : IUserRepository 
{
    private readonly TelemetryDbContext _context;

    public UserRepository(TelemetryDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User> GetByIdAsync(int id)
    {
        return await _context.Users
            .Include(u => u.Sensors)
            .Include(u => u.Races)
            .Include(u => u.Tracks)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task CreateAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var user = await GetByIdAsync(id);
        if (user != null)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
