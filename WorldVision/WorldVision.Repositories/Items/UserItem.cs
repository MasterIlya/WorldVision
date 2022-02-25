using SmartStore.Commons.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldVision.Repositories.Items
{
    [Table("Users")]
    public class UserItem
    {
        [Key]
        public virtual int UserId { get; set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
        public virtual string FName { get; set; }
        public virtual string LName { get; set; }
        public virtual DateTime RegistrationDate { get; set; }
        public virtual RoleTypes Role { get; set; }
    }

}
