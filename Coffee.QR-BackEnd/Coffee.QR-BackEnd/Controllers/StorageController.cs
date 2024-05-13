using Coffee.QR.API.Controllers;
using Coffee.QR.API.DTOs;
using Coffee.QR.API.Public;
using Microsoft.AspNetCore.Mvc;

namespace Coffee.QR_BackEnd.Controllers
{
    [Route("api/storages")]
    [ApiController]
    public class StorageController : BaseApiController
    {
        private readonly IStorageService _storageService;

        public StorageController(IStorageService storageService)
        {
            _storageService = storageService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] StorageDto storageDto)
        {
            if (storageDto == null)
            {
                return BadRequest("Storage data is required");
            }

            var result = _storageService.CreateStorage(storageDto);

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
            var result = _storageService.GetAllStorages();

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
        public IActionResult DeleteStorage(int id)
        {
            var isDeleted = _storageService.DeleteStorage(id);
            if (isDeleted)
            {
                // Return JSON response
                return Ok(new { message = "Storage deleted successfully." });
            }
            else
            {
                return NotFound(new { message = "Storage not found." });
            }
        }

        //IN PROGRESS...

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStorage(long id)
        {
            var result = await _storageService.GetStorageByIdAsync(id);
            if (result.IsSuccess)
                return Ok(result.Value);
            return NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStorage(StorageDto storageDto)
        {
            var result = await _storageService.UpdateStorageAsync(storageDto);
            if (result.IsSuccess)
                return Ok(result.Value);
            return BadRequest(result.Errors);
        }
    }
}
