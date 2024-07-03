using Coffee.QR.API.Controllers;
using Coffee.QR.API.DTOs;
using Coffee.QR.API.Public;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Coffee.QR_BackEnd.Controllers
{
    //[Authorize(Policy = "clientPolicy")]
    [Route("api/users")]
    public class UserController : BaseApiController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetById/{userId:int}")]
        public ActionResult<UserDto> GetById(long userId)
        {
            var result = _userService.GetById(userId);
            return CreateResponse(result);
        }

        [HttpGet("GetAllManagers")]
        public ActionResult<List<UserDto>> GetAllManagers()
        {
            var result = _userService.GetAllManagers();
            return CreateResponse(result);
        }

        [HttpGet("GetAllManagersNotInLocalUser")]
        public ActionResult<List<UserDto>> GetAllManagersNotInLocalUser()
        {
            var result = _userService.GetAllManagersNotInLocalUser();
            return CreateResponse(result);
        }
    }
}
