using Coffee.QR.API.Controllers;
using Coffee.QR.API.DTOs;
using Coffee.QR.API.Public;
using Coffee.QR.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Coffee.QR_BackEnd.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : BaseApiController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] OrderDto orderDto)
        {
            if (orderDto == null)
            {
                return BadRequest("Order data is required");
            }

            var result = _orderService.CreateOrder(orderDto);

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
            var result = _orderService.GetAllOrders();

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
        public IActionResult DeleteOrder(int id)
        {
            var isDeleted = _orderService.DeleteOrder(id);
            if (isDeleted)
            {
                // Return JSON response
                return Ok(new { message = "Order deleted successfully." });
            }
            else
            {
                return NotFound(new { message = "Order not found." });
            }
        }

        [HttpGet("getByLocalIdAndIsActive/{localId}")]
        public IActionResult getByLocalIdAndIsActive(long localId)
        {
            var result = _orderService.getByLocalIdAndIsActive(localId);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpPut("deactivate/{id}")]
        public IActionResult DeactivateOrder(long id)
        {
            _orderService.DeactivateOrder(id);
            return Ok();
        }

        [HttpGet("getById/{id}")]
        public IActionResult GetById(int id)
        {
            var result = _orderService.GetById(id);

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
