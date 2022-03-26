namespace WorldVision.Services.Models
{
    public class CreateCommentModel
    {
        public int ReviewId { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }
    }
}
