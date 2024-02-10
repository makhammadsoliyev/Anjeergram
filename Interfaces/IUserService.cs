using Anjeergram.Models.Users;

namespace Anjeergram.Interfaces;

public interface IUserService
{
    Task<UserViewModel> AddAsync(UserCreationModel user);
    Task<UserViewModel> GetByIdAsync(long id);
    Task<UserViewModel> UpdateAsync(long id, UserUpdateModel user);
    Task<bool> DeleteAsync(long id);
    Task<IEnumerable<UserViewModel>> GetAllAsync();
}
