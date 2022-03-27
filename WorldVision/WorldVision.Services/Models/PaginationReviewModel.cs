using System.Collections.Generic;
using WorldVision.Commons.Enums;

namespace WorldVision.Services.Models
{
    public class PaginationReviewModel
    {
        public List<ReviewModel> Models { get; set; }
        public int CountOfPages { get; set; }
        public int CurrentPage { get; set; }
        public string Email { get; set; }
        public string Search { get; set; }
        public string FilterName { get; set; }
        public FilterTypes Filter { get; set; }
        public SortTypes Sort { get; set; }
        public int CategoryId { get; set; }

    }
}
