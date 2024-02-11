using Anjeergram.Interfaces;
using Anjeergram.Models.Tags;

namespace Anjeergram.Services;

public class TagService : ITagService
{
    public Task<TagViewModel> AddAsync(TagCreationModel tag)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TagViewModel>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<TagViewModel> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<TagViewModel> UpdateAsync(long id, TagUpdateModel tag)
    {
        throw new NotImplementedException();
    }
}
