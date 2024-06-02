using Coffee.QR.API.Controllers;
using Coffee.QR.API.DTOs;
using Coffee.QR.API.Public;
using Microsoft.AspNetCore.Mvc;

namespace Coffee.QR_BackEnd.Controllers
{
    [Route("api/jobApplicationsReport")]
    [ApiController]
    public class JobApplicationsCountReportController : BaseApiController
    {
        private readonly IJobApplicationsCountReportService _jobApplicationsCountReportService;

        public JobApplicationsCountReportController(IJobApplicationsCountReportService jobApplicationsCountReportService)
        {
            _jobApplicationsCountReportService = jobApplicationsCountReportService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] JobApplicationsCountReportDto jobApplicationsCountReportDto)
        {
            if (jobApplicationsCountReportDto == null)
            {
                return BadRequest("Data is required");
            }

            var result = _jobApplicationsCountReportService.CreateReport(jobApplicationsCountReportDto);

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
            var result = _jobApplicationsCountReportService.GetAllReports();

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
            var isDeleted = _jobApplicationsCountReportService.DeleteReport(id);
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
  
