using Anjeergram.Models.Categories;
using Anjeergram.Models.CommentLikes;
using Anjeergram.Models.Comments;
using Anjeergram.Models.Follows;
using Anjeergram.Models.Messages;
using Anjeergram.Models.PostCategories;
using Anjeergram.Models.PostLikes;
using Anjeergram.Models.Posts;
using Anjeergram.Models.PostTags;
using Anjeergram.Models.Tags;
using Anjeergram.Models.Users;

namespace Anjeergram.Extensions;

public static class MapperExtension
{
    public static User ToMapMain(this UserCreationModel model)
    {
        return new User()
        {
            Email = model.Email,
            UserName = model.UserName,
            LastName = model.LastName,
            Password = model.Password,
            FirstName = model.FirstName,
            PictureUrl = model.PictureUrl,
        };
    }

    public static UserUpdateModel ToMapUpdate(this UserCreationModel model)
    {
        return new UserUpdateModel()
        {
            Email = model.Email,
            Password = model.Password,
            LastName = model.LastName,
            FirstName = model.FirstName,
            PictureUrl = model.PictureUrl,
        };
    }

    public static UserViewModel ToMapView(this User model)
    {
        return new UserViewModel()
        {
            Id = model.Id,
            Date = model.Date,
            Email = model.Email,
            UserName = model.UserName,
            LastName = model.LastName,
            FirstName = model.FirstName,
            Followers = model.Followers,
            PictureUrl = model.PictureUrl,
            Followings = model.Followings,
        };
    }


    public static Post ToMapMain(this PostCreationModel model)
    {
        return new Post()
        {
            Date = model.Date,
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
            Date = model.Date,
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
            Date = model.Date,
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
            Date = model.Date,
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


    public static Tag ToMapMain(this TagCreationModel model)
    {
        return new Tag()
        {
            Name = model.Name,
        };
    }

    public static TagViewModel ToMapView(this Tag model)
    {
        return new TagViewModel()
        {
            Id = model.Id,
            Name = model.Name,
        };
    }


    public static PostTag ToMapMain(this PostTagCreationModel model)
    {
        return new PostTag()
        {
            PostId = model.PostId,
            TagId = model.TagId,
        };
    }

    public static PostTagViewModel ToMapView(this PostTag model, PostViewModel post, TagViewModel tag)
    {
        return new PostTagViewModel()
        {
            Tag = tag,
            Post = post,
            Id = post.Id,
        };
    }


    public static Category ToMapMain(this CategoryCreationModel model)
    {
        return new Category()
        {
            Name = model.Name,
            Description = model.Description,
        };
    }

    public static CategoryViewModel ToMapView(this Category model)
    {
        return new CategoryViewModel()
        {
            Id = model.Id,
            Name = model.Name,
            Description = model.Description,
        };
    }


    public static PostCategory ToMapMain(this PostCategoryCreationModel model)
    {
        return new PostCategory()
        {
            PostId = model.PostId,
            CategoryId = model.CategoryId,
        };
    }

    public static PostCategoryViewModel ToMapView(this PostCategory model, CategoryViewModel category, PostViewModel post)
    {
        return new PostCategoryViewModel()
        {
            Post = post,
            Id = model.Id,
            Category = category,
        };
    }

    public static Follow ToMapMain(this FollowCreationModel model)
    {
        return new Follow()
        {
            Date = model.Date,
            FollowedUserId = model.FollowedUserId,
            FollowingUserId = model.FollowingUserId,
        };
    }

    public static FollowViewModel ToMapView(this Follow model, UserViewModel follower, UserViewModel following)
    {
        return new FollowViewModel()
        {
            Id = model.Id,
            Date = model.Date,
            FollowedUser = follower,
            FollowingUser = following,
        };
    }
}
