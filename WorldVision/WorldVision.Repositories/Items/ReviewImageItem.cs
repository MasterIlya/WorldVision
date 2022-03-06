using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WorldVision.Repositories.Items
{
    [Table("ReviewImages")]
    public class ReviewImageItem
    {
        [Key]
        public virtual int ImageId { get; set; }
        public virtual int ReviewId { get; set; }
        public virtual string ImageURL { get; set; }
        public virtual long Size { get; set; }
    }
}
