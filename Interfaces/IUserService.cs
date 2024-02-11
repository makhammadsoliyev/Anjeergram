using Anjeergram.Models.Users;

namespace Anjeergram.Interfaces;

public interface IUserService
{
    Task<UserViewModel> AddAsync(UserCreationModel user);
    Task<UserViewModel> GetByIdAsync(long id);
    Task<UserViewModel> UpdateAsync(long id, UserUpdateModel user, bool isUsedDeleted = false);
    Task<bool> DeleteAsync(long id);
    Task<IEnumerable<UserViewModel>> GetAllAsync();
}
