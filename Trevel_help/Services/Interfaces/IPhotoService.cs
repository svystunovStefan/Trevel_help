using Trevel_help.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Trevel_help.Services.Interfaces;

public interface IPhotoService
{
    Task<List<Photo>> GetByTripAsync(int tripId);
    Task<Photo?> GetByIdAsync(int id);
    Task<Photo> CreateAsync(Photo photo);
    Task<bool> DeleteAsync(int id);
}

