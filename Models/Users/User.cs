using Anjeergram.Models.Commons;

namespace Anjeergram.Models.Users;

public class User : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string PictureUrl { get; set; }
    public long Followers { get; set; }
    public long Followings { get; set; }
    public DateTime Date { get; set; } = DateTime.UtcNow;
}