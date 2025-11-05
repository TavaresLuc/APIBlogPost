namespace BlogApi.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Chave estrangeira
        public int BlogPostId { get; set; }

        // Relacionamento: Um comentário pertence a um post
        public BlogPost BlogPost { get; set; } = null!;
    }
}