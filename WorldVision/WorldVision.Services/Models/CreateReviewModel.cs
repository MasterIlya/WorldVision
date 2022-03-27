using System;
using System.Collections.Generic;
using System.Text;

namespace WorldVision.Services.Models
{
    public class CreateReviewModel
    {
        public int ReviewId { get; set; }
        public int UserId { get; set; }
        public int ReviewTypeId { get; set; }
        public List<TagModel> Tags { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public byte AuthorScore { get; set; }
    }
}
