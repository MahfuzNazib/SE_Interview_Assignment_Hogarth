﻿using Hogarth.UserManagement.Domain.Entities;

namespace Hogarth.UserManagement.Domain.IRepository.Users
{
    public interface IUserRepository
    {
        Task<(IEnumerable<User> Users, int TotalCount)> GetUsersAsync(int pageNumber, int pageSize, string search, 
            string sortBy, bool sortAsc);

        Task AddUserAsync(User user);

        Task<User> GetUserByIdAsync(int id);

        Task UpdateUserAsync(User user);

        Task<bool> DeleteUserAsync(int id);
    }
}
