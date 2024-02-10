using Anjeergram.Models.Posts;
using Anjeergram.Models.Users;

namespace Anjeergram.Models.PostLikes;

public class PostLikeViewModel
{
    public long Id { get; set; }
    public UserViewModel User { get; set; }
    public PostViewModel Post { get; set; }
    public DateTime Date { get; set; }
}