using Coffee.QR.API.Controllers;
using Coffee.QR.API.DTOs;
using Coffee.QR.API.Public;
using Coffee.QR.Core.Domain;
using Coffee.QR.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Coffee.QR_BackEnd.Controllers
{
    [Route("api/RentOffer")]
    [ApiController]
    public class RentOfferController : BaseApiController
    {
        private readonly IRentOfferService _rentOfferService;

        public RentOfferController(IRentOfferService rentOfferService)
        {
            _rentOfferService = rentOfferService;
        }

        [HttpPost]
        public IActionResult Create([FromBody] RentOfferDto rentOfferDto)
        {
            var result = _rentOfferService.CreateRentOffer(rentOfferDto);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Errors);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(long id)
        {
            var rentOffer = _rentOfferService.GetRentOfferById(id);
            if (rentOffer != null)
            {
                return Ok(rentOffer);
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var rentOffers = _rentOfferService.GetAllRentOffers();
            return Ok(rentOffers);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] RentOfferDto rentOfferDto)
        {
            var result = _rentOfferService.UpdateRentOffer(id, rentOfferDto);
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Errors);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var result = _rentOfferService.DeleteRentOffer(id);
            if (result.IsSuccess)
            {
                return Ok();
            }
            return NotFound(result.Errors);
        }
        [HttpPatch("{id}/accept")]
        public IActionResult AcceptRentOffer(long id)
        {
            var result = _rentOfferService.ChangeRentOfferStatus(id, "ACCEPTED");
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Errors);
        }


        [HttpPatch("{id}/decline")]
        public IActionResult DeclineRentOffer(long id)
        {
            var result = _rentOfferService.ChangeRentOfferStatus(id, "DECLINED");
            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Errors);
        }

        [HttpGet("local/{localId}")]
        public IActionResult GetByLocalId(long localId)
        {
            var rentOffers = _rentOfferService.GetRentOffersByLocalId(localId);
            if (rentOffers != null && rentOffers.Any())
            {
                return Ok(rentOffers);
            }
            return NotFound("No rent offers found for the given LocalId.");
        }
    }
}
