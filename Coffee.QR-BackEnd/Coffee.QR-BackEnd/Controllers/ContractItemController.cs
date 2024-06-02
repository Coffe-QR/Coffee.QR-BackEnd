using Coffee.QR.API.Controllers;
using Coffee.QR.API.DTOs;
using Coffee.QR.API.Public;
using Coffee.QR.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Coffee.QR_BackEnd.Controllers
{
    [Route("api/contractItems")]
    [ApiController]
    public class ContractItemController : BaseApiController
    {
        private readonly IContractItemService _contractItemService;

        public ContractItemController(IContractItemService contractItemService)
        {
            _contractItemService = contractItemService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] ContractItemDto contractItemDto)
        {
            if (contractItemDto == null)
            {
                return BadRequest("ContractItem data is required");
            }

            var result = _contractItemService.CreateContractItem(contractItemDto);

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
            var result = _contractItemService.GetAllContractItems();

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
        public IActionResult DeleteContractItem(int id)
        {
            var isDeleted = _contractItemService.DeleteContractItem(id);
            if (isDeleted)
            {
                // Return JSON response
                return Ok(new { message = "ContractItem deleted successfully." });
            }
            else
            {
                return NotFound(new { message = "ContractItem not found." });
            }
        }

        [HttpPost("create-list")]
        public IActionResult CreateList([FromBody] List<ContractItemDto> contractItemDtos)
        {
            if (contractItemDtos == null)
            {
                return BadRequest("ContractItems data is required");
            }

            var result = _contractItemService.CreateContractItems(contractItemDtos);

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
