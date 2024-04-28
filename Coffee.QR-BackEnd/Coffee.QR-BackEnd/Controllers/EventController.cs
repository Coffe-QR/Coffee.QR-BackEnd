using Coffee.QR.API.Controllers;
using Coffee.QR.API.DTOs;
using Coffee.QR.API.Public;
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvent(long id)
        {
            var result = await _eventService.GetEventByIdAsync(id);
            if (result.IsSuccess)
                return Ok(result.Value);
            return NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEvent(EventDto eventDto)
        {
            var result = await _eventService.UpdateEventAsync(eventDto);
            if (result.IsSuccess)
                return Ok(result.Value);
            return BadRequest(result.Errors);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(long id)
        {
            var result = await _eventService.DeleteEventAsync(id);
            if (result.IsSuccess)
                return Ok();
            return BadRequest(result.Errors);
        }
    }
}
