using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldVision.Repositories.Items
{
    [Table("ReviewLikes")]
    public class ReviewLikeItem
    {
        [Key]
        public virtual int LikeId { get; set; }
        public virtual int UserId { get; set; }
        public virtual int ReviewId { get; set; }
    }
}
