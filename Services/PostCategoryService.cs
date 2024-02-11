using Anjeergram.Configurations;
using Anjeergram.Extensions;
using Anjeergram.Helpers;
using Anjeergram.Interfaces;
using Anjeergram.Models.Categories;
using Anjeergram.Models.PostCategories;

namespace Anjeergram.Services;

public class PostCategoryService : IPostCategoryService
{
    private List<PostCategory> postCategories;
    private readonly IPostService postService;
    private readonly ICategoryService categoryService;

    public PostCategoryService(ICategoryService categoryService, IPostService postService)
    {
        this.postService = postService;
        this.categoryService = categoryService;
    }

    public async Task<PostCategoryViewModel> AddAsync(PostCategoryCreationModel postCategory)
    {
        var post = await postService.GetByIdAsync(postCategory.PostId);
        var category = await categoryService.GetByIdAsync(postCategory.CategoryId);
        postCategories = await FileIO.ReadAsync<PostCategory>(Constants.POST_CATEGORIES_PATH);
        var createdPostCategory = postCategory.ToMapMain();
        createdPostCategory.Id = postCategories.GenerateId();

        postCategories.Add(createdPostCategory);

        await FileIO.WriteAsync(Constants.POST_CATEGORIES_PATH, postCategories);

        return createdPostCategory.ToMapView(category, post);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        postCategories = await FileIO.ReadAsync<PostCategory>(Constants.POST_CATEGORIES_PATH);
        var postCategory = postCategories.FirstOrDefault(pc => pc.Id == id && !pc.IsDeleted)
            ?? throw new Exception($"PostCategory was not found with this id: {id}");

        postCategory.IsDeleted = true;
        postCategory.DeletedAt = DateTime.UtcNow;

        await FileIO.WriteAsync(Constants.POST_CATEGORIES_PATH, postCategories);

        return true;
    }

    public async Task<IEnumerable<PostCategoryViewModel>> GetAllAsync()
    {
        postCategories = await FileIO.ReadAsync<PostCategory>(Constants.POST_CATEGORIES_PATH);
        postCategories = postCategories.FindAll(pc => !pc.IsDeleted);
        var result = new List<PostCategoryViewModel>();

        foreach (var pc in postCategories)
        {
            var post = await postService.GetByIdAsync(pc.PostId);
            var category = await categoryService.GetByIdAsync(pc.CategoryId);
            result.Add(pc.ToMapView(category, post));
        }

        return result;
    }

    public async Task<IEnumerable<PostCategoryViewModel>> GetAllByCategoryIdAsync(long categoryId)
    {
        var category = await categoryService.GetByIdAsync(categoryId);
        postCategories = await FileIO.ReadAsync<PostCategory>(Constants.POST_CATEGORIES_PATH);
        postCategories = postCategories.FindAll(pc => !pc.IsDeleted && pc.CategoryId == categoryId);
        var result = new List<PostCategoryViewModel>();

        foreach (var pc in postCategories)
        {
            var post = await postService.GetByIdAsync(pc.PostId);
            result.Add(pc.ToMapView(category, post));
        }

        return result;
    }

    public async Task<IEnumerable<PostCategoryViewModel>> GetAllByPostIdAsync(long postId)
    {
        var post = await postService.GetByIdAsync(postId);
        postCategories = await FileIO.ReadAsync<PostCategory>(Constants.POST_CATEGORIES_PATH);
        postCategories = postCategories.FindAll(pc => !pc.IsDeleted && pc.PostId == postId);
        var result = new List<PostCategoryViewModel>();

        foreach (var pc in postCategories)
        {
            var category = await categoryService.GetByIdAsync(pc.CategoryId);
            result.Add(pc.ToMapView(category, post));
        }

        return result;
    }

    public async Task<PostCategoryViewModel> GetByIdAsync(long id)
    {
        postCategories = await FileIO.ReadAsync<PostCategory>(Constants.POST_CATEGORIES_PATH);
        var postCategory = postCategories.FirstOrDefault(pc => pc.Id == id && !pc.IsDeleted)
            ?? throw new Exception($"PostCategory was not found with this id: {id}");
        var post = await postService.GetByIdAsync(postCategory.PostId);
        var category = await categoryService.GetByIdAsync(postCategory.PostId);

        return postCategory.ToMapView(category, post);
    }
}