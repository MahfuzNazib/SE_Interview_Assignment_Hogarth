using AutoMapper;
using Hogarth.UserManagement.Application.DTOs.Common;
using Hogarth.UserManagement.Application.DTOs.User;
using Hogarth.UserManagement.Application.Helpers;
using Hogarth.UserManagement.Application.IService.Users;
using Hogarth.UserManagement.Domain.Entities;
using Hogarth.UserManagement.Domain.IRepository.Users;

namespace Hogarth.UserManagement.Application.Service.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<UserDto>>> GetAllUsersAsync(PaginationRequestDto paginationRequestDto)
        {
            var (users, totalCount) = await _userRepository.GetUsersAsync(paginationRequestDto.pageNumber, paginationRequestDto.pageSize,
                paginationRequestDto.searchValue, paginationRequestDto.orderBy, paginationRequestDto.isAscending);

            var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);

            var paginationSummary = new PaginationSummary
            {
                PerPage = paginationRequestDto.pageSize,
                Page = paginationRequestDto.pageNumber,
                TotalCount = totalCount,
                OrderBy = paginationRequestDto.orderBy,
                Ascending = paginationRequestDto.isAscending,
                SearchParam = paginationRequestDto.searchValue,
            };

            return new ApiResponse<IEnumerable<UserDto>>
            {
                Status = true,
                Message = "User list fetched successfully",
                Values = userDtos,
                PaginationSummary = paginationSummary
            };
        }

        public async Task<ApiResponse<bool>> AddUserAsync(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            await _userRepository.AddUserAsync(user);

            return new ApiResponse<bool>
            {
                Status = true,
                Message = "User added successfully",
                Values = true
            };
        }


        public async Task<ApiResponse<UserDto>> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return new ApiResponse<UserDto>
                {
                    Status = false,
                    Message = "User not found"
                };
            }

            var userDto = _mapper.Map<UserDto>(user);
            return new ApiResponse<UserDto>
            {
                Status = true,
                Message = "User data fetched successfully",
                Values = userDto
            };
        }
    }
}
