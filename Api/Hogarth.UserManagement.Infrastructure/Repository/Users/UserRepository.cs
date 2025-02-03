using Hogarth.UserManagement.Domain.Entities;
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


        public async Task AddUserAsync(User user)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                int contactId = await AddUserContactAsync(user.Contact);

                user.ContactId = contactId;
                _dbContext.Users.Add(user);
                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception($"An error occurred while adding the user and related data. Error message : {ex.Message}");
            }
        }


        public async Task<int> AddUserContactAsync(Contact contact)
        {
            _dbContext.Contacts.Add(contact);
            await _dbContext.SaveChangesAsync();
            return contact.Id;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _dbContext.Users
            .Include(u => u.Contact)
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Id == id);
        }


        public async Task UpdateUserAsync(User user)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();
            try
            {
                if (user.Contact != null)
                {
                    var existingContact = await _dbContext.Contacts.FindAsync(user.Contact.Id);
                    if (existingContact != null)
                    {
                        _dbContext.Entry(existingContact).CurrentValues.SetValues(user.Contact);
                    }
                    else
                    {
                        _dbContext.Contacts.Add(user.Contact);
                    }
                }

                var existingUser = await _dbContext.Users
                    .Include(u => u.Contact) 
                    .FirstOrDefaultAsync(u => u.Id == user.Id);

                if (existingUser != null)
                {
                    _dbContext.Entry(existingUser).CurrentValues.SetValues(user);

                    if (user.Contact != null)
                    {
                        existingUser.ContactId = user.Contact.Id;
                    }
                }

                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                throw new Exception($"An error occurred while updating the user and related data. Error message: {ex.Message}");
            }
        }


        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _dbContext.Users.FindAsync(id);
            
            if (user != null )
            {
                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();
                return true;
            }

            return false;
        }

    }
}
