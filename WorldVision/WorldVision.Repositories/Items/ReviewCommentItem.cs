using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WorldVision.Repositories.Items
{
    [Table("ReviewComments")]
    public class ReviewCommentItem
    {
        [Key]
        public virtual int CommentId { get; set; }
        public virtual int UserId { get; set; }
        public virtual int ReviewId { get; set; }
        public virtual string Content { get; set; }
        public virtual DateTime CreateDate { get; set; }
    }
}
