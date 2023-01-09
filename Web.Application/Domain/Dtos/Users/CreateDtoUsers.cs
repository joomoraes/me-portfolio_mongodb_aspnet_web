using System.Security.Principal;

namespace Web.Application.Domain.Dtos.Users
{
    public class CreateDtoUsers
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Profile { get; set; }
        public string? Biography { get; set; }
        public string? ZipCode { get; set; }
        public string? State { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }

    }
}
