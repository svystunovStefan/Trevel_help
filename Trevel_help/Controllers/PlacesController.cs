using Microsoft.AspNetCore.Mvc;
using Trevel_help.Models;
using Trevel_help.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Trevel_help.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlacesController : ControllerBase
    {
        private readonly IPlaceService _service;

        public PlacesController(IPlaceService service)
        {
            _service = service;
        }

        [HttpGet("{tripId}")]
        public async Task<ActionResult<List<Place>>> GetByTrip(int tripId)
        {
            var places = await _service.GetByTripAsync(tripId);
            return Ok(places); 
        }

        [HttpPost]
        public async Task<ActionResult<Place>> Create(Place place)
        {
            var created = await _service.CreateAsync(place);
            return Ok(created); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}
