using Anjeergram.Models.Commons;

namespace Anjeergram.Models.CommentLikes;

public class CommentLike : Auditable
{
    public long UserId { get; set; }
    public long CommentId { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
}
