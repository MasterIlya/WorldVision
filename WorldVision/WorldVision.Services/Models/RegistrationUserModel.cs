using System.ComponentModel.DataAnnotations;

namespace WorldVision.Services.Models
{
    public class RegistrationUserModel
    {
        [Required(ErrorMessage = "Email not specified")]
        [EmailAddress(ErrorMessage = "Incorrect email")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "The length of the email address must be from 8 to 50 characters")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password not specified")]
        [Compare("PasswordConfirm", ErrorMessage = "Passwords don't match")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "The length of the password must be from 8 to 50 characters")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Confirm password not specified")]
        [StringLength(50, MinimumLength = 8, ErrorMessage = "The length of the confirm password must be from 8 to 50 characters")]
        public string PasswordConfirm { get; set; }
        [Required(ErrorMessage = "FName not specified")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "The length of the FName must be from 2 to 50 characters")]
        public string FName { get; set; }
        [Required(ErrorMessage = "LName not specified")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "The length of the LName must be from 2 to 50 characters")]
        public string LName { get; set; }
    }
}
