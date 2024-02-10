using Anjeergram.Models.Commons;

namespace Anjeergram.Models.PostLikes;

public class PostLike : Auditable
{
    public long UserId { get; set; }
    public long PostId { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
}