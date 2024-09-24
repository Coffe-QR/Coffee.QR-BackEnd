using Coffee.QR.API.Controllers;
using Coffee.QR.API.DTOs;
using Coffee.QR.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coffee.QR_BackEnd.Controllers
{
    [Route("api/sales")]
    public class SaleController : BaseApiController
    {
        private readonly ISaleService _saleService;

        public SaleController(ISaleService saleService)
        {
            _saleService = saleService;
        }

        [HttpGet("get-for-company/{companyName}")]
        public ActionResult<List<SaleDto>> GetAllForCompany(string companyName)
        {
            var result = _saleService.GetAllForCompany(companyName);
            return CreateResponse(result);  
        }
    }
}
