using Anjeergram.Configurations;
using Anjeergram.Extensions;
using Anjeergram.Helpers;
using Anjeergram.Interfaces;
using Anjeergram.Models.CommentLikes;

namespace Anjeergram.Services;

public class CommentLikeService : ICommentLikeService
{
    private List<CommentLike> commentLikes;
    private readonly IUserService userService;
    private readonly ICommentService commentService;

    public CommentLikeService(ICommentService commentService, IUserService userService)
    {
        this.userService = userService;
        this.commentService = commentService;
    }

    public async Task<CommentLikeViewModel> AddAsync(CommentLikeCreationModel commentLike)
    {
        var user = await userService.GetByIdAsync(commentLike.UserId);
        var comment = await commentService.GetByIdAsync(commentLike.CommentId);
        commentLikes = await FileIO.ReadAsync<CommentLike>(Constants.COMMENT_LIKES_PATH);

        var createdLike = commentLike.ToMapMain();
        createdLike.Id = commentLikes.GenerateId();

        commentLikes.Add(createdLike);

        await commentService.IncrementLikeAsync(commentLike.CommentId);
        await FileIO.WriteAsync(Constants.COMMENT_LIKES_PATH, commentLikes);

        return createdLike.ToMapView(user, comment);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        commentLikes = await FileIO.ReadAsync<CommentLike>(Constants.COMMENT_LIKES_PATH);
        var like = commentLikes.FirstOrDefault(cl => !cl.IsDeleted && cl.Id == id)
            ?? throw new Exception($"CommentLike was not found with this id: {commentLikes}");

        like.IsDeleted = true;
        like.DeletedAt = DateTime.UtcNow;

        await commentService.DecrementLikeAsync(like.CommentId);
        await FileIO.WriteAsync(Constants.COMMENT_LIKES_PATH, commentLikes);

        return true;
    }

    public async Task<IEnumerable<CommentLikeViewModel>> GetAllAsync()
    {
        commentLikes = await FileIO.ReadAsync<CommentLike>(Constants.COMMENT_LIKES_PATH);
        commentLikes = commentLikes.FindAll(cl => !cl.IsDeleted);

        var result = new List<CommentLikeViewModel>();
        foreach (var cl in commentLikes)
        {
            var user = await userService.GetByIdAsync(cl.UserId);
            var comment = await commentService.GetByIdAsync(cl.CommentId);
            result.Add(cl.ToMapView(user, comment));
        }

        return result;
    }

    public async Task<IEnumerable<CommentLikeViewModel>> GetAllByCommentIdAsync(long commentId)
    {
        var comment = await commentService.GetByIdAsync(commentId);
        commentLikes = await FileIO.ReadAsync<CommentLike>(Constants.COMMENT_LIKES_PATH);
        commentLikes = commentLikes.FindAll(cl => !cl.IsDeleted && cl.CommentId == commentId);

        var result = new List<CommentLikeViewModel>();
        foreach (var cl in commentLikes)
        {
            var user = await userService.GetByIdAsync(cl.UserId);
            result.Add(cl.ToMapView(user, comment));
        }

        return result;
    }

    public async Task<IEnumerable<CommentLikeViewModel>> GetAllByUserIdAsync(long userId)
    {
        var user = await userService.GetByIdAsync(userId);
        commentLikes = await FileIO.ReadAsync<CommentLike>(Constants.COMMENT_LIKES_PATH);
        commentLikes = commentLikes.FindAll(cl => !cl.IsDeleted && cl.UserId == userId);

        var result = new List<CommentLikeViewModel>();
        foreach (var cl in commentLikes)
        {
            var comment = await commentService.GetByIdAsync(cl.CommentId);
            result.Add(cl.ToMapView(user, comment));
        }

        return result;
    }

    public async Task<CommentLikeViewModel> GetByIdAsync(long id)
    {
        commentLikes = await FileIO.ReadAsync<CommentLike>(Constants.COMMENT_LIKES_PATH);
        var like = commentLikes.FirstOrDefault(cl => !cl.IsDeleted && cl.Id == id)
            ?? throw new Exception($"CommentLike was not found with this id: {commentLikes}");
        var user = await userService.GetByIdAsync(like.UserId);
        var comment = await commentService.GetByIdAsync(like.CommentId);

        return like.ToMapView(user, comment);
    }

    public async Task<CommentLikeViewModel> UpdateAsync(long id, CommentLikeCreationModel commentLike)
    {
        var user = await userService.GetByIdAsync(commentLike.UserId);
        var comment = await commentService.GetByIdAsync(commentLike.CommentId);
        commentLikes = await FileIO.ReadAsync<CommentLike>(Constants.COMMENT_LIKES_PATH);
        var existLike = commentLikes.FirstOrDefault(cl => !cl.IsDeleted && cl.Id == id)
            ?? throw new Exception($"CommentLike was not found with this id: {commentLikes}");

        existLike.UpdatedAt = DateTime.UtcNow;
        existLike.CommentId = commentLike.CommentId;
        existLike.UserId = commentLike.UserId;

        await FileIO.WriteAsync(Constants.COMMENT_LIKES_PATH, commentLikes);

        return existLike.ToMapView(user, comment);
    }
}
