using Coffee.QR.API.Controllers;
using Coffee.QR.API.DTOs;
using Coffee.QR.API.Public;
using Coffee.QR.BuildingBlocks.Core.UseCases;
using Coffee.QR.Core.Domain;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace Coffee.QR_BackEnd.Controllers
{
    [Route("api/errorSupplies")]
    [ApiController]
    public class ErrorSupplyController : BaseApiController
    {
        private readonly IErrorSupplyService _errorSupplyService;

        public ErrorSupplyController(IErrorSupplyService errorSupplyService)
        {
            _errorSupplyService = errorSupplyService;
        }

        [HttpPost]
        public ActionResult Create(ErrorSupplyDto errorSupply)
        {
            var result = _errorSupplyService.Create(errorSupply);
            return CreateResponse(result);
        }

        [HttpGet("for-supply/{supplyId}")]
        public ActionResult GetAllForSupply(long supplyId)
        {
            var result = _errorSupplyService.GetAllForSupply(supplyId);
            return CreateResponse(result);
        }
    }
}
