using SharpCompress.Compressors.PPMd;
using System.ComponentModel.DataAnnotations;
using Web.Application.Domain.Enums;

namespace Web.Application.Domain.Dtos.Users
{
    public class UpdateDtoUsers
    {
        public string Id { get; set; }
        [StringLength(30, ErrorMessage = "The name has max {1}  characteres"),
            Display(Name = "User Name")]
        public string Username { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Profile")]
        public EProfile Profile { get; set; }
        [Display(Name = "Country")]
        public string Country { get; set; }
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Display(Name = "City")]
        public string City { get; set; }
        [Display(Name = "State")]
        public string State { get; set; }
        [Display(Name = "ZipCode")]
        public string ZipCode { get; set; }
        private DateTime _updateAt;

        public DateTime UpdateAt
        {
            get { return _updateAt; }
            set { _updateAt = DateTime.UtcNow; }
        }

    }
}
