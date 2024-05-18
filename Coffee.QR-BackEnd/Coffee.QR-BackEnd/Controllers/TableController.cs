using Coffee.QR.API.Controllers;
using Coffee.QR.API.DTOs;
using Coffee.QR.API.Public;
using Coffee.QR.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Coffee.QR_BackEnd.Controllers
{
    [Route("api/table")]
    [ApiController]
    public class TableController : BaseApiController
    {
        private readonly ITableService _tableService;

        public TableController(ITableService tableService)
        {
            _tableService = tableService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] TableDto tableDto)
        {
            if (tableDto == null)
            {
                return BadRequest("Table data is required");
            }

            var result = _tableService.CreateTable(tableDto);

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
            var result = _tableService.GetAllTables();

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
            var result = _tableService.GetById(id);

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
        public IActionResult DeleteTable(int id)
        {
            var isDeleted = _tableService.DeleteTable(id);
            if (isDeleted)
            {
                // Return JSON response
                return Ok(new { message = "Table deleted successfully." });
            }
            else
            {
                return NotFound(new { message = "Table not found." });
            }
        }
    }
}
