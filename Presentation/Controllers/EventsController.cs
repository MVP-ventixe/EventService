using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Business.Services;
using Business.Models;

namespace Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController(IEventService eventService) : ControllerBase
    {
        private readonly IEventService _eventService = eventService;

        [HttpGet]
        public async Task<IActionResult> GetAllEvents()
        {
            var events = await _eventService.GetAllEventsAsync();
            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEventById(string id)
        {
            var eventModel = await _eventService.GetEventByIdAsync(id);
            if (eventModel == null)
            {
                return NotFound();
            }
            return Ok(eventModel);
        }

        [HttpPost]

        public async Task<IActionResult> CreateEvent(CreateEventRequest request)
        {
            if ( !ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _eventService.CreateEventAsync(request);
            return result.IsSuccess
                ? Ok() : StatusCode(500, result.Error);


        }
    }
}
