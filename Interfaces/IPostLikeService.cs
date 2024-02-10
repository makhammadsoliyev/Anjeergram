using Anjeergram.Models.PostLikes;

namespace Anjeergram.Interfaces;

public interface IPostLikeService
{
    Task<PostLikeViewModel> AddAsync(PostLikeCreationModel postLike);
    Task<PostLikeViewModel> GetByIdAsync(long id);
    Task<bool> DeleteAsync(long id);
    Task<IEnumerable<PostLikeViewModel>> GetAllAsync();
    Task<IEnumerable<PostLikeViewModel>> GetAllByUserIdAsync(long userId);
    Task<IEnumerable<PostLikeViewModel>> GetAllByPostIdAsync(long postId);
}
