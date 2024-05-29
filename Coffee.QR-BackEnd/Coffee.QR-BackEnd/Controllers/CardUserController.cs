using Coffee.QR.API.DTOs;
using Coffee.QR.Core.Interfaces;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coffee.QR.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardUserController : ControllerBase
    {
        private readonly ICardUserService _cardUserService;

        public CardUserController(ICardUserService cardUserService)
        {
            _cardUserService = cardUserService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CardUserDto>>> GetAllCardUsers()
        {
            var result = await _cardUserService.GetAllCardUsersAsync();
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Errors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CardUserDto>> GetCardUserById(long id)
        {
            var result = await _cardUserService.GetCardUserByIdAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return NotFound(result.Errors);
        }

        [HttpPost]
        public IActionResult CreateCardUser([FromBody] CardUserDto cardUserDto)
        {
            if(cardUserDto == null)
            {
                return BadRequest("Card user data required");
            }

            var result = _cardUserService.CreateCardUser(cardUserDto);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCardUser(long id, [FromBody] CardUserDto cardUserDto)
        {
            if (id != cardUserDto.CardId)
            {
                return BadRequest("Mismatched ID in the URL and body.");
            }

            var getUserResult = await _cardUserService.GetCardUserByIdAsync(id);
            if (!getUserResult.IsSuccess)
            {
                return NotFound(getUserResult.Errors);
            }

            await _cardUserService.UpdateCardUserAsync(cardUserDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCardUser(long id)
        {
            var result = await _cardUserService.DeleteCardUserAsync(id);
            if (result)
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}
