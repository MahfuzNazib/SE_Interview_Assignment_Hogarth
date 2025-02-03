using Hogarth.UserManagement.Domain.Entities;
using Hogarth.UserManagement.Domain.IRepository.Users;
using Hogarth.UserManagement.Infrastructure.JsonDataSource;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using System.Text.Json;

namespace Hogarth.UserManagement.Infrastructure.Repository.Users
{
    public class UserJsonRepository : IUserRepository
    {
        private readonly string _filePath;
        private readonly JsonSerializerOptions _jsonOptions;


        public UserJsonRepository(IConfiguration configuration)
        {
            var baseDirectory = AppContext.BaseDirectory;
            _filePath = Path.Combine(baseDirectory, configuration["JsonData:FilePath"]);

            if (string.IsNullOrEmpty(_filePath))
            {
                throw new Exception("JSON file path is not set in configuration (JsonData:FilePath).");
            }

            _jsonOptions = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true
            };

            // If the file doesn't exist, create an initial empty data store.
            if (!File.Exists(_filePath))
            {
                var initialData = new DataStore();
                WriteDataStoreAsync(initialData).Wait();
            }
        }

        private async Task<DataStore> ReadDataStoreAsync()
        {
            if (!File.Exists(_filePath))
            {
                return new DataStore();
            }

            var jsonData = await File.ReadAllTextAsync(_filePath);
            var dataStore = JsonSerializer.Deserialize<DataStore>(jsonData, _jsonOptions);
            return dataStore ?? new DataStore();
        }

        private async Task WriteDataStoreAsync(DataStore dataStore)
        {
            var jsonData = JsonSerializer.Serialize(dataStore, _jsonOptions);
            await File.WriteAllTextAsync(_filePath, jsonData);
        }


        public async Task<(IEnumerable<User> Users, int TotalCount)> GetUsersAsync(
            int pageNumber, int pageSize, string search, string sortBy, bool sortAsc)
        {
            var dataStore = await ReadDataStoreAsync();
            var users = dataStore.Users;

            if (!string.IsNullOrEmpty(search))
            {
                users = users.Where(u =>
                    (!string.IsNullOrEmpty(u.FirstName) && u.FirstName.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                    (!string.IsNullOrEmpty(u.LastName) && u.LastName.Contains(search, StringComparison.OrdinalIgnoreCase)) ||
                    (!string.IsNullOrEmpty(u.Company) && u.Company.Contains(search, StringComparison.OrdinalIgnoreCase))
                ).ToList();
            }

            // Apply sorting using reflection
            if (!string.IsNullOrEmpty(sortBy))
            {
                PropertyInfo property = typeof(User).GetProperty(sortBy, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
                if (property != null)
                {
                    users = sortAsc
                        ? users.OrderBy(u => property.GetValue(u)).ToList()
                        : users.OrderByDescending(u => property.GetValue(u)).ToList();
                }
            }

            int totalCount = users.Count;

            users = users.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return (users, totalCount);
        }



        public async Task AddUserAsync(User user)
        {
            var dataStore = await ReadDataStoreAsync();

            // Generate new user id.
            int newUserId = dataStore.Users.Any() ? dataStore.Users.Max(u => u.Id) + 1 : 1;
            user.Id = newUserId;

            if (user.Contact != null)
            {
                int newContactId = dataStore.Contacts.Any() ? dataStore.Contacts.Max(c => c.Id) + 1 : 1;
                user.Contact.Id = newContactId;
                user.ContactId = newContactId;
                dataStore.Contacts.Add(user.Contact);
            }

            dataStore.Users.Add(user);
            await WriteDataStoreAsync(dataStore);
        }


        public async Task<User> GetUserByIdAsync(int id)
        {
            var dataStore = await ReadDataStoreAsync();
            return dataStore.Users.FirstOrDefault(u => u.Id == id);
        }


        public async Task UpdateUserAsync(User user)
        {
            var dataStore = await ReadDataStoreAsync();
            var existingUser = dataStore.Users.FirstOrDefault(u => u.Id == user.Id);

            if (existingUser != null)
            {
                // Update basic user fields.
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.Active = user.Active;
                existingUser.Company = user.Company;
                existingUser.Sex = user.Sex;
                existingUser.RoleId = user.RoleId;

                // Update the contact if provided.
                if (user.Contact != null)
                {
                    var existingContact = dataStore.Contacts.FirstOrDefault(c => c.Id == user.Contact.Id);
                    if (existingContact != null)
                    {
                        existingContact.Phone = user.Contact.Phone;
                        existingContact.Address = user.Contact.Address;
                        existingContact.City = user.Contact.City;
                        existingContact.Country = user.Contact.Country;
                    }
                    else
                    {
                        int newContactId = dataStore.Contacts.Any() ? dataStore.Contacts.Max(c => c.Id) + 1 : 1;
                        user.Contact.Id = newContactId;
                        existingUser.ContactId = newContactId;
                        dataStore.Contacts.Add(user.Contact);
                    }
                    existingUser.Contact = user.Contact;
                }

                await WriteDataStoreAsync(dataStore);
            }
        }


        public async Task<bool> DeleteUserAsync(int id)
        {
            var dataStore = await ReadDataStoreAsync();
            var userToDelete = dataStore.Users.FirstOrDefault(u => u.Id == id);
            if (userToDelete != null)
            {
                dataStore.Users.Remove(userToDelete);

                // Optionally remove the associated contact.
                var contactToDelete = dataStore.Contacts.FirstOrDefault(c => c.Id == userToDelete.ContactId);
                if (contactToDelete != null)
                {
                    dataStore.Contacts.Remove(contactToDelete);
                }

                await WriteDataStoreAsync(dataStore);
                return true;
            }
            return false;
        }

    }
}
