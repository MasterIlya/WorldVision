using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WorldVision.Services.Models
{
    public class CreateReviewModel
    {
        public int ReviewId { get; set; }
        public int UserId { get; set; }
        public int ReviewTypeId { get; set; }
        public List<TagModel> Tags { get; set;}
        [Required(ErrorMessage = "Title not specified")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Content not specified")]
        public string Content { get; set; }
        [Required(ErrorMessage = "Author Score not specified")]
        [Range(1, 5, ErrorMessage = "Author Score can be only from 1 to 5")]
        public byte AuthorScore { get; set; }
    }
}
