using System.Collections.Generic;

namespace WorldVision.Services.Models
{
    public class CompositeCreateReviewModel
    {
        public CreateReviewModel ReviewModel { get; set; }
        public List<ReviewTypeModel> Types { get; set; }
        public List<ReviewImageModel> Images { get; set; }
        public string Email { get; set; }
        public List<TagModel> Tags { get; set; }
    }
}
