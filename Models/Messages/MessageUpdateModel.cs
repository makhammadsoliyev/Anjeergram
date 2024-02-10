namespace Anjeergram.Models.Messages;

public class MessageUpdateModel
{
    public long SourceUserId { get; set; }
    public long TargetUserId { get; set; }
    public string Content { get; set; }
    public DateTime EditedAt { get; set; } = DateTime.UtcNow;
}
