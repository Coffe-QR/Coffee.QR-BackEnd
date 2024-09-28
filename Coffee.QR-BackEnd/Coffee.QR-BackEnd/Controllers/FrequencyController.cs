using Coffee.QR.API.Controllers;
using Coffee.QR.API.DTOs;
using Coffee.QR.API.Public;
using Microsoft.AspNetCore.Mvc;

namespace Coffee.QR_BackEnd.Controllers
{
    [ApiController]
    [Route("api/frequencies")]
    public class FrequencyController : BaseApiController
    {
        private readonly IFrequencyService _frequencyService;

        public FrequencyController(IFrequencyService frequencyService)
        {
            _frequencyService = frequencyService;
        }

        [HttpPost]
        public IActionResult CreateFrequency([FromBody] FrequencyDto frequencyDto)
        {
            var result = _frequencyService.Create(frequencyDto);
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
        public IActionResult GetAllFrequencys()
        {
            var result = _frequencyService.GetAll();
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpGet("units")]
        public IActionResult GetAllFrequencysUnit()
        {
            var result = _frequencyService.GetAllFrequencysUnit();
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }
    }
}
