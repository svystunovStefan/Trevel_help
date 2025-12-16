using Trevel_help.Models;

namespace Trevel_help.Services.Interfaces;

public interface IPhotoService
{
    Task<List<Photo>> GetByTripAsync(int tripId);
    Task<Photo> CreateAsync(Photo photo);
    Task<bool> DeleteAsync(int id);
}
