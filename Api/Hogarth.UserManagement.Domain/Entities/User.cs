
namespace Hogarth.UserManagement.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public bool Active { get; set; }
        public required string Company { get; set; }
        public required string Sex { get; set; }

        // Foreign Keys
        public int ContactId { get; set; }
        public int RoleId { get; set; }

        public Contact Contact { get; set; }
        public Role Role { get; set; }
    }
}
