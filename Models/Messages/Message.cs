using Anjeergram.Models.Commons;

namespace Anjeergram.Models.Messages;

public class Message : Auditable
{
    public long SourceUserId { get; set; }
    public long TargetUserId { get; set; }
    public string Content { get; set; }
    public DateTime EditedAt { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
}