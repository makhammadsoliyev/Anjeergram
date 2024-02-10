using Anjeergram.Models.Posts;

namespace Anjeergram.Interfaces;

public interface IPostService
{
    Task<PostViewModel> AddAsync(PostCreationModel post);
    Task<PostViewModel> GetByIdAsync(long id);
    Task<PostViewModel> UpdateAsync(long id, PostUpdateModel post);
    Task<bool> DeleteAsync(long id);
    Task<IEnumerable<PostViewModel>> GetAllAsync();
    Task<IEnumerable<PostViewModel>> GetAllAsync(long userId);
    Task<long> IncrementLikeAsync(long id);
    Task<long> DecrementLikeAsync(long id);
}
