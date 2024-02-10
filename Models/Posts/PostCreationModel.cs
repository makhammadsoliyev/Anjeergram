namespace Anjeergram.Models.Posts;

public class PostCreationModel
{
    public long UserId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Content { get; set; }
    public string PictureUrl { get; set; }
}
