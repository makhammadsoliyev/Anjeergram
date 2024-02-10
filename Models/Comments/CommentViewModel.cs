namespace Anjeergram.Models.Comments;

public class CommentViewModel
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public long PostId { get; set; }
    public string Content { get; set; }
    public long Likes { get; set; }
    public DateTime EditedAt { get; set; }
    public DateTime Date { get; set; }
}