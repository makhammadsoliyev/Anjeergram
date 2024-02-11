using Anjeergram.Models.Users;

namespace Anjeergram.Extensions;

public static class UserNameCheckerExtension
{
    public static bool IsUserNameExists(this IEnumerable<User> users, string username)
        => users.Where(u => u.Equals(username)).Any();
}
