using System.Collections.Generic;

namespace WorldVision.Services.Models
{
    public class CompositeCreateReviewModel
    {
        public CreateReviewModel ReviewModel { get; set; }
        public List<ReviewTypesModel> Types { get; set; }
        public List<ReviewImageModel> Images { get; set; }
    }
}
