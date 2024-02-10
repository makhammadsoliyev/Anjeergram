using Anjeergram.Models.CommentLikes;
using Anjeergram.Models.Comments;
using Anjeergram.Models.Messages;
using Anjeergram.Models.PostLikes;
using Anjeergram.Models.Posts;
using Anjeergram.Models.Users;

namespace Anjeergram.Extensions;

public static class MapperExtension
{
    public static User ToMapMain(this UserCreationModel model)
    {
        return new User()
        {
            Email = model.Email,
            PictureUrl = model.PictureUrl,
            Password = model.Password,
            UserName = model.UserName,
            LastName = model.LastName,
            FirstName = model.FirstName,
        };
    }

    public static UserViewModel ToMapView(this User model)
    {
        return new UserViewModel()
        {
            Id = model.Id,
            Date = model.Date,
            Email = model.Email,
            PictureUrl = model.PictureUrl,
            UserName = model.UserName,
            LastName = model.LastName,
            FirstName = model.FirstName,
            Followers = model.Followers,
            Followings = model.Followings,
        };
    }


    public static Post ToMapMain(this PostCreationModel model)
    {
        return new Post()
        {
            Title = model.Title,
            UserId = model.UserId,
            Content = model.Content,
            PictureUrl = model.PictureUrl,
            Description = model.Description,
        };
    }

    public static PostViewModel ToMapView(this Post model, UserViewModel user)
    {
        return new PostViewModel()
        {
            User = user,
            Id = model.Id,
            Date = model.Date,
            Title = model.Title,
            Likes = model.Likes,
            Content = model.Content,
            EditedAt = model.EditedAt,
            PictureUrl = model.PictureUrl,
            Description = model.Description,
        };
    }


    public static PostLike ToMapMain(this PostLikeCreationModel model)
    {
        return new PostLike()
        {
            PostId = model.PostId,
            UserId = model.UserId,
        };
    }

    public static PostLikeViewModel ToMapView(this PostLike model, UserViewModel user, PostViewModel post)
    {
        return new PostLikeViewModel()
        {
            Post = post,
            User = user,
            Id = model.Id,
            Date = model.Date,
        };
    }


    public static Comment ToMapMain(this CommentCreationModel model)
    {
        return new Comment()
        {
            UserId = model.UserId,
            PostId = model.PostId,
            Content = model.Content,
        };
    }

    public static CommentViewModel ToMapView(this Comment model, UserViewModel user, PostViewModel post)
    {
        return new CommentViewModel()
        {
            User = user,
            Post = post,
            Id = model.Id,
            Date = model.Date,
            Likes = model.Likes,
            Content = model.Content,
            EditedAt = model.EditedAt,
        };
    }


    public static CommentLike ToMapMain(this CommentLikeCreationModel model)
    {
        return new CommentLike()
        {
            CommentId = model.CommentId,
            UserId = model.UserId,
        };
    }

    public static CommentLikeViewModel ToMapView(this CommentLike model, UserViewModel user, CommentViewModel comment)
    {
        return new CommentLikeViewModel()
        {
            User = user,
            Id = model.Id,
            Comment = comment,
            Date = model.Date,
        };
    }


    public static Message ToMapMain(this MessageCreationModel model)
    {
        return new Message()
        {
            Content = model.Content,
            SourceUserId = model.SourceUserId,
            TargetUserId = model.TargetUserId,
        };
    }

    public static MessageViewModel ToMapView(this Message model, UserViewModel sourceUser, UserViewModel targetUser)
    {
        return new MessageViewModel()
        {
            Id = model.Id,
            Date = model.Date,
            Content = model.Content,
            SourceUser = sourceUser,
            TargetUser = targetUser,
            EditedAt = model.EditedAt,
        };
    }
}
