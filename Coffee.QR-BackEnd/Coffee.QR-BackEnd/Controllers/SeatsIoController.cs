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
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;



namespace Coffee.QR_BackEnd.Controllers
{
    
    [Route("api/seatsio")]
    [ApiController]
    public class SeatsIoController : BaseApiController
    {

        private readonly string _seatsioSecretKey = "709f52bc-9892-4334-b511-99fe2a56646a";
        private const string RegionNew = "eu";
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

        [HttpPost("updateCategory")]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryRequestDto request)
        {
            try
            {
                var client = new SeatsioClient(Region.EU(), _seatsioSecretKey);
                var eventKey = request.EventKey;
                var objectIds = request.ObjectIds;
                var newCategory = request.NewCategory;

                var eventDetail = await client.Events.RetrieveAsync(eventKey);
                var currentObjectCategories = new Dictionary<string, string>();

                foreach (var objectId in eventDetail.ObjectCategories.Keys)
                {
                    var category = eventDetail.ObjectCategories[objectId];
                    currentObjectCategories[objectId] = category.ToString();
                }

                
                foreach (var objectId in objectIds)
                {
                    currentObjectCategories[objectId] = newCategory;
                }

                var updateParams = new UpdateEventParams
                {
                    ObjectCategories = currentObjectCategories.ToDictionary(
                        kvp => kvp.Key,
                        kvp => (object)kvp.Value
                    )
                };

                await client.Events.UpdateAsync(eventKey, updateParams);

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

        [HttpGet("getChartDetails/{chartKey}")]
        public async Task<IActionResult> GetChartDetails(string chartKey)
        {
            try
            {
                var client = new SeatsioClient(Region.EU(), _seatsioSecretKey);
                var report = await client.ChartReports.ByLabelAsync(chartKey); // This method needs to be supported by the SDK

                if (report != null)
                {
                    return Ok(report);
                }
                else
                {
                    return NotFound("No details found for the chart.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
        }



        [HttpGet("getChartCategories/{chartKey}")]
        public async Task<IActionResult> GetChartCategories(string chartKey)
        {
            try
            {
                var client = new SeatsioClient(Region.EU(), _seatsioSecretKey);
                var report = await client.ChartReports.SummaryByCategoryLabelAsync(chartKey); 

                if (report != null)
                {
                    report.Remove("NO_CATEGORY");

                    return Ok(report);
                }
                else
                {
                    return NotFound("No details found for the chart.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred: " + ex.Message);
            }
        }

    }
}