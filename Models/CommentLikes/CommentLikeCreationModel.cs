﻿namespace Anjeergram.Models.CommentLikes;

public class CommentLikeCreationModel
{
    public long UserId { get; set; }
    public long CommentId { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
}
