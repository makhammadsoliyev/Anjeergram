using Anjeergram.Configurations;
using Anjeergram.Extensions;
using Anjeergram.Helpers;
using Anjeergram.Interfaces;
using Anjeergram.Models.Categories;

namespace Anjeergram.Services;

public class CategoryService : ICategoryService
{
    private List<Category> categories;

    public async Task<CategoryViewModel> AddAsync(CategoryCreationModel category)
    {
        categories = await FileIO.ReadAsync<Category>(Constants.CATEGORIES_PATH);
        var createdCategory = category.ToMapMain();
        createdCategory.Id = categories.GenerateId();

        categories.Add(createdCategory);

        await FileIO.WriteAsync(Constants.CATEGORIES_PATH, categories);

        return createdCategory.ToMapView();
    }

    public async Task<bool> DeleteAsync(long id)
    {
        categories = await FileIO.ReadAsync<Category>(Constants.CATEGORIES_PATH);
        var category = categories.FirstOrDefault(c => !c.IsDeleted && c.Id == id)
            ?? throw new Exception($"Category was not found with this id: {id}");

        category.IsDeleted = true;
        category.DeletedAt = DateTime.UtcNow;

        await FileIO.WriteAsync(Constants.CATEGORIES_PATH, categories);

        return true;
    }

    public async Task<IEnumerable<CategoryViewModel>> GetAllAsync()
    {
        categories = await FileIO.ReadAsync<Category>(Constants.CATEGORIES_PATH);
        return categories.FindAll(c => !c.IsDeleted).Select(c => c.ToMapView());
    }

    public async Task<CategoryViewModel> GetById(long id)
    {
        categories = await FileIO.ReadAsync<Category>(Constants.CATEGORIES_PATH);
        var category = categories.FirstOrDefault(c => !c.IsDeleted && c.Id == id)
            ?? throw new Exception($"Category was not found with this id: {id}");

        return category.ToMapView();
    }

    public async Task<CategoryViewModel> UpdateAsync(long id, CategoryUpdateModel category)
    {
        categories = await FileIO.ReadAsync<Category>(Constants.CATEGORIES_PATH);
        var existCategory = categories.FirstOrDefault(c => !c.IsDeleted && c.Id == id)
            ?? throw new Exception($"Category was not found with this id: {id}");

        existCategory.Id = id;
        existCategory.IsDeleted = true;
        existCategory.Name = category.Name;
        existCategory.DeletedAt = DateTime.UtcNow;
        existCategory.Description = category.Description;

        await FileIO.WriteAsync(Constants.CATEGORIES_PATH, categories);

        return existCategory.ToMapView();
    }
}
