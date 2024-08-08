using Coffee.QR.API.Controllers;
using Coffee.QR.API.DTOs;
using Coffee.QR.API.Public;
using Coffee.QR.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Coffee.QR_BackEnd.Controllers
{
    [ApiController]
    [Route("api/cardevents")]
    public class CardEventController : BaseApiController
    {
        private readonly ICardEventService _cardEventService;

        public CardEventController(ICardEventService cardEventService)
        {
            _cardEventService = cardEventService;
        }

        [HttpPost]
        public IActionResult CreateCard([FromBody] CardEventDto cardEventDto)
        {
            var result = _cardEventService.CreateCardEvent(cardEventDto);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpDelete("{cardEventId}")]
        public IActionResult DeleteCard(long cardEventId)
        {
            var isDeleted = _cardEventService.DeleteCardEvent(cardEventId);
            if (isDeleted)
            {
                return NoContent();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("event/{eventId}")]
        public IActionResult GetAllByEventId(long eventId)
        {
            var result = _cardEventService.GetAllByEventId(eventId);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpGet]
        public IActionResult GetAllCards()
        {
            var result = _cardEventService.GetAllCardEvents();
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
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var result = await _cardEventService.GetByIdAsync(id);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet("card/{cardId}")]
        public async Task<ActionResult<CardEventDto>> GetCardEventByCardIdAsync(long cardId)
        {
            var cardEvent = await _cardEventService.GetCardEventByCardIdAsync(cardId);
            if (cardEvent == null)
            {
                return NotFound();
            }
            return Ok(cardEvent);
        }

    }
}
