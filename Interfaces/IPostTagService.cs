using Anjeergram.Models.PostTags;

namespace Anjeergram.Interfaces;

public interface IPostTagService
{
    Task<PostTagViewModel> AddAsync(PostTagCreationModel postTag);
    Task<PostTagViewModel> GetById(long id);
    Task<bool> DeleteAsync(long id);
    Task<IEnumerable<PostTagViewModel>> GetAllAsync();
    Task<IEnumerable<PostTagViewModel>> GetAllByTagIdAsync(long tagId);
    Task<IEnumerable<PostTagViewModel>> GetAllByPostIdAsync(long postId);
}
