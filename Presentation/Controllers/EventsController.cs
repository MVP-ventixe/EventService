using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Business.Services;

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
        public async Task<IActionResult> GetEventById(int id)
        {
            var eventModel = await _eventService.GetEventByIdAsync(id);
            if (eventModel == null)
            {
                return NotFound();
            }
            return Ok(eventModel);
        }
    }
}
