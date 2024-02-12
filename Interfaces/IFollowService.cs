using Anjeergram.Models.Follows;

namespace Anjeergram.Interfaces;

public interface IFollowService
{
    Task<FollowViewModel> AddAsync(FollowCreationModel follow);
    Task<FollowViewModel> GetByIdAsync(long id);
    Task<bool> DeleteAsync(long id);
    Task<IEnumerable<FollowViewModel>> GetAllAsync();
    Task<IEnumerable<FollowViewModel>> GetAllFollowersByUserIdAsync(long userId);
    Task<IEnumerable<FollowViewModel>> GetAllFollowingsByUserIdAsync(long userId);
}
