using Microsoft.AspNetCore.Mvc;
using Moq;
using Trevel_help.Controllers;
using Trevel_help.Models;
using Trevel_help.Services.Interfaces;
using Xunit;

namespace Trevel_help.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TripsController : ControllerBase
{
    private readonly ITripService _tripService;

    public TripsController(ITripService tripService)
    {
        _tripService = tripService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Trip>>> GetAll()
    {
        var trips = await _tripService.GetAllAsync();
        return Ok(trips);
    }

    [HttpPost]
    public async Task<ActionResult<Trip>> CreateTrip(Trip trip)
    {
        var created = await _tripService.CreateAsync(trip);
        return CreatedAtAction(nameof(GetAll), new { id = created.Id }, created);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTrip(int id)
    {
        await _tripService.DeleteAsync(id);
        return NoContent();
    }
}

