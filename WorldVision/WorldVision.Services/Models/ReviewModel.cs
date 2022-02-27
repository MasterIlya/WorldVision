using System;
using System.Collections.Generic;
using System.Text;

namespace WorldVision.Services.Models
{
    public class ReviewModel
    {
        public int ReviewId { get; set; }
        public int UserId { get; set; }
        public string ReviewType { get; set; }
        public string Tags { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public byte AuthorScore { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool Delisted { get; set; }
    }
}
