using Anjeergram.Models.Comments;
using Anjeergram.Models.Users;

namespace Anjeergram.Models.CommentLikes;

public class CommentLikeViewModel
{
    public long Id { get; set; }
    public UserViewModel User { get; set; }
    public CommentViewModel Comment { get; set; }
    public DateTime Date { get; set; }
}