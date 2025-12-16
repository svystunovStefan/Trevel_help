using Microsoft.AspNetCore.Mvc;
using Trevel_help.Models;
using Trevel_help.Services.Interfaces;

namespace Trevel_help.Controllers;

[ApiController]
[Route("api/trips/{tripId}/photos")]
public class PhotosController : ControllerBase
{
    private readonly IPhotoService _service;

    public PhotosController(IPhotoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get(int tripId)
        => Ok(await _service.GetByTripAsync(tripId));

    [HttpPost]
    public async Task<IActionResult> Create(int tripId, Photo photo)
    {
        photo.TripId = tripId;
        return Ok(await _service.CreateAsync(photo));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
        => await _service.DeleteAsync(id) ? NoContent() : NotFound();
}
