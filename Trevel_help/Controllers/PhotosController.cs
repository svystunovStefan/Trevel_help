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
    {
        var photos = await _service.GetByTripAsync(tripId);
        return Ok(photos);
    }

    [HttpPost]
    public async Task<IActionResult> Create(int tripId, Photo photo)
    {
        var created = await _service.CreateAsync(photo);
        return Ok(created);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
