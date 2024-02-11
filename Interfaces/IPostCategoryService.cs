using Anjeergram.Models.PostCategories;

namespace Anjeergram.Interfaces;

public interface IPostCategoryService
{
    Task<PostCategoryViewModel> AddAsync(PostCategoryCreationModel postCategory);
    Task<PostCategoryViewModel> GetByIdAsync(long id);
    Task<bool> DeleteAsync(long id);
    Task<IEnumerable<PostCategoryViewModel>> GetAllAsync();
    Task<IEnumerable<PostCategoryViewModel>> GetAllByPostIdAsync(long postId);
    Task<IEnumerable<PostCategoryViewModel>> GetAllByCategoryIdAsync(long categoryId);
}
