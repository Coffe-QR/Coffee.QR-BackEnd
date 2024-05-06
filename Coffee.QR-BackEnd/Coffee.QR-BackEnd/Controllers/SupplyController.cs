using Coffee.QR.API.Controllers;
using Coffee.QR.API.DTOs;
using Coffee.QR.API.Public;
using Microsoft.AspNetCore.Mvc;

namespace Coffee.QR_BackEnd.Controllers
{
    [Route("api/supplies")]
    [ApiController]
    public class SupplyController : BaseApiController
    {
        private readonly ISupplyService _supplyService;

        public SupplyController(ISupplyService supplyService)
        {
            _supplyService = supplyService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] SupplyDto supplyDto)
        {
            if (supplyDto == null)
            {
                return BadRequest("Supply data is required");
            }

            var result = _supplyService.CreateSupply(supplyDto);

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
            var result = _supplyService.GetAllSupplys();

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
        public IActionResult DeleteSupply(int id)
        {
            var isDeleted = _supplyService.DeleteSupply(id);
            if (isDeleted)
            {
                // Return JSON response
                return Ok(new { message = "Supply deleted successfully." });
            }
            else
            {
                return NotFound(new { message = "Supply not found." });
            }
        }

        //IN PROGRESS...

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSupply(long id)
        {
            var result = await _supplyService.GetSupplyByIdAsync(id);
            if (result.IsSuccess)
                return Ok(result.Value);
            return NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSupply(SupplyDto supplyDto)
        {
            var result = await _supplyService.UpdateSupplyAsync(supplyDto);
            if (result.IsSuccess)
                return Ok(result.Value);
            return BadRequest(result.Errors);
        }

        [HttpGet("getById/{supplyId}")]

        public IActionResult GetById(long supplyId)
        {
            var result = _supplyService.GetById(supplyId);

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
