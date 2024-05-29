using Coffee.QR.API.Controllers;
using Coffee.QR.API.DTOs;
using Coffee.QR.API.Public;
using Coffee.QR.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Coffee.QR_BackEnd.Controllers
{
    [Route("api/cardSaleReport")]
    [ApiController]
    public class CardSaleReportController : BaseApiController
    {
        private readonly ICardSaleReportService _cardSaleReportService;

        public CardSaleReportController(ICardSaleReportService cardSaleReportService)
        {
            _cardSaleReportService = cardSaleReportService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CardSaleReportDto cardSaleReportDto)
        {
            if (cardSaleReportDto == null)
            {
                return BadRequest("Receipt data is required");
            }

            var result = _cardSaleReportService.CreateReport(cardSaleReportDto);

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
            var result = _cardSaleReportService.GetAllReports();

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
        public IActionResult DeleteReport(long id)
        {
            var isDeleted = _cardSaleReportService.DeleteReport(id);
            if (isDeleted)
            {
                return Ok("Report deleted successfully.");
            }
            else
            {
                return NotFound("Report not found.");
            }
        }


    }
}
