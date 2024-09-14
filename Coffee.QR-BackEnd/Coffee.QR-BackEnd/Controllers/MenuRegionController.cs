using Coffee.QR.API.Controllers;
using Coffee.QR.API.DTOs;
using Coffee.QR.API.Public;
using Microsoft.AspNetCore.Mvc;

namespace Coffee.QR_BackEnd.Controllers
{
    [Route("api/menuRegions")]
    [ApiController]
    public class MenuRegionController : BaseApiController
    {
        private readonly IMenuRegionService _menuRegionService;

        public MenuRegionController(IMenuRegionService menuRegionService)
        {
            _menuRegionService = menuRegionService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] MenuRegionDto menuRegionDto)
        {
            if (menuRegionDto == null)
            {
                return BadRequest("Menu region data is required");
            }

            var result = _menuRegionService.CreateMenuRegion(menuRegionDto);

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
            var result = _menuRegionService.GetAllMenuRegions();

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpGet("byMenu/{menuId}")]
        public IActionResult GetAllByMenuId(long menuId)
        {
            var result = _menuRegionService.GetAllByMenuId(menuId);
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
            var result = _menuRegionService.GetById(id);

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
        public IActionResult DeleteMenuRegion(int id)
        {
            var isDeleted = _menuRegionService.DeleteMenuRegion(id);
            if (isDeleted)
            {
                // Return JSON response
                return Ok(new { message = "Menu region deleted successfully." });
            }
            else
            {
                return NotFound(new { message = "Menu region not found." });
            }
        }

        [HttpPut("UpdateMenuRegion")]
        public IActionResult UpdateMenuRegion([FromBody] MenuRegionDto menuRegionDto)
        {
            if (menuRegionDto == null)
            {
                return BadRequest("Invalid menu region data.");
            }

            var updated = _menuRegionService.UpdateMenuRegion(menuRegionDto);
            if (updated)
            {
                return Ok(new { message = "Menu region updated successfully." });
            }
            else
            {
                return NotFound(new { message = "Menu region not found." });
            }
        }
    }
}
