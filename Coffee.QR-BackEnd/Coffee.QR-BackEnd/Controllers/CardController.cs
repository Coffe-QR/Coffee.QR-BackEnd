using Coffee.QR.API.DTOs;
using Coffee.QR.API.Public;
using Coffee.QR.Core.Domain;
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

        [HttpPut("{cardId}")]
        public async Task<IActionResult> UpdateCard(long cardId, [FromBody] CardUpdateDto updateDto)
        {
            await _cardService.UpdateCardDetails(cardId, updateDto.Type, updateDto.Note, updateDto.Price);
            return Ok();
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

        [HttpGet("type/{type}")]
        public ActionResult<IEnumerable<CardDto>> GetByType(string type)
        {
            var cards = _cardService.GetByType(type);
            return Ok(cards);
        }

        [HttpGet("type/{type}/event/{eventId}")]
        public ActionResult<IEnumerable<CardDto>> GetByTypeAndEventId(string type, long eventId)
        {
            var cards = _cardService.GetByTypeAndEventId(type, eventId);
            return Ok(cards);
        }


    }
}
