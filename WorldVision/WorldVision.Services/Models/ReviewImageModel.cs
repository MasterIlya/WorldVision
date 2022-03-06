using System;
using System.Collections.Generic;
using System.Text;

namespace WorldVision.Services.Models
{
    public class ReviewImageModel
    {
        public int ImageId { get; set; }
        public string ImageName { get; set; }
        public string ImageURL { get; set; }
        public long ImageSize { get; set; }
    }
}
