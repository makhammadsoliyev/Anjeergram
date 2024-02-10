using Anjeergram.Models.Users;

namespace Anjeergram.Models.Follows;

public class FollowViewModel
{
    public long Id { get; set; }
    public UserViewModel FollowingUser { get; set; }
    public UserViewModel FollowedUser { get; set; }
    public DateTime Date { get; set; }
}