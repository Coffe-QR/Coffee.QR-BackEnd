using Coffee.QR.API.Controllers;
using Coffee.QR.API.DTOs;
using Coffee.QR.API.Public;
using Coffee.QR.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Coffee.QR_BackEnd.Controllers
{
    [Route("api/event")]
    [ApiController]
    public class EventController : BaseApiController
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] EventDto eventDto)
        {
            if (eventDto == null)
            {
                return BadRequest("Event data is required");
            }

            var result = _eventService.CreateEvent(eventDto);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _eventService.GetAllEvents();

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEvent(int id)
        {
            var isDeleted = _eventService.DeleteEvent(id);
            if (isDeleted)
            {
                // Return JSON response
                return Ok(new { message = "Event deleted successfully." });
            }
            else
            {
                return NotFound(new { message = "Event not found." });
            }
        }

        [HttpGet("byUser/{userId}")]
        public IActionResult GetAllByUserId(long userId)
        {
            var result = _eventService.GetAllByUserId(userId);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<EventDto>> GetById(long id)
        {
            var @event = await _eventService.GetByIdAsync(id);
            if (@event == null)
            {
                return NotFound("Local not found");
            }
            return Ok(@event);
        }

        //IN PROGRESS...



        [HttpPut]
        public async Task<IActionResult> UpdateEvent(EventDto eventDto)
        {
            var result = await _eventService.UpdateEventAsync(eventDto);
            if (result.IsSuccess)
                return Ok(result.Value);
            return BadRequest(result.Errors);
        }

    }
}
