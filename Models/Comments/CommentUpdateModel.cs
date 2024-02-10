namespace Anjeergram.Models.Comments;

public class CommentUpdateModel
{
    public long UserId { get; set; }
    public long PostId { get; set; }
    public string Content { get; set; }
    public DateTime EditedAt { get; set; } = DateTime.UtcNow;
}
