using System.Collections.Generic;

namespace WorldVision.Services.Models
{
    public class CompositeReviewModel
    {
        public ReviewModel Review { get; set; }
        public List<ReviewImageModel> Images { get; set; }
        public List<ReviewModel> LastReviewsInCategory { get; set; }
        public int Rating { get; set; }
        public ReviewLikeModel CurrentUserLike { get; set; }
    }
}
