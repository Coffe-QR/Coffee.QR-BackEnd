using Coffee.QR.API.Controllers;
using Coffee.QR.API.DTOs;
using Coffee.QR.API.Public;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace Coffee.QR_BackEnd.Controllers
{
    [Route("api/contracts")]
    [ApiController]
    public class ContractController : BaseApiController
    {
        private readonly IContractService _contractService;

        public ContractController(IContractService contractService)
        {
            _contractService = contractService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] ContractDto contractDto)
        {
            if (contractDto == null)
            {
                return BadRequest("Contract data is required");
            }

            var result = _contractService.CreateContract(contractDto);

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
            var result = _contractService.GetAllContracts();

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
        public IActionResult DeleteContract(long id)
        {
            var isDeleted = _contractService.DeleteContract(id);
            if (isDeleted)
            {
                return Ok("Contract deleted successfully.");
            }
            else
            {
                return NotFound("Contract not found.");
            }
        }
        [HttpGet("getAllForLocal/{localId}")]
        public IActionResult GetAllForLocal(long localId)
        {
            var result = _contractService.GetAllForLocal(localId);

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
