using System;
using System.Collections.Generic;
using System.Text;

namespace WorldVision.Services.Models
{
    public class CompositeReviewModel
    {
        public ReviewModel Review { get; set; }
        public List<ReviewImageModel> Images { get; set; }
        public List<ReviewModel> LastReviewsInCategory { get; set; }
    }
}
