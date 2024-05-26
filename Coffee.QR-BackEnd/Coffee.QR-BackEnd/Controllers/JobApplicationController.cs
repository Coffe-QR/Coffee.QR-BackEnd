using Coffee.QR.API.Controllers;
using Coffee.QR.API.DTOs;
using Coffee.QR.API.Public;
using Microsoft.AspNetCore.Mvc;

namespace Coffee.QR_BackEnd.Controllers
{
    [Route("api/jobApplications")]
    [ApiController]
    public class JobApplicationController : BaseApiController
    {
        private readonly IJobApplicationService _jobApplicationService;

        public JobApplicationController(IJobApplicationService jobApplicationService)
        {
            _jobApplicationService = jobApplicationService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] JobApplicationDto jobApplicationDto)
        {
            if (jobApplicationDto == null)
            {
                return BadRequest("JobApplication data is required");
            }

            var result = _jobApplicationService.CreateJobApplication(jobApplicationDto);

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
            var result = _jobApplicationService.GetAllJobApplications();

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
        public IActionResult DeleteJobApplication(int id)
        {
            var isDeleted = _jobApplicationService.DeleteJobApplication(id);
            if (isDeleted)
            {
                // Return JSON response
                return Ok(new { message = "JobApplication deleted successfully." });
            }
            else
            {
                return NotFound(new { message = "JobApplication not found." });
            }
        }

        [HttpGet("jobsByLocal/{localId}")]
        public IActionResult GetJobApplicationsByLocal(long localId)
        {
            var result = _jobApplicationService.GetJobApplicationsByLocal(localId);

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
        public async Task<IActionResult> GetJobApplication(long id)
        {
            var result = await _jobApplicationService.GetJobApplicationByIdAsync(id);
            if (result.IsSuccess)
                return Ok(result.Value);
            return NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateJobApplication(JobApplicationDto jobApplicationDto)
        {
            var result = await _jobApplicationService.UpdateJobApplicationAsync(jobApplicationDto);
            if (result.IsSuccess)
                return Ok(result.Value);
            return BadRequest(result.Errors);
        }
    }
}
