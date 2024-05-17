using Coffee.QR.API.Controllers;
using Coffee.QR.API.DTOs;
using Coffee.QR.API.Public;
using Coffee.QR.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Coffee.QR_BackEnd.Controllers
{
    [Route("api/notification")]
    [ApiController]
    public class NotificationController : BaseApiController
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] NotificationDto notificationDto)
        {
            if (notificationDto == null)
            {
                return BadRequest("Notification data is required");
            }

            var result = _notificationService.CreateNotification(notificationDto);

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
            var result = _notificationService.GetAllNotifications();

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
        public IActionResult DeactivateNotification(long id)
        {
            _notificationService.DeactivateNotification(id);
            return Ok();
        }

        [HttpGet("getAllActive")]
        public IActionResult GetAllActive(int localId)
        {
            var result = _notificationService.GetAllActiveNotifications(localId);

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
        public IActionResult DeleteNotification(int id)
        {
            var isDeleted = _notificationService.DeleteNotification(id);
            if (isDeleted)
            {
                // Return JSON response
                return Ok(new { message = "Notification deleted successfully." });
            }
            else
            {
                return NotFound(new { message = "Notification not found." });
            }
        }
    }
}
