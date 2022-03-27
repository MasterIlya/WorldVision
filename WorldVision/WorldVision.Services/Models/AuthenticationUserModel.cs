using System.ComponentModel.DataAnnotations;
using WorldVision.Commons.Enums;

namespace WorldVision.Services.Models
{
    public class AuthenticationUserModel
    {
        [Required(ErrorMessage = "Email not specified")]
        [EmailAddress(ErrorMessage = "Incorrect email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password not specified")]
        public string Password { get; set; }
        public RoleTypes Role { get; set; }
    }
}
