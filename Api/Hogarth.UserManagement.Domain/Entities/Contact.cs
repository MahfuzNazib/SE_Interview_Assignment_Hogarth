
namespace Hogarth.UserManagement.Domain.Entities
{
    public class Contact
    {
        public int Id { get; set; }
        public required string Phone { get; set; }
        public required string Address { get; set; }
        public required string City { get; set; }
        public required string Country { get; set; }

        public User User { get; set; }
    }
}
