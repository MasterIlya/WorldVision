using SmartStore.Commons.Enums;
using System;

namespace WorldVision.Services.Models
{
    public class UserModel
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public DateTime RegistrationDate { get; set; }
        public RoleTypes Role { get; set; }
    }
}
