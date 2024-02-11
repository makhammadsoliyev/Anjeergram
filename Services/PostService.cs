using Anjeergram.Configurations;
using Anjeergram.Extensions;
using Anjeergram.Helpers;
using Anjeergram.Interfaces;
using Anjeergram.Models.Posts;

namespace Anjeergram.Services;

public class PostService : IPostService
{
    private List<Post> posts;
    private readonly IUserService userService;

    public PostService(IUserService userService)
    {
        this.userService = userService;
    }

    public async Task<PostViewModel> AddAsync(PostCreationModel post)
    {
        posts = await FileIO.ReadAsync<Post>(Constants.POSTS_PATH);
        var user = await userService.GetByIdAsync(post.UserId);

        var createdPost = post.ToMapMain();
        createdPost.Id = posts.GenerateId();

        posts.Add(createdPost);

        await FileIO.WriteAsync(Constants.POSTS_PATH, posts);

        return createdPost.ToMapView(user);
    }

    public async Task<long> DecrementLikeAsync(long id)
    {
        posts = await FileIO.ReadAsync<Post>(Constants.POSTS_PATH);
        var post = posts.FirstOrDefault(p => p.Id == id && !p.IsDeleted)
            ?? throw new Exception($"Post was not found with this id: {id}");

        post.Likes--;

        await FileIO.WriteAsync(Constants.POSTS_PATH, posts);

        return post.Likes;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        posts = await FileIO.ReadAsync<Post>(Constants.POSTS_PATH);
        var post = posts.FirstOrDefault(p => p.Id == id && !p.IsDeleted)
            ?? throw new Exception($"Post was not found with this id: {id}");

        post.IsDeleted = true;
        post.DeletedAt = DateTime.UtcNow;

        await FileIO.WriteAsync(Constants.POSTS_PATH, posts);

        return true;
    }

    public async Task<IEnumerable<PostViewModel>> GetAllAsync()
    {
        posts = await FileIO.ReadAsync<Post>(Constants.POSTS_PATH);
        posts = posts.FindAll(p => !p.IsDeleted);
        var result = new List<PostViewModel>();
        foreach (var post in posts)
        {
            var user = await userService.GetByIdAsync(post.UserId);
            result.Add(post.ToMapView(user));
        }

        return result;
    }

    public async Task<IEnumerable<PostViewModel>> GetAllAsync(long userId)
    {
        posts = await FileIO.ReadAsync<Post>(Constants.POSTS_PATH);
        posts = posts.FindAll(p => p.UserId == userId && !p.IsDeleted);
        var user = await userService.GetByIdAsync(userId);
        var result = new List<PostViewModel>();
        foreach (var post in posts)
            result.Add(post.ToMapView(user));

        return result;
    }

    public async Task<PostViewModel> GetByIdAsync(long id)
    {
        posts = await FileIO.ReadAsync<Post>(Constants.POSTS_PATH);
        var post = posts.FirstOrDefault(p => p.Id == id && !p.IsDeleted)
            ?? throw new Exception($"Post was not found with this id: {id}");
        var user = await userService.GetByIdAsync(post.UserId);

        return post.ToMapView(user);
    }

    public async Task<long> IncrementLikeAsync(long id)
    {
        posts = await FileIO.ReadAsync<Post>(Constants.POSTS_PATH);
        var post = posts.FirstOrDefault(p => p.Id == id && !p.IsDeleted)
            ?? throw new Exception($"Post was not found with this id: {id}");

        post.Likes++;

        await FileIO.WriteAsync(Constants.POSTS_PATH, posts);

        return post.Likes;
    }

    public async Task<PostViewModel> UpdateAsync(long id, PostUpdateModel post)
    {
        posts = await FileIO.ReadAsync<Post>(Constants.POSTS_PATH);
        var existPost = posts.FirstOrDefault(p => p.Id == id && !p.IsDeleted)
            ?? throw new Exception($"Post was not found with this id: {id}");
        var user = await userService.GetByIdAsync(id);

        existPost.Title = post.Title;
        existPost.UserId = post.UserId;
        existPost.Content = post.Content;
        existPost.EditedAt = post.EditedAt;
        existPost.UpdatedAt = DateTime.UtcNow;
        existPost.PictureUrl = post.PictureUrl;
        existPost.Description = post.Description;

        await FileIO.WriteAsync(Constants.POSTS_PATH, posts);

        return existPost.ToMapView(user);
    }
}
