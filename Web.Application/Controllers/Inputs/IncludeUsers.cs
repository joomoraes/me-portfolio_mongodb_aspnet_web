using Web.Application.Domain.Enums;

namespace Web.Application.Controllers.Inputs
{
    public class IncludeUsers
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
