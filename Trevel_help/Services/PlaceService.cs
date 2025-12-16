using Microsoft.EntityFrameworkCore;
using Trevel_help.Data;
using Trevel_help.Models;
using Trevel_help.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class PlaceService : IPlaceService
{
    private readonly AppDbContext _context;

    public PlaceService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Place>> GetByTripAsync(int tripId)
    {
        return await _context.Places
            .Where(p => p.TripId == tripId)
            .ToListAsync();
    }

    public async Task<Place> CreateAsync(Place place)
    {
        _context.Places.Add(place);
        await _context.SaveChangesAsync();
        return place;
    }

    public async Task<bool> DeleteAsync(int placeId)
    {
        var place = await _context.Places.FindAsync(placeId);
        if (place == null) return false;

        _context.Places.Remove(place);
        await _context.SaveChangesAsync();
        return true;
    }
}

