using BlogApi.Data;
using BlogApi.DTOs;
using BlogApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApi.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET /api/posts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostResponseDto>>> GetAllPosts()
        {
            var posts = await _context.BlogPosts
                .Include(p => p.Comments)
                .Select(p => new PostResponseDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    Content = p.Content,
                    CreatedAt = p.CreatedAt,
                    CommentCount = p.Comments.Count
                })
                .ToListAsync();

            return Ok(posts);
        }

        // POST /api/posts
        [HttpPost]
        public async Task<ActionResult<PostResponseDto>> CreatePost(CreatePostDto dto)
        {
            var post = new BlogPost
            {
                Title = dto.Title,
                Content = dto.Content
            };

            _context.BlogPosts.Add(post);
            await _context.SaveChangesAsync();

            var response = new PostResponseDto
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                CreatedAt = post.CreatedAt,
                CommentCount = 0
            };

            return CreatedAtAction(nameof(GetPostById), new { id = post.Id }, response);
        }

        // GET /api/posts/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PostWithCommentsDto>> GetPostById(int id)
        {
            var post = await _context.BlogPosts
                .Include(p => p.Comments)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (post == null)
                return NotFound(new { message = "Post não encontrado" });

            var response = new PostWithCommentsDto
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                CreatedAt = post.CreatedAt,
                CommentCount = post.Comments.Count,
                Comments = post.Comments.Select(c => new CommentResponseDto
                {
                    Id = c.Id,
                    Content = c.Content,
                    CreatedAt = c.CreatedAt
                }).ToList()
            };

            return Ok(response);
        }

        // POST /api/posts/{id}/comments
        [HttpPost("{id}/comments")]
        public async Task<ActionResult<CommentResponseDto>> AddComment(int id, CreateCommentDto dto)
        {
            var post = await _context.BlogPosts.FindAsync(id);

            if (post == null)
                return NotFound(new { message = "Post não encontrado" });

            var comment = new Comment
            {
                Content = dto.Content,
                BlogPostId = id
            };

            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            var response = new CommentResponseDto
            {
                Id = comment.Id,
                Content = comment.Content,
                CreatedAt = comment.CreatedAt
            };

            return CreatedAtAction(nameof(GetPostById), new { id = post.Id }, response);
        }

    }
}