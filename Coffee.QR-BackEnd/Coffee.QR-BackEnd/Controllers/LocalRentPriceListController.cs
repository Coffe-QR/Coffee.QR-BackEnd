using Coffee.QR.API.Controllers;
using Coffee.QR.API.DTOs;
using Coffee.QR.API.Public;
using Microsoft.AspNetCore.Mvc;

namespace Coffee.QR_BackEnd.Controllers
{
    [Route("api/LocalRentPriceList")]
    [ApiController]
    public class LocalRentPriceListController : BaseApiController
    {

        private readonly ILocalRentPriceListService _localRentPriceListService;

        public LocalRentPriceListController(ILocalRentPriceListService localRentPriceListService)
        {
            _localRentPriceListService = localRentPriceListService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] LocalRentPriceListDto localRentPriceListDto)
        {
            if (localRentPriceListDto == null)
            {
                return BadRequest("Required data");
            }

            var result = _localRentPriceListService.CreateLocalRentPriceList(localRentPriceListDto);

            if(result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else 
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpGet("active/{localId}")]
        public IActionResult GetActiveByLocalId(long localId)
        {
            var result = _localRentPriceListService.GetActiveLocalRentPriceList(localId);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return NotFound(result.Errors);
            }
        }

    }
}
