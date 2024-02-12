using Anjeergram.Configurations;
using Anjeergram.Extensions;
using Anjeergram.Helpers;
using Anjeergram.Interfaces;
using Anjeergram.Models.Messages;

namespace Anjeergram.Services;

public class MessageService : IMessageService
{
    private List<Message> messages;
    private readonly IUserService userService;

    public MessageService(IUserService userService)
    {
        this.userService = userService;
    }

    public async Task<MessageViewModel> AddAsync(MessageCreationModel message)
    {
        var sourceUser = await userService.GetByIdAsync(message.SourceUserId);
        var targetUser = await userService.GetByIdAsync(message.TargetUserId);
        if (sourceUser.Id == targetUser.Id)
            throw new Exception("Sender and receiver cannot be one person");

        messages = await FileIO.ReadAsync<Message>(Constants.MESSAGES_PATH);

        var createdMessage = message.ToMapMain();
        createdMessage.Id = messages.GenerateId();

        messages.Add(createdMessage);

        await FileIO.WriteAsync(Constants.MESSAGES_PATH, messages);

        return createdMessage.ToMapView(sourceUser, targetUser);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        messages = await FileIO.ReadAsync<Message>(Constants.MESSAGES_PATH);
        var message = messages.FirstOrDefault(m => m.Id == id && !m.IsDeleted)
            ?? throw new Exception($"Message was not found with this id: {id}");

        message.IsDeleted = true;
        message.DeletedAt = DateTime.UtcNow;

        await FileIO.WriteAsync(Constants.MESSAGES_PATH, messages);

        return true;
    }

    public async Task<IEnumerable<MessageViewModel>> GetAllAsync()
    {
        messages = await FileIO.ReadAsync<Message>(Constants.MESSAGES_PATH);
        messages = messages.FindAll(m => !m.IsDeleted);
        var result = new List<MessageViewModel>();

        foreach (var message in messages)
        {
            var sourceUser = await userService.GetByIdAsync(message.SourceUserId);
            var targetUser = await userService.GetByIdAsync(message.TargetUserId);
            result.Add(message.ToMapView(sourceUser, targetUser));
        }

        return result;
    }

    public async Task<IEnumerable<MessageViewModel>> GetAllAsync(long userId)
    {
        var sourceUser = await userService.GetByIdAsync(userId);
        messages = await FileIO.ReadAsync<Message>(Constants.MESSAGES_PATH);
        messages = messages.FindAll(m => !m.IsDeleted && m.SourceUserId == userId);
        var result = new List<MessageViewModel>();

        foreach (var message in messages)
        {
            var targetUser = await userService.GetByIdAsync(message.TargetUserId);
            result.Add(message.ToMapView(sourceUser, targetUser));
        }

        return result;
    }

    public async Task<MessageViewModel> GetByIdAsync(long id)
    {
        messages = await FileIO.ReadAsync<Message>(Constants.MESSAGES_PATH);
        var message = messages.FirstOrDefault(m => m.Id == id && !m.IsDeleted)
            ?? throw new Exception($"Message was not found with this id: {id}");
        var sourceUser = await userService.GetByIdAsync(message.SourceUserId);
        var targetUser = await userService.GetByIdAsync(message.TargetUserId);

        return message.ToMapView(sourceUser, targetUser);
    }

    public async Task<MessageViewModel> UpdateAsync(long id, MessageUpdateModel message)
    {
        var sourceUser = await userService.GetByIdAsync(message.SourceUserId);
        var targetUser = await userService.GetByIdAsync(message.TargetUserId);
        messages = await FileIO.ReadAsync<Message>(Constants.MESSAGES_PATH);
        var existMessage = messages.FirstOrDefault(m => m.Id == id && !m.IsDeleted)
            ?? throw new Exception($"Message was not found with this id: {id}");

        existMessage.Id = id;
        existMessage.Content = message.Content;
        existMessage.EditedAt = message.EditedAt;
        existMessage.UpdatedAt = DateTime.UtcNow;
        existMessage.TargetUserId = message.TargetUserId;
        existMessage.SourceUserId = message.SourceUserId;

        await FileIO.WriteAsync(Constants.MESSAGES_PATH, messages);

        return existMessage.ToMapView(sourceUser, targetUser);
    }
}