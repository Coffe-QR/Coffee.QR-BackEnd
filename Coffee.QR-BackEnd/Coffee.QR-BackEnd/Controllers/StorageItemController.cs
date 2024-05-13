using Coffee.QR.API.Controllers;
using Coffee.QR.API.DTOs;
using Coffee.QR.API.Public;
using Microsoft.AspNetCore.Mvc;

namespace Coffee.QR_BackEnd.Controllers
{
    [Route("api/storageItems")]
    [ApiController]
    public class StorageItemController : BaseApiController
    {
        private readonly IStorageItemService _storageItemService;

        public StorageItemController(IStorageItemService storageItemService)
        {
            _storageItemService = storageItemService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] StorageItemDto storageItemDto)
        {
            if (storageItemDto == null)
            {
                return BadRequest("StorageItem data is required");
            }

            var result = _storageItemService.CreateStorageItem(storageItemDto);

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
            var result = _storageItemService.GetAllStorageItems();

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
        public IActionResult DeleteStorageItem(int id)
        {
            var isDeleted = _storageItemService.DeleteStorageItem(id);
            if (isDeleted)
            {
                // Return JSON response
                return Ok(new { message = "StorageItem deleted successfully." });
            }
            else
            {
                return NotFound(new { message = "StorageItem not found." });
            }
        }

        //IN PROGRESS...

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStorageItem(long id)
        {
            var result = await _storageItemService.GetStorageItemByIdAsync(id);
            if (result.IsSuccess)
                return Ok(result.Value);
            return NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStorageItem(StorageItemDto storageItemDto)
        {
            var result = await _storageItemService.UpdateStorageItemAsync(storageItemDto);
            if (result.IsSuccess)
                return Ok(result.Value);
            return BadRequest(result.Errors);
        }
    }
}
