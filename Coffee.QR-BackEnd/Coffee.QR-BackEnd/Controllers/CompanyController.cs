using Coffee.QR.API.Controllers;
using Coffee.QR.API.DTOs;
using Coffee.QR.API.Public;
using Microsoft.AspNetCore.Mvc;

namespace Coffee.QR_BackEnd.Controllers
{
    [Route("api/companies")]
    [ApiController]
    public class CompanyController : BaseApiController
    {
        private readonly ICompanyService _companyService;

        public CompanyController(ICompanyService companyService)
        {
            _companyService = companyService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] CompanyDto companyDto)
        {
            if (companyDto == null)
            {
                return BadRequest("Company data is required");
            }

            var result = _companyService.CreateCompany(companyDto);

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
            var result = _companyService.GetAllCompanys();

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
        public IActionResult DeleteCompany(long id)
        {
            var isDeleted = _companyService.DeleteCompany(id);
            if (isDeleted)
            {
                return Ok("Company deleted successfully.");
            }
            else
            {
                return NotFound("Company not found.");
            }
        }

        //IN PROGRESS...

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompany(long id)
        {
            var result = await _companyService.GetCompanyByIdAsync(id);
            if (result.IsSuccess)
                return Ok(result.Value);
            return NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCompany(CompanyDto companyDto)
        {
            var result = await _companyService.UpdateCompanyAsync(companyDto);
            if (result.IsSuccess)
                return Ok(result.Value);
            return BadRequest(result.Errors);
        }

    }
}
