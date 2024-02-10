using Anjeergram.Models.Users;

namespace Anjeergram.Models.Messages;

public class MessageViewModel
{
    public long Id { get; set; }
    public UserViewModel SourceUser { get; set; }
    public UserViewModel TargetUser { get; set; }
    public string Content { get; set; }
    public DateTime EditedAt { get; set; }
    public DateTime Date { get; set; }
}