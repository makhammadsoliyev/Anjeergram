using Anjeergram.Configurations;
using Anjeergram.Extensions;
using Anjeergram.Helpers;
using Anjeergram.Interfaces;
using Anjeergram.Models.Users;

namespace Anjeergram.Services;

public class UserService : IUserService
{
    private List<User> users; 

    public async Task<UserViewModel> AddAsync(UserCreationModel user)
    {
        users = await FileIO.ReadAsync<User>(Constants.USERS_PATH);
        var existUser = users.FirstOrDefault(u => u.Email.Equals(user.Email));
        if (existUser is not null && existUser.IsDeleted)
            return await UpdateAsync(existUser.Id, user.ToMapUpdate(), true);

        if (existUser is not null && users.IsUserNameExists(user.UserName))
            throw new Exception($"User already exists with this userName: {user.UserName} or email: {user.Email}");

        var createdUser = user.ToMapMain();
        createdUser.Id = users.GenerateId();

        users.Add(createdUser);

        await FileIO.WriteAsync(Constants.USERS_PATH, users);

        return createdUser.ToMapView();
    }

    public async Task<bool> DeleteAsync(long id)
    {
        users = await FileIO.ReadAsync<User>(Constants.USERS_PATH);
        var user = users.FirstOrDefault(u => u.Id == id && !u.IsDeleted)
            ?? throw new Exception($"User was not found with this id: {id}");

        user.IsDeleted = true;
        user.DeletedAt = DateTime.UtcNow;

        await FileIO.WriteAsync(Constants.USERS_PATH, users);

        return true;
    }

    public async Task<IEnumerable<UserViewModel>> GetAllAsync()
    {
        users = await FileIO.ReadAsync<User>(Constants.USERS_PATH);
        return users.Where(u => !u.IsDeleted).Select(u => u.ToMapView());
    }

    public async Task<UserViewModel> GetByIdAsync(long id)
    {
        users = await FileIO.ReadAsync<User>(Constants.USERS_PATH);
        var user = users.FirstOrDefault(u => u.Id == id && !u.IsDeleted)
            ?? throw new Exception($"User was not found with this id: {id}");

        return user.ToMapView();
    }

    public async Task<UserViewModel> UpdateAsync(long id, UserUpdateModel user, bool isUsedDeleted = false)
    {
        users = await FileIO.ReadAsync<User>(Constants.USERS_PATH);
        var existUser = new User();

        if (isUsedDeleted)
            existUser = users.FirstOrDefault(u => u.Id == id);
        else
            existUser = users.FirstOrDefault(u => u.Id == id && !u.IsDeleted)
                ?? throw new Exception($"User was not found with this id: {id}");

        existUser.Id = id;
        existUser.IsDeleted = false;
        existUser.Email = user.Email;
        existUser.Password = user.Password;
        existUser.LastName = user.LastName;
        existUser.FirstName = user.FirstName;
        existUser.UpdatedAt = DateTime.UtcNow;
        existUser.PictureUrl = user.PictureUrl;

        await FileIO.WriteAsync(Constants.USERS_PATH, users);

        return existUser.ToMapView();
    }
}
