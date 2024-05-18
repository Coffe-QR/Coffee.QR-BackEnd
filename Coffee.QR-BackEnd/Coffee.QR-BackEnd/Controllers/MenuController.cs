using Coffee.QR.API.Controllers;
using Coffee.QR.API.DTOs;
using Coffee.QR.API.Public;
using Coffee.QR.Core.Domain;
using Coffee.QR.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Coffee.QR_BackEnd.Controllers
{
    [Route("api/menus")]
    [ApiController]
    public class MenuController : BaseApiController
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] MenuDto menuDto)
        {
            if (menuDto == null)
            {
                return BadRequest("Menu data is required");
            }

            var result = _menuService.CreateMenu(menuDto);

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
            var result = _menuService.GetAllMenus();

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpGet("byLocal/{localId}")]
        public IActionResult GetAllByLocalId(long localId)
        {
            var result = _menuService.GetAllByLocalId(localId);
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
            var result = _menuService.GetById(id);

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
        public IActionResult DeleteMenu(int id)
        {
            var isDeleted = _menuService.DeleteMenu(id);
            if (isDeleted)
            {
                // Return JSON response
                return Ok(new { message = "Menu deleted successfully." });
            }
            else
            {
                return NotFound(new { message = "Menu not found." });
            }
        }

        //IN PROGRESS...
        /*
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMenu(long id)
        {
            var result = await _menuService.GetMenuByIdAsync(id);
            if (result.IsSuccess)
                return Ok(result.Value);
            return NotFound();
        }
        */
        /*
        [HttpPut]
        public async Task<IActionResult> UpdateMenu(MenuDto menuDto)
        {
            var result = await _menuService.UpdateMenuAsync(menuDto);
            if (result.IsSuccess)
                return Ok(result.Value);
            return BadRequest(result.Errors);
        }
        */
        [HttpPut("UpdateMenu")]
        public IActionResult UpdateMenu([FromBody] MenuDto menuDto)
        {
            if (menuDto == null)
            {
                return BadRequest("Invalid menu data.");
            }

            var updated = _menuService.UpdateMenu(menuDto);
            if (updated)
            {
                return Ok(new { message = "Menu updated successfully." });
            }
            else
            {
                return NotFound(new { message = "Menu not found." });
            }
        }

    }
}
