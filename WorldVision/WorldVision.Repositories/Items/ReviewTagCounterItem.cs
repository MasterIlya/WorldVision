using System.ComponentModel.DataAnnotations;

namespace WorldVision.Repositories.Items
{
    public class ReviewTagCounterItem
    {
        [Key]
        public virtual string Tag { get; set; }
        public virtual int TagCount { get; set; }
    }
}
