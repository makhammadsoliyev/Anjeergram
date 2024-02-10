using Anjeergram.Models.Posts;
using Anjeergram.Models.Users;

namespace Anjeergram.Models.Comments;

public class CommentViewModel
{
    public long Id { get; set; }
    public UserViewModel User { get; set; }
    public PostViewModel Post { get; set; }
    public string Content { get; set; }
    public long Likes { get; set; }
    public DateTime EditedAt { get; set; }
    public DateTime Date { get; set; }
}