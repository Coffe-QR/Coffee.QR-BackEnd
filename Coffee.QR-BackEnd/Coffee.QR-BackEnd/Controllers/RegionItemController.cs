using Coffee.QR.API.Controllers;
using Coffee.QR.API.DTOs;
using Coffee.QR.API.Public;
using Coffee.QR.Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace Coffee.QR_BackEnd.Controllers
{
    [Route("api/regionItems")]
    [ApiController]
    public class RegionItemController : BaseApiController
    {
        private readonly IRegionItemService _regionItemService;

        public RegionItemController(IRegionItemService regionItemService)
        {
            _regionItemService = regionItemService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] RegionItemDto regionItemDto)
        {
            if (regionItemDto == null)
            {
                return BadRequest("RegionItem data is required");
            }

            var result = _regionItemService.CreateRegionItem(regionItemDto);

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
            var result = _regionItemService.GetAllRegionItems();

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
        public IActionResult DeleteRegionItem(int id)
        {
            var isDeleted = _regionItemService.DeleteRegionItem(id);
            if (isDeleted)
            {
                // Return JSON response
                return Ok(new { message = "RegionItem deleted successfully." });
            }
            else
            {
                return NotFound(new { message = "RegionItem not found." });
            }
        }

        [HttpDelete("DeleteByRegionIdAndItemId/{regionId}/{itemId}")]
        public IActionResult DeleteByRegionIdAndItemId(int regionId, int itemId)
        {
            var isDeleted = _regionItemService.DeleteByRegionIdAndItemId(regionId, itemId);
            if (isDeleted)
            {
                // Return JSON response
                return Ok(new { message = "RegionItem deleted successfully." });
            }
            else
            {
                return NotFound(new { message = "RegionItem not found." });
            }
        }

        [HttpGet("for-region/{regionId}")]
        public IActionResult GetAllForRegion(long regionId)
        {
            var result = _regionItemService.GetAllForRegion(regionId);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpGet("not-on-region/{regionId}")]
        public IActionResult GetAllNotOnRegion(long regionId)
        {
            var result = _regionItemService.GetAllNotOnRegion(regionId);

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
