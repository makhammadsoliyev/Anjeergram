﻿namespace Anjeergram.Models.Follows;

public class FollowCreationModel
{
    public long FollowingUserId { get; set; }
    public long FollowedUserId { get; set; }
}
