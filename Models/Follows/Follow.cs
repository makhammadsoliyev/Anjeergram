namespace Anjeergram.Models.Follows;

public class Follow
{
    public long FollowingUserId { get; set; }
    public long FollowedUserId { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
}
