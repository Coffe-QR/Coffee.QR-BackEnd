using Coffee.QR.API.Controllers;
using Coffee.QR.API.DTOs;
using Coffee.QR.API.Public;
using Coffee.QR.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Coffee.QR_BackEnd.Controllers
{
    [Route("api/local")]
    [ApiController]
    public class LocalController : BaseApiController
    {
        private readonly ILocalService _localService;
        public LocalController(ILocalService localService)
        {
            _localService = localService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] LocalDto localDto)
        {
            if (localDto == null)
            {
                return BadRequest("Local data is required");
            }

            var result = _localService.CreateLocal(localDto);

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
            var result = _localService.GetAllLocals();

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
        public IActionResult DeleteLocal(int id)
        {
            var isDeleted = _localService.DeleteLocal(id);
            if (isDeleted)
            {
                // Return JSON response
                return Ok(new { message = "Local deleted successfully." });
            }
            else
            {
                return NotFound(new { message = "Local not found." });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LocalDto>> GetById(long id)
        {
            var local = await _localService.GetByIdAsync(id);
            if (local == null)
            {
                return NotFound("Local not found");
            }
            return Ok(local);
        }
    }
}
