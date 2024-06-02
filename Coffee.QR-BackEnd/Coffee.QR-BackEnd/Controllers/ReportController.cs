using Coffee.QR.API.Controllers;
using Coffee.QR.API.DTOs;
using Coffee.QR.API.Public;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace Coffee.QR_BackEnd.Controllers
{
    [Route("api/reports")]
    [ApiController]
    public class ReportController : BaseApiController
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] ReportDto reportDto)
        {
            if (reportDto == null)
            {
                return BadRequest("Event data is required");
            }

            var result = _reportService.CreateReport(reportDto);

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
            var result = _reportService.GetAllReports();

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
            var isDeleted = _reportService.DeleteReport(id);
            if (isDeleted)
            {
                return Ok("Report deleted successfully.");
            }
            else
            {
                return NotFound("Report not found.");
            }
        }

        [HttpGet("getAllForLocal/{localId}")]
        public IActionResult GetAllForLocal(long localId)
        {
            var result = _reportService.GetAllForLocalProfit(localId);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpGet("getAllCoastForLocal/{localId}")]
        public IActionResult GetAllForLocalCost(long localId)
        {
            var result = _reportService.GetAllForLocalCost(localId);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpPost("cost-report")]
        public IActionResult CreateCostReport([FromBody] ReportDto reportDto)
        {
            if (reportDto == null)
            {
                return BadRequest("Event data is required");
            }

            var result = _reportService.CreateCostReport(reportDto);

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
