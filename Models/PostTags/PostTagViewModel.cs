﻿using Anjeergram.Models.Posts;
using Anjeergram.Models.Tags;

namespace Anjeergram.Models.PostTags;

public class PostTagViewModel
{
    public long Id { get; set; }
    public PostViewModel Post { get; set; }
    public TagViewModel Tag { get; set; }
}