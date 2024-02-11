using Anjeergram.Configurations;
using Anjeergram.Extensions;
using Anjeergram.Helpers;
using Anjeergram.Interfaces;
using Anjeergram.Models.Comments;

namespace Anjeergram.Services;

public class CommentService : ICommentService
{
    private List<Comment> comments;
    private IUserService userService;
    private IPostService postService;

    public CommentService(IPostService postService, IUserService userService)
    {
        this.postService = postService;
        this.userService = userService;
    }

    public async Task<CommentViewModel> AddAsync(CommentCreationModel comment)
    {
        var post = await postService.GetByIdAsync(comment.PostId);
        var user = await userService.GetByIdAsync(comment.UserId);
        comments = await FileIO.ReadAsync<Comment>(Constants.COMMENTS_PATH);

        var createdComment = comment.ToMapMain();
        createdComment.Id = comments.GenerateId();

        await FileIO.WriteAsync(Constants.COMMENTS_PATH, comments);

        return createdComment.ToMapView(user, post);
    }

    public async Task<long> DecrementLikeAsync(long id)
    {
        comments = await FileIO.ReadAsync<Comment>(Constants.COMMENTS_PATH);
        var comment = comments.FirstOrDefault(c => !c.IsDeleted && c.Id == id)
            ?? throw new Exception($"Comment was not found with this id: {id}");

        comment.Likes--;

        await FileIO.WriteAsync(Constants.COMMENTS_PATH, comments);

        return comment.Likes;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        comments = await FileIO.ReadAsync<Comment>(Constants.COMMENTS_PATH);
        var comment = comments.FirstOrDefault(c => !c.IsDeleted && c.Id == id)
            ?? throw new Exception($"Comment was not found with this id: {id}");

        comment.IsDeleted = true;
        comment.DeletedAt = DateTime.UtcNow;

        await FileIO.WriteAsync(Constants.COMMENTS_PATH, comments);

        return true;
    }

    public async Task<IEnumerable<CommentViewModel>> GetAllAsync()
    {
        comments = await FileIO.ReadAsync<Comment>(Constants.COMMENTS_PATH);
        comments = comments.FindAll(c => !c.IsDeleted);
        var result = new List<CommentViewModel>();

        foreach (var comment in comments)
        {
            var post = await postService.GetByIdAsync(comment.PostId);
            var user = await userService.GetByIdAsync(comment.UserId);
            result.Add(comment.ToMapView(user, post));
        }

        return result;
    }

    public async Task<IEnumerable<CommentViewModel>> GetAllAsyncByPostId(long postId)
    {
        var post = await postService.GetByIdAsync(postId);
        comments = await FileIO.ReadAsync<Comment>(Constants.COMMENTS_PATH);
        comments = comments.FindAll(c => !c.IsDeleted && c.PostId == postId);
        var result = new List<CommentViewModel>();

        foreach (var comment in comments)
        {
            var user = await userService.GetByIdAsync(comment.UserId);
            result.Add(comment.ToMapView(user, post));
        }

        return result;
    }

    public async Task<IEnumerable<CommentViewModel>> GetAllAsyncByUserId(long userId)
    {
        var user = await userService.GetByIdAsync(userId);
        comments = await FileIO.ReadAsync<Comment>(Constants.COMMENTS_PATH);
        comments = comments.FindAll(c => !c.IsDeleted && c.UserId == userId);
        var result = new List<CommentViewModel>();

        foreach (var comment in comments)
        {
            var post = await postService.GetByIdAsync(comment.PostId);
            result.Add(comment.ToMapView(user, post));
        }

        return result;
    }

    public async Task<CommentViewModel> GetById(long id)
    {
        comments = await FileIO.ReadAsync<Comment>(Constants.COMMENTS_PATH);
        var comment = comments.FirstOrDefault(c => !c.IsDeleted && c.Id == id)
            ?? throw new Exception($"Comment was not found with this id: {id}");
        var post = await postService.GetByIdAsync(comment.PostId);
        var user = await userService.GetByIdAsync(comment.UserId);

        return comment.ToMapView(user, post);
    }

    public async Task<long> IncrementLikeAsync(long id)
    {
        comments = await FileIO.ReadAsync<Comment>(Constants.COMMENTS_PATH);
        var comment = comments.FirstOrDefault(c => !c.IsDeleted && c.Id == id)
            ?? throw new Exception($"Comment was not found with this id: {id}");

        comment.Likes++;

        await FileIO.WriteAsync(Constants.COMMENTS_PATH, comments);

        return comment.Likes;
    }

    public async Task<CommentViewModel> UpdateAsync(long id, CommentUpdateModel comment)
    {
        var post = await postService.GetByIdAsync(comment.PostId);
        var user = await userService.GetByIdAsync(comment.UserId);
        comments = await FileIO.ReadAsync<Comment>(Constants.COMMENTS_PATH);
        var existComment = comments.FirstOrDefault(c => !c.IsDeleted && c.Id == id)
            ?? throw new Exception($"Comment was not found with this id: {id}");

        existComment.Id = id;
        existComment.UserId = comment.UserId;
        existComment.PostId = comment.PostId;
        existComment.Content = comment.Content;
        existComment.UpdatedAt = DateTime.UtcNow;

        await FileIO.WriteAsync(Constants.COMMENTS_PATH, comments);

        return existComment.ToMapView(user, post);
    }
}
