namespace BlogApi.Models
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Um post tem vários comentários
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}