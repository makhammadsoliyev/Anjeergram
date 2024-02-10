using Anjeergram.Models.Commons;

namespace Anjeergram.Models.Posts;

public class Post : Auditable
{
    public long UserId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Content { get; set; }
    public string PictureUrl { get; set; }
    public long Likes { get; set; }
    public DateTime EditedAt { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
}