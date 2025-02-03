using Hogarth.UserManagement.Application.DTOs.Common;
using Hogarth.UserManagement.Application.DTOs.User;
using Hogarth.UserManagement.Application.Helpers;

namespace Hogarth.UserManagement.Application.IService.Users
{
    public interface IUserService
    {
        Task<ApiResponse<IEnumerable<UserDto>>> GetAllUsersAsync(PaginationRequestDto paginationRequestDto);

        Task<ApiResponse<bool>> AddUserAsync(UserDto userDto);

        Task<ApiResponse<UserDto>> GetUserByIdAsync(int id);

        Task<ApiResponse<bool>> UpdateUserAsync(UserDto userDto);

        Task<ApiResponse<bool>> DeleteUserAsync(int id);
    }
}
