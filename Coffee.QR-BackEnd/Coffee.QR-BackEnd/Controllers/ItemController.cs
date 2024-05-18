using Coffee.QR.API.Controllers;
using Coffee.QR.API.DTOs;
using Coffee.QR.API.Public;
using FluentResults;
using Coffee.QR.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Coffee.QR_BackEnd.Controllers
{
    [Route("api/items")]
    [ApiController]
    public class ItemController : BaseApiController
    {
        private readonly IItemService _itemService;

        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] ItemDto itemDto)
        {
            if (itemDto == null)
            {
                return BadRequest("Event data is required");
            }

            var result = _itemService.CreateItem(itemDto);

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
            var result = _itemService.GetAllItems();

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
        public IActionResult DeleteItem(long id)
        {
            var isDeleted = _itemService.DeleteItem(id);
            if (isDeleted)
            {
                return Ok("Item deleted successfully.");
            }
            else
            {
                return NotFound("Item not found.");
            }
        }


        [HttpGet("getAllStorage/{storageId}")]
        public IActionResult GetAllForStorage(long storageId)
        {
            var result = _itemService.GetAllForStorage(storageId);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }
        

        [HttpGet("getById/{id}")]
        public IActionResult GetById(int id)
        {
            var result = _itemService.GetById(id);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpPut("UpdateMenu")]
        public IActionResult UpdateItem([FromBody] ItemDto itemDto)
        {
            if (itemDto == null)
            {
                return BadRequest("Invalid item data.");
            }

            var updated = _itemService.UpdateItem(itemDto);
            if (updated)
            {
                return Ok(new { message = "Item updated successfully." });
            }
            else
            {
                return NotFound(new { message = "Item not found." });
            }
        }
    }
}
