using Hogarth.UserManagement.Application.DTOs.Common;
using Hogarth.UserManagement.Application.DTOs.User;
using Hogarth.UserManagement.Application.IService.Users;
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

        [HttpPost("GetUsers")]
        public async Task<IActionResult> GetUsers([FromBody] PaginationRequestDto paginationRequestDto)
        {
            var response = await _userService.GetAllUsersAsync(paginationRequestDto);
            return Ok(response);
        }


        [HttpPost("AddUser")]
        public async Task<IActionResult> AddUser([FromBody] UserDto userDto)
        {
            var response = await _userService.AddUserAsync(userDto);    
            return Ok(response);
        }

        [HttpGet("GetUserById/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var response = await _userService.GetUserByIdAsync(id);

            if (!response.Status)
                return NotFound(response);

            return Ok(response);
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser([FromBody] UserDto userDto)
        {
            var response = await _userService.UpdateUserAsync(userDto);
            return Ok(response);
        }


        [HttpDelete("DeleteUser/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var response = await _userService.DeleteUserAsync(id);
            return Ok(response);
        }
    }
}
