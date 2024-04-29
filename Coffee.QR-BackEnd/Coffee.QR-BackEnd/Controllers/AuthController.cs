using Coffee.QR.API.Controllers;
using Coffee.QR.API.DTOs;
using Coffee.QR.API.Public;
using Microsoft.AspNetCore.Mvc;

namespace Coffee.QR_BackEnd.Controllers
{
    [Route("api/users")]
    public class AuthController : BaseApiController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public ActionResult<AuthenticationTokensDto> Register([FromBody] RegisterDto account)
        {
            var result = _authService.Register(account);
            return CreateResponse(result);
        }

        [HttpPost("login")]
        public ActionResult<AuthenticationTokensDto> Login([FromBody] LoginDto credentials)
        {
            var result = _authService.Login(credentials);
            return CreateResponse(result);
        }


    }
}
