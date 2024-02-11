using Anjeergram.Configurations;
using Anjeergram.Extensions;
using Anjeergram.Helpers;
using Anjeergram.Interfaces;
using Anjeergram.Models.Tags;

namespace Anjeergram.Services;

public class TagService : ITagService
{
    private List<Tag> tags;

    public async Task<TagViewModel> AddAsync(TagCreationModel tag)
    {
        tags = await FileIO.ReadAsync<Tag>(Constants.TAGS_PATH);
        var createdTag = tag.ToMapMain();
        createdTag.Id = tags.GenerateId();

        tags.Add(createdTag);

        await FileIO.WriteAsync(Constants.TAGS_PATH, tags);

        return createdTag.ToMapView();
    }

    public async Task<bool> DeleteAsync(long id)
    {
        tags = await FileIO.ReadAsync<Tag>(Constants.TAGS_PATH);
        var tag = tags.FirstOrDefault(t => t.Id == id && !t.IsDeleted)
            ?? throw new Exception($"Tag was not found with this id: {id}");

        tag.IsDeleted = true;
        tag.DeletedAt = DateTime.UtcNow;

        await FileIO.WriteAsync(Constants.TAGS_PATH, tags);

        return true;
    }

    public async Task<IEnumerable<TagViewModel>> GetAllAsync()
    {
        tags = await FileIO.ReadAsync<Tag>(Constants.TAGS_PATH);
        return tags.Where(t => !t.IsDeleted).Select(t => t.ToMapView());
    }

    public async Task<TagViewModel> GetByIdAsync(long id)
    {
        tags = await FileIO.ReadAsync<Tag>(Constants.TAGS_PATH);
        var tag = tags.FirstOrDefault(t => !t.IsDeleted && t.Id == id)
            ?? throw new Exception($"Tag was not found with this id: {id}");

        return tag.ToMapView();
    }

    public async Task<TagViewModel> UpdateAsync(long id, TagUpdateModel tag)
    {
        tags = await FileIO.ReadAsync<Tag>(Constants.TAGS_PATH);
        var existTag = tags.FirstOrDefault(t => !t.IsDeleted && t.Id == id)
            ?? throw new Exception($"Tag was not found with this id: {id}");

        existTag.Id = id;
        existTag.Name = tag.Name;
        existTag.UpdatedAt = DateTime.UtcNow;

        await FileIO.WriteAsync(Constants.TAGS_PATH, tags);

        return existTag.ToMapView();
    }
}
