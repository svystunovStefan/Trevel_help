using Microsoft.AspNetCore.Mvc;
using Trevel_help.Models;
using Trevel_help.Services.Interfaces;

namespace Trevel_help.Controllers;

[ApiController]
[Route("api/trips/{tripId}/places")]
public class PlacesController : ControllerBase
{
    private readonly IPlaceService _service;

    public PlacesController(IPlaceService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get(int tripId)
        => Ok(await _service.GetByTripAsync(tripId));

    [HttpPost]
    public async Task<IActionResult> Create(int tripId, Place place)
    {
        place.TripId = tripId;
        return Ok(await _service.CreateAsync(place));
    }
}
