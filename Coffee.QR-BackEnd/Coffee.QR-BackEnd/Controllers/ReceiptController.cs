using Coffee.QR.API.Controllers;
using Coffee.QR.API.DTOs;
using Coffee.QR.API.Public;
using Coffee.QR.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Coffee.QR_BackEnd.Controllers
{
    [Route("api/receipts")]
    [ApiController]
    public class ReceiptController : BaseApiController
    {
        private readonly IReceiptService _receiptService;

        public ReceiptController(IReceiptService receiptService)
        {
            _receiptService = receiptService;
        }

        [HttpPost("{moneyReceived}")]
        public IActionResult Create(double moneyReceived, [FromBody] ReceiptDto receiptDto)
        {
            if (receiptDto == null)
            {
                return BadRequest("Receipt data is required");
            }

            var result = _receiptService.CreateReceipt(receiptDto, moneyReceived);

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
        public IActionResult DeleteReceipt(long id)
        {
            var isDeleted = _receiptService.DeleteReceipt(id);
            if (isDeleted)
            {
                return Ok("Receipt deleted successfully.");
            }
            else
            {
                return NotFound("Receipt not found.");
            }
        }

        [HttpGet("getAllForLocal/{localId}")]
        public IActionResult GetAllForLocal(long localId)
        {
            var result = _receiptService.GetAllForLocal(localId);

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
