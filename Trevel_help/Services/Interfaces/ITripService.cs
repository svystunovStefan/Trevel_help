using Trevel_help.Models;

namespace Trevel_help.Services.Interfaces;

public interface ITripService
{
    Task<List<Trip>> GetAllAsync();
    Task<Trip?> GetByIdAsync(int id);
    Task<Trip> CreateAsync(Trip trip);
}
