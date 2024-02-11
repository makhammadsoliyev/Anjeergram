using Anjeergram.Models.Categories;

namespace Anjeergram.Interfaces;

public interface ICategoryService
{
    Task<CategoryViewModel> AddAsync(CategoryCreationModel category);
    Task<CategoryViewModel> GetByIdAsync(long id);
    Task<CategoryViewModel> UpdateAsync(long id, CategoryUpdateModel category);
    Task<bool> DeleteAsync(long id);
    Task<IEnumerable<CategoryViewModel>> GetAllAsync();
}
