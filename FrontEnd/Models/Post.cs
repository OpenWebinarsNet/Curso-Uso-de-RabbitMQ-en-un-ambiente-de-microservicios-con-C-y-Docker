namespace FrontEnd.Models
{
    public class Post
    {
        public Guid PostId { get; set; }
        public string PostTitle { get; set; }
        public string PostText { get; set; }
        public DateTime? PostCreationDate { get; set; }
        public Guid UserId { get; set; }
    }
}
