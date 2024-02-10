namespace Anjeergram.Models.Posts;

public class PostUpdateModel
{
    public long UserId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Content { get; set; }
    public string PictureUrl { get; set; }
    public DateTime EditedAt { get; set; } = DateTime.UtcNow;
}
