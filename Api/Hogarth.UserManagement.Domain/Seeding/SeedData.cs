
using Hogarth.UserManagement.Domain.Entities;

namespace Hogarth.UserManagement.Domain.Seeding
{
    public static class SeedData
    {
        // Role Data List
        public static List<Role> GetRoles()
        {
            return new List<Role>
            {
                new Role { Id = 1, Name = "Admin" },
                new Role { Id = 2, Name = "Manager" },
                new Role { Id = 3, Name = "User" }
            };
        }

        // Contact Data List
        public static List<Contact> GetContacts()
        {
            return new List<Contact>
            {
                new Contact { Id = 1, Phone = "+41023658", Address = "Banani", City = "Dhaka", Country = "Bangladesh" },
                new Contact { Id = 2, Phone = "+8801777127618", Address = "Gulshan", City = "Dhaka", Country = "Bangladesh" },
                new Contact { Id = 3, Phone = "+8801777127614", Address = "Gulshan", City = "Dhaka", Country = "Bangladesh" }
            };
        }

        // User Data List (Assigning only FK values)
        public static List<User> GetUsers()
        {
            return new List<User>
            {
                new User { Id = 1, FirstName = "Shibli", LastName = "Arafat", Active = true, Company = "SoftwarePeople", Sex = "M", ContactId = 1, RoleId = 1 },
                new User { Id = 2, FirstName = "Nazib", LastName = "Mahfuz", Active = true, Company = "hSenid Business Solutions PLC.", Sex = "M", ContactId = 2, RoleId = 2 },
                new User { Id = 3, FirstName = "Hasib", LastName = "Ahmed", Active = true, Company = "Hogarth Dhaka", Sex = "M", ContactId = 3, RoleId = 3 }
            };
        }
    }
}
