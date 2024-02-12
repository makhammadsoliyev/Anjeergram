using Anjeergram.Models.Commons;

namespace Anjeergram.Models.Follows;

public class Follow : Auditable
{
    public long FollowingUserId { get; set; }
    public long FollowedUserId { get; set; }
    public DateTime Date { get; set; }
}
