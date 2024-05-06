using Coffee.QR.API.Controllers;
using Coffee.QR.API.DTOs;
using Coffee.QR.API.Public;
using Microsoft.AspNetCore.Mvc;

namespace Coffee.QR_BackEnd.Controllers
{
    [Route("api/supplyItems")]
    [ApiController]
    public class SupplyItemController : BaseApiController
    {
        private readonly ISupplyItemService _supplyItemService;

        public SupplyItemController(ISupplyItemService supplyItemService)
        {
            _supplyItemService = supplyItemService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] SupplyItemDto supplyItemDto)
        {
            if (supplyItemDto == null)
            {
                return BadRequest("SupplyItem data is required");
            }

            var result = _supplyItemService.CreateSupplyItem(supplyItemDto);

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
            var result = _supplyItemService.GetAllSupplyItems();

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
        public IActionResult DeleteSupplyItem(int id)
        {
            var isDeleted = _supplyItemService.DeleteSupplyItem(id);
            if (isDeleted)
            {
                // Return JSON response
                return Ok(new { message = "SupplyItem deleted successfully." });
            }
            else
            {
                return NotFound(new { message = "SupplyItem not found." });
            }
        }

        //IN PROGRESS...

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSupplyItem(long id)
        {
            var result = await _supplyItemService.GetSupplyItemByIdAsync(id);
            if (result.IsSuccess)
                return Ok(result.Value);
            return NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSupplyItem(SupplyItemDto supplyItemDto)
        {
            var result = await _supplyItemService.UpdateSupplyItemAsync(supplyItemDto);
            if (result.IsSuccess)
                return Ok(result.Value);
            return BadRequest(result.Errors);
        }

        [HttpPost("create-list")]
        public IActionResult CreateList([FromBody] List<SupplyItemDto> supplyItemDtos)
        {
            if (supplyItemDtos == null)
            {
                return BadRequest("SupplyItems data is required");
            }

            var result = _supplyItemService.CreateSupplyItems(supplyItemDtos);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpGet("for-supply/{supplyId}")]
        public IActionResult GetAllForSupply(long supplyId)
        {
            var result = _supplyItemService.GetAllForSupply(supplyId);
            
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
