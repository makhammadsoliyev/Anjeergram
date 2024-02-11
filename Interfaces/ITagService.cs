using Anjeergram.Models.Tags;

namespace Anjeergram.Interfaces;

public interface ITagService
{
    Task<TagViewModel> AddAsync(TagCreationModel tag);
    Task<TagViewModel> GetByIdAsync(long id);
    Task<TagViewModel> UpdateAsync(long id, TagUpdateModel tag);
    Task<bool> DeleteAsync(long id);
    Task<IEnumerable<TagViewModel>> GetAllAsync();
}
