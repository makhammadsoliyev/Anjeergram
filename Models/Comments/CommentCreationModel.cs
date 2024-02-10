namespace Anjeergram.Models.Comments;

public class CommentCreationModel
{
    public long UserId { get; set; }
    public long PostId { get; set; }
    public string Content { get; set; }
}
