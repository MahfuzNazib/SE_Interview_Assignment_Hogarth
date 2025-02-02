namespace Hogarth.UserManagement.Application.DTOs.User
{
    public class UserDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool Active { get; set; }
        public string Company { get; set; }
        public string Sex { get; set; }
    }
}
