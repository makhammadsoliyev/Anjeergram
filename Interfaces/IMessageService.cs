using Anjeergram.Models.Messages;

namespace Anjeergram.Interfaces;

public interface IMessageService
{
    Task<MessageViewModel> AddAsync(MessageCreationModel message);
    Task<MessageViewModel> GetByIdAsync(long id);
    Task<MessageViewModel> UpdateAsync(long id, MessageUpdateModel message);
    Task<bool> DeleteAsync(long id);
    Task<IEnumerable<MessageViewModel>> GetAllAsync();
    Task<IEnumerable<MessageViewModel>> GetAllAsync(long userId);
}