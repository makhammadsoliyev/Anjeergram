using Anjeergram.Interfaces;
using Anjeergram.Services;
using Spectre.Console;

namespace Anjeergram.Display;

public class MainMenu
{
    private readonly ITagService tagService;
    private readonly IUserService userService;
    private readonly IPostService postService;
    private readonly IFollowService followService;
    private readonly IPostTagService postTagService;
    private readonly ICommentService commentService;
    private readonly IMessageService messageService;
    private readonly ICategoryService categoryService;
    private readonly IPostLikeService postLikeService;
    private readonly ICommentLikeService commentLikeService;
    private readonly IPostCategoryService postCategoryService;

    private readonly TagMenu tagMenu;
    private readonly UserMenu userMenu;
    private readonly PostMenu postMenu;
    private readonly FollowMenu followMenu;
    private readonly PostTagMenu postTagMenu;
    private readonly CommentMenu commentMenu;
    private readonly MessageMenu messageMenu;
    private readonly CategoryMenu categoryMenu;
    private readonly PostLikeMenu postLikeMenu;
    private readonly CommentLikeMenu commentLikeMenu;
    private readonly PostCategoryMenu postCategoryMenu;

    public MainMenu()
    {
        tagService = new TagService();
        userService = new UserService();
        categoryService = new CategoryService();
        postService = new PostService(userService);
        followService = new FollowService(userService);
        messageService = new MessageService(userService);
        postTagService = new PostTagService(tagService, postService);
        commentService = new CommentService(postService, userService);
        postLikeService = new PostLikeService(userService, postService);
        commentLikeService = new CommentLikeService(commentService, userService);
        postCategoryService = new PostCategoryService(categoryService, postService);

        tagMenu = new TagMenu(tagService);
        userMenu = new UserMenu(userService);
        postMenu = new PostMenu(postService);
        followMenu = new FollowMenu();
        postTagMenu = new PostTagMenu();
        commentMenu = new CommentMenu();
        messageMenu = new MessageMenu();
        categoryMenu = new CategoryMenu(categoryService);
        postLikeMenu = new PostLikeMenu(postLikeService);
        commentLikeMenu = new CommentLikeMenu();
        postCategoryMenu = new PostCategoryMenu(postCategoryService);
    }

    public async Task Main()
    {
        var circle = true;
        var selectionDisplay = new SelectionMenu();

        while (circle)
        {
            AnsiConsole.Clear();
            var selection = selectionDisplay.ShowSelectionMenu(
                "Choose one of options", 
                new string[] { "User", "Post", "PostLike", "Category", "PostCategory", "Tag", "PostTag", "Comment", "CommentLike", "Message", "Follow", "Exit" });

            switch (selection)
            {
                case "User":
                    await userMenu.Display();
                    break;
                case "Post":
                    await postMenu.Display();
                    break;
                case "PostLike":
                    await postLikeMenu.Display();
                    break;
                case "Category":
                    await categoryMenu.Display();
                    break;
                case "PostCategory":
                    await postCategoryMenu.Display();
                    break;
                case "Tag":
                    await tagMenu.Display();
                    break;
                case "PostTag":
                    await postTagMenu.Display();
                    break;
                case "Comment":
                    await commentMenu.Display();
                    break;
                case "CommentLike":
                    await commentLikeMenu.Display();
                    break;
                case "Message":
                    await messageMenu.Display();
                    break;
                case "Follow":
                    await followMenu.Display();
                    break;
                case "Exit":
                    circle = false;
                    break;
            }
        }
    }
}