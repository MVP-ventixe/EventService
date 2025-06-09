using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Business.Services;
using Business.Models;
using Business.Service;

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
            //Tagit hjälp av ChatGPT
            var result = await _eventService.GetAllEventsAsync();
            if (!result.IsSuccess)
                return StatusCode(500, result.ErrorMessage);

            if (result.Result == null || !result.Result.Any())
                return NotFound("No events found.");

            return Ok(result.Result);
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
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var model = new EventModel
            {
                Image = request.Image,
                Name = request.Name,
                Date = request.Date,
                Location = request.Location,
                Description = request.Description,
                IsActive = request.IsActive
            };

            var result = await _eventService.CreateEventAsync(model);
            return result.IsSuccess
                ? Ok(result)
                : StatusCode(500, result.ErrorMessage);
        }

    }
    
}
