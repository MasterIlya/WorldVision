using System;
using System.Collections.Generic;
using System.Text;

namespace WorldVision.Services.Models
{
    public class GeneralPageModel
    {
        public List<ReviewModel> PopularReviews { get; set; }
        public List<ReviewModel> LastReviews { get; set; }
        public List<PopularTagModel> Tags { get; set; }
    }
}
