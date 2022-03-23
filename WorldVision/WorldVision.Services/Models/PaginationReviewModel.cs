using System.Collections.Generic;

namespace WorldVision.Services.Models
{
    public class PaginationReviewModel
    {
        public List<ReviewModel> Models { get; set; }
        public int CountOfPages { get; set; }
        public int CurrentPage { get; set; }
        public string Email { get; set; }
        public string Search { get; set; }
    }
}
