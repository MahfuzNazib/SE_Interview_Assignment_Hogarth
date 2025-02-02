using Hogarth.UserManagement.Application.DTOs.Common;
using Hogarth.UserManagement.Application.IService.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hogarth.UserManagement.API.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> GetUsers(PaginationRequestDto paginationRequestDto)
        {
            var response = await _userService.GetAllUsersAsync(paginationRequestDto);
            return Ok(response);
        }
    }
}
