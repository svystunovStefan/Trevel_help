using Trevel_help.Models;

namespace Trevel_help.Services.Interfaces;

public interface IPlaceService
{
    Task<List<Place>> GetByTripAsync(int tripId);
    Task<Place> CreateAsync(Place place);
}
