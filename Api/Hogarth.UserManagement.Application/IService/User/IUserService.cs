﻿using Hogarth.UserManagement.Application.DTOs.Common;
using Hogarth.UserManagement.Application.DTOs.User;
using Hogarth.UserManagement.Application.Helpers;

namespace Hogarth.UserManagement.Application.IService.User
{
    public interface IUserService
    {
        Task<ApiResponse<IEnumerable<UserDto>>> GetAllUsersAsync(PaginationRequestDto paginationRequestDto);
    }
}
