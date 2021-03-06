using System.Collections.Generic;

namespace WorldVision.Services.Models
{
    public class PaginationUserModel
    {
        public List<UserModel> Users { get; set; }
        public int CountOfPages { get; set; }
        public int CurrentPage { get; set; }
        public string Search { get; set; }
    }
}
