using Coffee.QR.API.Controllers;
using Coffee.QR.API.DTOs;
using Coffee.QR.API.Public;
using Coffee.QR.Core.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using SeatsioDotNet;
using SeatsioDotNet.Charts;
using SeatsioDotNet.Events;
using System.Diagnostics;


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

        [HttpPost("createCategory")]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequestDto request)
        {
            try
            {
                var client = new SeatsioClient(Region.EU(), _seatsioSecretKey);

                var categoryKeyName = request.CategoryKeyName;

                if (string.IsNullOrEmpty(categoryKeyName))
                {
                    return BadRequest("Category key or name is missing.");
                }

                var randomColor = GenerateRandomColor();

                await client.Charts.AddCategoryAsync(request.ChartKey, new Category(categoryKeyName, categoryKeyName, randomColor, false));

                await client.Charts.PublishDraftVersionAsync(request.ChartKey);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
        }

        private string GenerateRandomColor()
        {
            Random random = new Random();
            return String.Format("#{0:X6}", random.Next(0x1000000));
        }
    }


}