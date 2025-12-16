using Microsoft.EntityFrameworkCore;
using Trevel_help.Data;
using Trevel_help.Models;
using Trevel_help.Services.Interfaces;

namespace Trevel_help.Services;

public class TripService : ITripService
{
    private readonly AppDbContext _context;

    public TripService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Trip>> GetAllAsync()
        => await _context.Trips
            .Include(t => t.Places)
            .ToListAsync();

    public async Task<Trip?> GetByIdAsync(int id)
        => await _context.Trips
            .Include(t => t.Places)
            .FirstOrDefaultAsync(t => t.Id == id);

    public async Task<Trip> CreateAsync(Trip trip)
    {
        _context.Trips.Add(trip);
        await _context.SaveChangesAsync();
        return trip;
    }
}

