using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldVision.Repositories.Items
{
    [Table("Reviews")]
    public class ReviewItem
    {
        [Key]
        public virtual int ReviewId { get; set; }
        public virtual int UserId { get; set; }
        public virtual int ReviewTypeId { get; set; }
        public virtual string Tags { get; set; }
        public virtual string Title { get; set; }
        public virtual string Content { get; set; }
        public virtual byte AuthorScore { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual DateTime UpdateDate { get; set; }
        public virtual bool Delisted { get; set; }
    }
}
