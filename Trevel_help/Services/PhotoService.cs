using Microsoft.EntityFrameworkCore;
using Trevel_help.Data;
using Trevel_help.Models;
using Trevel_help.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trevel_help.Services;

public class PhotoService : IPhotoService
{
    private readonly AppDbContext _context;

    public PhotoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Photo>> GetByTripAsync(int tripId)
    {
        return await _context.Photos
            .Include(p => p.Place)
            .Where(p => p.Place.TripId == tripId)
            .ToListAsync();
    }

    public async Task<Photo?> GetByIdAsync(int id)
    {
        return await _context.Photos.FindAsync(id);
    }

    public async Task<Photo> CreateAsync(Photo photo)
    {
        _context.Photos.Add(photo);
        await _context.SaveChangesAsync();
        return photo;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var photo = await _context.Photos.FindAsync(id);
        if (photo == null) return false;

        _context.Photos.Remove(photo);
        await _context.SaveChangesAsync();
        return true;
    }
}

