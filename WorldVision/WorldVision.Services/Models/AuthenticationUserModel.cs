using SmartStore.Commons.Enums;

namespace WorldVision.Services.Models
{
    public class AuthenticationUserModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public RoleTypes Role { get; set; }
    }
}
