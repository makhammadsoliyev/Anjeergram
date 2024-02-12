using Spectre.Console;

namespace Anjeergram.Display;

public class MainMenu
{
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
        tagMenu = new TagMenu();
        userMenu = new UserMenu();
        postMenu = new PostMenu();
        followMenu = new FollowMenu();
        postTagMenu = new PostTagMenu();
        commentMenu = new CommentMenu();
        messageMenu = new MessageMenu();
        categoryMenu = new CategoryMenu();
        postLikeMenu = new PostLikeMenu();
        commentLikeMenu = new CommentLikeMenu();
        postCategoryMenu = new PostCategoryMenu();
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