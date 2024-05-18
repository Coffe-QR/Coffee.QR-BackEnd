using Coffee.QR.API.Controllers;
using Coffee.QR.API.DTOs;
using Coffee.QR.API.Public;
using Coffee.QR.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Coffee.QR_BackEnd.Controllers
{
    [Route("api/localUsers")]
    [ApiController]
    public class LocalUserController : BaseApiController
    {
        private readonly ILocalUserService _localUserService;

        public LocalUserController(ILocalUserService localUserService)
        {
            _localUserService = localUserService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] LocalUserDto localUserDto)
        {
            if (localUserDto == null)
            {
                return BadRequest("LocalUser data is required");
            }

            var result = _localUserService.CreateLocalUser(localUserDto);

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
            var result = _localUserService.GetAllLocalUsers();

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
        public IActionResult DeleteLocalUser(long id)
        {
            var isDeleted = _localUserService.DeleteLocalUser(id);
            if (isDeleted)
            {
                return Ok("LocalUser deleted successfully.");
            }
            else
            {
                return NotFound("LocalUser not found.");
            }
        }

        [HttpGet("getByUserId/{id}")]
        public IActionResult GetByUserId(int id)
        {
            var result = _localUserService.GetByUserId(id);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        //IN PROGRESS...

        [HttpGet("{id}")]
        public async Task<IActionResult> GetLocalUser(long id)
        {
            var result = await _localUserService.GetLocalUserByIdAsync(id);
            if (result.IsSuccess)
                return Ok(result.Value);
            return NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateLocalUser(LocalUserDto localUserDto)
        {
            var result = await _localUserService.UpdateLocalUserAsync(localUserDto);
            if (result.IsSuccess)
                return Ok(result.Value);
            return BadRequest(result.Errors);
        }
    }
}
