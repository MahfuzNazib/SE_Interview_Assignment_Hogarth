using Hogarth.UserManagement.Domain.Entities;

namespace Hogarth.UserManagement.Infrastructure.JsonDataSource
{
    public class DataStore
    {
        public List<User> Users { get; set; } = new List<User>();
        public List<Contact> Contacts { get; set; } = new List<Contact>();
        public List<Role> Roles { get; set; } = new List<Role>();
    }
}
