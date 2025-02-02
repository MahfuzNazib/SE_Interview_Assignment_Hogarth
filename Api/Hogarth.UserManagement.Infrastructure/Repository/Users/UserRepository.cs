﻿using Hogarth.UserManagement.Domain.Entities;
using Hogarth.UserManagement.Domain.IRepository.Users;
using Hogarth.UserManagement.Infrastructure.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Hogarth.UserManagement.Infrastructure.Repository.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationEFCoreDbContext _dbContext;

        public UserRepository(ApplicationEFCoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<(IEnumerable<User> Users, int TotalCount)> GetUsersAsync(int pageNumber, int pageSize, string search, string sortBy, bool sortAsc)
        {
            var query = _dbContext.Users.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(u =>
                    u.FirstName.Contains(search) ||
                    u.LastName.Contains(search) ||
                    u.Company.Contains(search));
            }

            int totalCount = await query.CountAsync();

            if (!string.IsNullOrEmpty(sortBy))
            {
                query = sortAsc
                    ? query.OrderBy(u => EF.Property<object>(u, sortBy))
                    : query.OrderByDescending(u => EF.Property<object>(u, sortBy));
            }

            var users = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (users, totalCount);
        }

    }
}
