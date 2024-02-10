namespace Anjeergram.Models.Users;

public class UserViewModel
{
    public long Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Picture { get; set; }
    public long Followers { get; set; }
    public long Followings { get; set; }
    public DateTime Date { get; set; }
}