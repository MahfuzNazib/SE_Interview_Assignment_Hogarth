namespace Hogarth.UserManagement.Application.DTOs.User
{
    public class UserDto
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public string? LastName { get; set; }
        public bool Active { get; set; }
        public required string Company { get; set; }
        public required string Sex { get; set; }
        public int RoleId { get; set; }

        public ContactDto Contact { get; set; }
    }
}
