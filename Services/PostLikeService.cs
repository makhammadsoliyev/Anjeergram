using Anjeergram.Configurations;
using Anjeergram.Extensions;
using Anjeergram.Helpers;
using Anjeergram.Interfaces;
using Anjeergram.Models.PostLikes;

namespace Anjeergram.Services;

public class PostLikeService : IPostLikeService
{
    private List<PostLike> postLikes;
    private readonly IPostService postService;
    private readonly IUserService userService;

    public PostLikeService(IUserService userService, IPostService postService)
    {
        this.userService = userService;
        this.postService = postService;
    }

    public async Task<PostLikeViewModel> AddAsync(PostLikeCreationModel postLike)
    {
        var user = await userService.GetByIdAsync(postLike.UserId);
        var post = await postService.GetByIdAsync(postLike.PostId);
        postLikes = await FileIO.ReadAsync<PostLike>(Constants.POST_LIKES_PATH);

        var createdLike = postLike.ToMapMain();
        createdLike.Id = postLikes.GenerateId();

        postLikes.Add(createdLike);

        await postService.IncrementLikeAsync(createdLike.PostId);
        await FileIO.WriteAsync(Constants.POST_LIKES_PATH, postLikes);

        return createdLike.ToMapView(user, post);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        postLikes = await FileIO.ReadAsync<PostLike>(Constants.POST_LIKES_PATH);
        var like = postLikes.FirstOrDefault(p => p.Id == id && !p.IsDeleted)
            ?? throw new Exception($"PostLike was not found with this id: {id}");
        
        like.IsDeleted = true;
        like.DeletedAt = DateTime.UtcNow;

        await postService.DecrementLikeAsync(like.PostId);
        await FileIO.WriteAsync(Constants.POST_LIKES_PATH, postLikes);

        return true;
    }

    public async Task<IEnumerable<PostLikeViewModel>> GetAllAsync()
    {
        postLikes = await FileIO.ReadAsync<PostLike>(Constants.POST_LIKES_PATH);
        postLikes = postLikes.FindAll(pl => !pl.IsDeleted);
        var result = new List<PostLikeViewModel>();

        foreach (var pl in postLikes)
        {
            var user = await userService.GetByIdAsync(pl.UserId);
            var post = await postService.GetByIdAsync(pl.PostId);
            result.Add(pl.ToMapView(user, post));
        }

        return result;
    }

    public async Task<IEnumerable<PostLikeViewModel>> GetAllByPostIdAsync(long postId)
    {
        postLikes = await FileIO.ReadAsync<PostLike>(Constants.POST_LIKES_PATH);
        postLikes = postLikes.FindAll(pl => !pl.IsDeleted && pl.PostId == postId);
        var result = new List<PostLikeViewModel>();
        var post = await postService.GetByIdAsync(postId);

        foreach (var pl in postLikes)
        {
            var user = await userService.GetByIdAsync(pl.UserId);
            result.Add(pl.ToMapView(user, post));
        }

        return result;
    }

    public async Task<IEnumerable<PostLikeViewModel>> GetAllByUserIdAsync(long userId)
    {
        postLikes = await FileIO.ReadAsync<PostLike>(Constants.POST_LIKES_PATH);
        postLikes = postLikes.FindAll(pl => !pl.IsDeleted && pl.UserId == userId);
        var result = new List<PostLikeViewModel>();
        var user = await userService.GetByIdAsync(userId);

        foreach (var pl in postLikes)
        {
            var post = await postService.GetByIdAsync(pl.PostId);
            result.Add(pl.ToMapView(user, post));
        }

        return result;
    }

    public async Task<PostLikeViewModel> GetByIdAsync(long id)
    {
        postLikes = await FileIO.ReadAsync<PostLike>(Constants.POST_LIKES_PATH);
        var like = postLikes.FirstOrDefault(p => p.Id == id && !p.IsDeleted)
            ?? throw new Exception($"PostLike was not found with this id: {id}");

        var user = await userService.GetByIdAsync(like.UserId);
        var post = await postService.GetByIdAsync(like.PostId);

        return like.ToMapView(user, post);
    }
}
