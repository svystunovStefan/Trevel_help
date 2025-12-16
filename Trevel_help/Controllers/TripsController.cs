using Microsoft.AspNetCore.Mvc;
using Trevel_help.Models;
using Trevel_help.Services.Interfaces;

namespace Trevel_help.Controllers;

[ApiController]
[Route("api/trips")]
public class TripsController : ControllerBase
{
    private readonly ITripService _service;

    public TripsController(ITripService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
        => Ok(await _service.GetAllAsync());

    [HttpPost]
    public async Task<IActionResult> Create(Trip trip)
        => Ok(await _service.CreateAsync(trip));
}
