using System;
using WorldVision.Commons.Enums;

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
        public StateTypes State { get; set; }
        public RoleTypes Role { get; set; }
        public bool Delisted { get; set; }
    }
}
