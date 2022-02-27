using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldVision.Repositories.Items
{
    [Table("ReviewTypes")]
    public class ReviewTypeItem
    {
        [Key]
        public virtual int ReviewTypeId { get; set; }
        public virtual string ReviewType { get; set; }
    }
}
