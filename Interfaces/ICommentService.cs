using Anjeergram.Models.Comments;

namespace Anjeergram.Interfaces;

public interface ICommentService
{
    Task<CommentViewModel> AddAsync(CommentCreationModel comment);
    Task<CommentViewModel> GetById(long id);
    Task<CommentViewModel> UpdateAsync(long id, CommentUpdateModel comment);
    Task<bool> DeleteAsync(long id);
    Task<IEnumerable<CommentViewModel>> GetAllAsync();
    Task<IEnumerable<CommentViewModel>> GetAllAsyncByUserId(long userId);
    Task<IEnumerable<CommentViewModel>> GetAllAsyncByPostId(long postId);
    Task<long> IncrementLikeAsync(long id);
    Task<long> DecrementLikeAsync(long id);
}
