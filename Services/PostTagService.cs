using Anjeergram.Configurations;
using Anjeergram.Extensions;
using Anjeergram.Helpers;
using Anjeergram.Interfaces;
using Anjeergram.Models.PostTags;

namespace Anjeergram.Services;

public class PostTagService : IPostTagService
{
    private List<PostTag> postTags;
    private readonly ITagService tagService;
    private readonly IPostService postService;

    public PostTagService(ITagService tagService, IPostService postService)
    {
        this.tagService = tagService;
        this.postService = postService;
    }

    public async Task<PostTagViewModel> AddAsync(PostTagCreationModel postTag)
    {
        var tag = await tagService.GetByIdAsync(postTag.TagId);
        var post = await postService.GetByIdAsync(postTag.PostId);
        postTags = await FileIO.ReadAsync<PostTag>(Constants.POST_TAGS_PATH);

        var createdPostTag = postTag.ToMapMain();
        createdPostTag.Id = postTags.GenerateId();

        postTags.Add(createdPostTag);

        await FileIO.WriteAsync(Constants.POST_TAGS_PATH, postTags);

        return createdPostTag.ToMapView(post, tag);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        postTags = await FileIO.ReadAsync<PostTag>(Constants.POST_TAGS_PATH);
        var postTag = postTags.FirstOrDefault(pt => pt.Id == id && !pt.IsDeleted)
            ?? throw new Exception($"PostTag was not found with this id: {id}");

        postTag.IsDeleted = true;
        postTag.DeletedAt = DateTime.UtcNow;

        await FileIO.WriteAsync(Constants.POST_TAGS_PATH, postTags);

        return true;
    }

    public async Task<IEnumerable<PostTagViewModel>> GetAllAsync()
    {
        postTags = await FileIO.ReadAsync<PostTag>(Constants.POST_TAGS_PATH);
        postTags = postTags.FindAll(pt => !pt.IsDeleted);
        var result = new List<PostTagViewModel>();

        foreach (var postTag in postTags)
        {
            var post = await postService.GetByIdAsync(postTag.PostId);
            var tag = await tagService.GetByIdAsync(postTag.TagId);
            result.Add(postTag.ToMapView(post, tag));
        }

        return result;
    }

    public async Task<IEnumerable<PostTagViewModel>> GetAllByPostIdAsync(long postId)
    {
        var post = await postService.GetByIdAsync(postId);
        postTags = await FileIO.ReadAsync<PostTag>(Constants.POST_TAGS_PATH);
        postTags = postTags.FindAll(pt => !pt.IsDeleted);
        var result = new List<PostTagViewModel>();

        foreach (var postTag in postTags)
        {
            var tag = await tagService.GetByIdAsync(postTag.TagId);
            result.Add(postTag.ToMapView(post, tag));
        }

        return result;
    }

    public async Task<IEnumerable<PostTagViewModel>> GetAllByTagIdAsync(long tagId)
    {
        var tag = await tagService.GetByIdAsync(tagId);
        postTags = await FileIO.ReadAsync<PostTag>(Constants.POST_TAGS_PATH);
        postTags = postTags.FindAll(pt => !pt.IsDeleted);
        var result = new List<PostTagViewModel>();

        foreach (var postTag in postTags)
        {
            var post = await postService.GetByIdAsync(postTag.PostId);
            result.Add(postTag.ToMapView(post, tag));
        }

        return result;

    }

    public async Task<PostTagViewModel> GetById(long id)
    {
        postTags = await FileIO.ReadAsync<PostTag>(Constants.POST_TAGS_PATH);
        var postTag = postTags.FirstOrDefault(pt => pt.Id == id && !pt.IsDeleted)
            ?? throw new Exception($"PostTag was not found with this id: {id}");
        var tag = await tagService.GetByIdAsync(postTag.TagId);
        var post = await postService.GetByIdAsync(postTag.PostId);

        return postTag.ToMapView(post, tag);
    }
}
