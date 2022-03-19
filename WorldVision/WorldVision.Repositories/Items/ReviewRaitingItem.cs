using System;
using System.ComponentModel.DataAnnotations;

namespace WorldVision.Repositories.Items
{
    public class ReviewRaitingItem
    {
        [Key]
        public virtual int ReviewId { get; set; }
        public virtual int UserId { get; set; }
        public virtual int ReviewTypeId { get; set; }
        public virtual string Title { get; set; }
        public virtual string Content { get; set; }
        public virtual byte AuthorScore { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual DateTime UpdateDate { get; set; }
        public virtual int LikeCount { get; set; }
    }
}
