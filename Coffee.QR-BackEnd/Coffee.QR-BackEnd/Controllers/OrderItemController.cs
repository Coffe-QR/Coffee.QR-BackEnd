using Coffee.QR.API.Controllers;
using Coffee.QR.API.DTOs;
using Coffee.QR.API.Public;
using Coffee.QR.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Coffee.QR_BackEnd.Controllers
{
    [Route("api/orderItems")]
    [ApiController]
    public class OrderItemController : BaseApiController
    {
        private readonly IOrderItemService _orderItemService;

        public OrderItemController(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] OrderItemDto orderItemDto)
        {
            if (orderItemDto == null)
            {
                return BadRequest("OrderItem data is required");
            }

            var result = _orderItemService.CreateOrderItem(orderItemDto);

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
            var result = _orderItemService.GetAllOrderItems();

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
        public IActionResult DeleteOrderItem(int id)
        {
            var isDeleted = _orderItemService.DeleteOrderItem(id);
            if (isDeleted)
            {
                // Return JSON response
                return Ok(new { message = "OrderItem deleted successfully." });
            }
            else
            {
                return NotFound(new { message = "OrderItem not found." });
            }
        }

        [HttpGet("for-order/{orderId}")]
        public IActionResult GetAllForOrder(long orderId)
        {
            var result = _orderItemService.GetAllForOrder(orderId);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpGet("getByOrderId/{id}")]
        public IActionResult GetByOrderId(int id)
        {
            var result = _orderItemService.GetByOrderId(id);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpGet("getQuantityByOrderIdAndItemId/{orderId}/{itemId}")]
        public IActionResult getQuantityByOrderIdAndItemId(long orderId,long itemId)
        {
            var result = _orderItemService.getQuantityByOrderIdAndItemId(orderId,itemId);

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
