namespace Anjeergram.Models.Messages;

public class MessageCreationModel
{
    public long SourceUserId { get; set; }
    public long TargetUserId { get; set; }
    public string Content { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
}
