using Coffee.QR.API.DTOs;
using Coffee.QR.API.Public;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coffee.QR.API.Controllers
{
    [ApiController]
    [Route("api/cards")]
    public class CardController : BaseApiController
    {
        private readonly ICardService _cardService;

        public CardController(ICardService cardService)
        {
            _cardService = cardService;
        }

        [HttpPost]
        public IActionResult CreateCard([FromBody] CardDto cardDto)
        {
            var result = _cardService.CreateCard(cardDto);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpDelete("{cardId}")]
        public IActionResult DeleteCard(long cardId)
        {
            var isDeleted = _cardService.DeleteCard(cardId);
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
            var result = _cardService.GetAllByEventId(eventId);
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
            var result = _cardService.GetAllCards();
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
            var result = await _cardService.GetByIdAsync(id);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
