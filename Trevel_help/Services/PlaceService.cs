using Microsoft.EntityFrameworkCore;
using Trevel_help.Data;
using Trevel_help.Models;
using Trevel_help.Services.Interfaces;

namespace Trevel_help.Services;

public class PlaceService : IPlaceService
{
    private readonly AppDbContext _context;

    public PlaceService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Place>> GetByTripAsync(int tripId)
        => await _context.Places
            .Where(p => p.TripId == tripId)
            .Include(p => p.Photos)
            .ToListAsync();

    public async Task<Place> CreateAsync(Place place)
    {
        _context.Places.Add(place);
        await _context.SaveChangesAsync();
        return place;
    }
}
