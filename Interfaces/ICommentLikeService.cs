using Anjeergram.Models.CommentLikes;

namespace Anjeergram.Interfaces;

public interface ICommentLikeService
{
    Task<CommentLikeViewModel> AddAsync(CommentLikeCreationModel commentLike);
    Task<CommentLikeViewModel> GetByIdAsync(long id);
    Task<CommentLikeViewModel> UpdateAsync(long id, CommentLikeCreationModel commentLike);
    Task<bool> DeleteAsync(long id);
    Task<IEnumerable<CommentLikeViewModel>> GetAllAsync();
    Task<IEnumerable<CommentLikeViewModel>> GetAllByUserIdAsync(long userId);
    Task<IEnumerable<CommentLikeViewModel>> GetAllByCommentIdAsync(long commentId);
}
