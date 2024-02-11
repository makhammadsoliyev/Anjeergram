﻿namespace Anjeergram.Models.PostLikes;

public class PostLikeCreationModel
{
    public long UserId { get; set; }
    public long PostId { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
}
