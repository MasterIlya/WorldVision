using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldVision.Repositories.Items
{
    [Table("ReviewTags")]
    public class ReviewTagItem
    {
        [Key]
        public virtual int TagId { get; set; }
        public virtual int ReviewId { get; set; }
        public virtual string Tag { get; set; }
    }
}
