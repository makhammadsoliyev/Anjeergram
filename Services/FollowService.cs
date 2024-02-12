using Anjeergram.Configurations;
using Anjeergram.Extensions;
using Anjeergram.Helpers;
using Anjeergram.Interfaces;
using Anjeergram.Models.Follows;

namespace Anjeergram.Services;

public class FollowService : IFollowService
{
    private List<Follow> follows;
    private readonly IUserService userService;

    public FollowService(IUserService userService)
    {
        this.userService = userService;
    }

    public async Task<FollowViewModel> AddAsync(FollowCreationModel follow)
    {
        var follower = await userService.GetByIdAsync(follow.FollowedUserId);
        var following = await userService.GetByIdAsync(follow.FollowingUserId);
        if (follower.Id == following.Id)
            throw new Exception("follower and following cannot be one person");
        follows = await FileIO.ReadAsync<Follow>(Constants.FOLLOWS_PATH);

        var createdFollow = follow.ToMapMain();
        createdFollow.Id = follows.GenerateId();

        follows.Add(createdFollow);

        await userService.IncrementFollowAsync(createdFollow.FollowingUserId, false);
        await userService.IncrementFollowAsync(createdFollow.FollowedUserId, true);
        await FileIO.WriteAsync(Constants.FOLLOWS_PATH, follows);

        return createdFollow.ToMapView(follower, following);
    }

    public async Task<bool> DeleteAsync(long id)
    {
        follows = await FileIO.ReadAsync<Follow>(Constants.FOLLOWS_PATH);
        var follow = follows.FirstOrDefault(f => f.Id == id && !f.IsDeleted)
            ?? throw new Exception($"Follow was not found with this id: {id}");

        follow.IsDeleted = true;
        follow.DeletedAt = DateTime.UtcNow;

        await userService.DecrementFollowAsync(follow.FollowingUserId, false);
        await userService.DecrementFollowAsync(follow.FollowedUserId, true);
        await FileIO.WriteAsync(Constants.FOLLOWS_PATH, follows);

        return true;
    }

    public async Task<IEnumerable<FollowViewModel>> GetAllAsync()
    {
        follows = await FileIO.ReadAsync<Follow>(Constants.FOLLOWS_PATH);
        follows = follows.FindAll(f => !f.IsDeleted);

        var result = new List<FollowViewModel>();
        foreach (var follow in follows)
        {
            var follower = await userService.GetByIdAsync(follow.FollowedUserId);
            var following = await userService.GetByIdAsync(follow.FollowingUserId);
            result.Add(follow.ToMapView(follower, following));
        }

        return result;
    }

    public async Task<IEnumerable<FollowViewModel>> GetAllFollowersByUserIdAsync(long userId)
    {
        var follower = await userService.GetByIdAsync(userId);
        follows = await FileIO.ReadAsync<Follow>(Constants.FOLLOWS_PATH);
        follows = follows.FindAll(f => !f.IsDeleted && f.FollowedUserId == userId);

        var result = new List<FollowViewModel>();
        foreach (var follow in follows)
        {
            var following = await userService.GetByIdAsync(follow.FollowingUserId);
            result.Add(follow.ToMapView(follower, following));
        }

        return result;
    }

    public async Task<IEnumerable<FollowViewModel>> GetAllFollowingsByUserIdAsync(long userId)
    {
        var following = await userService.GetByIdAsync(userId);
        follows = await FileIO.ReadAsync<Follow>(Constants.FOLLOWS_PATH);
        follows = follows.FindAll(f => !f.IsDeleted && f.FollowingUserId == userId);

        var result = new List<FollowViewModel>();
        foreach (var follow in follows)
        {
            var follower = await userService.GetByIdAsync(follow.FollowedUserId);
            result.Add(follow.ToMapView(follower, following));
        }

        return result;
    }

    public async Task<FollowViewModel> GetByIdAsync(long id)
    {
        follows = await FileIO.ReadAsync<Follow>(Constants.FOLLOWS_PATH);
        var follow = follows.FirstOrDefault(f => f.Id == id && !f.IsDeleted)
            ?? throw new Exception($"Follow was not found with this id: {id}");
        var follower = await userService.GetByIdAsync(follow.FollowedUserId);
        var following = await userService.GetByIdAsync(follow.FollowingUserId);

        return follow.ToMapView(follower, following);
    }
}
