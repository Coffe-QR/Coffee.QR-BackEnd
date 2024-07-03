using Coffee.QR.API.Controllers;
using Coffee.QR.API.DTOs;
using Coffee.QR.API.Public;
using Coffee.QR.Core.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using SeatsioDotNet;


namespace Coffee.QR_BackEnd.Controllers
{
    [Route("api/seatsio")]
    [ApiController]
    public class SeatsIoController : BaseApiController
    {

        private readonly string _seatsioSecretKey = "709f52bc-9892-4334-b511-99fe2a56646a";
        public SeatsIoController()
        {
        }

        [HttpPost("book")]
        public async Task<IActionResult> BookSeats([FromBody] BookSeatsRequestDto request)
        {
            try
            {
                var client = new SeatsioClient(Region.EU(), _seatsioSecretKey);

                // Example: Assuming EventName and SeatsToBook are passed in the request
                var result = await client.Events.BookAsync(request.EventName, request.SeatsToBook);

                if (result != null && result.Objects != null && result.Objects.Count > 0)
                {
                    return Ok(); // or return some success response
                }
                else
                {
                    return StatusCode(500, "Failed to book seats: ");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
        }

        [HttpPost("hold")]
        public async Task<IActionResult> HoldSeats([FromBody] HoldSeatsRequestDto request)
        {
            try
            {
                var client = new SeatsioClient(Region.EU(), _seatsioSecretKey);

                // Assuming that we use a unique identifier per session to hold the seats
                string holdToken = Guid.NewGuid().ToString();  // Create a unique hold token for each hold request

                // Example: Assuming EventName and SeatsToHold are passed in the request
                var result = await client.Events.HoldAsync(request.EventName, request.SeatsToHold, holdToken);

                if (result != null && result.Objects != null && result.Objects.Count > 0)
                {
                    return Ok(new { holdToken = holdToken, heldSeats = result.Objects }); // Return hold token and held seats info
                }
                else
                {
                    return StatusCode(500, "Failed to hold seats");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
        }



    }
}
