using Coffee.QR.API.Controllers;
using Coffee.QR.API.DTOs;
using Coffee.QR.API.Public;
using Coffee.QR.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Coffee.QR_BackEnd.Controllers
{
    [Route("api/menuItems")]
    [ApiController]
    public class MenuItemController : BaseApiController
    {
        private readonly IMenuItemService _menuItemService;

        public MenuItemController(IMenuItemService menuItemService)
        {
            _menuItemService = menuItemService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] MenuItemDto menuItemDto)
        {
            if(menuItemDto == null)
            {
                return BadRequest("MenuItem data is required");
            }

            var result = _menuItemService.CreateMenuItem(menuItemDto);

            if(result.IsSuccess)
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
            var result = _menuItemService.GetAllMenuItems();

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
        public IActionResult DeleteMenuItem(int id)
        {
            var isDeleted = _menuItemService.DeleteMenuItem(id);
            if (isDeleted)
            {
                // Return JSON response
                return Ok(new { message = "MenuItem deleted successfully." });
            }
            else
            {
                return NotFound(new { message = "MenuItem not found." });
            }
        }

        //IN PROGRESS...

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMenuItem(long id)
        {
            var result = await _menuItemService.GetMenuItemByIdAsync(id);
            if (result.IsSuccess)
                return Ok(result.Value);
            return NotFound();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMenuItem(MenuItemDto menuItemDto)
        {
            var result = await _menuItemService.UpdateMenuItemAsync(menuItemDto);
            if (result.IsSuccess)
                return Ok(result.Value);
            return BadRequest(result.Errors);
        }
    }
}
