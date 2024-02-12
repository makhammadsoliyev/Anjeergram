using Anjeergram.Interfaces;
using Anjeergram.Models.PostLikes;
using Anjeergram.Models.Posts;
using Anjeergram.Services;
using Spectre.Console;

namespace Anjeergram.Display;

public class PostLikeMenu
{
    private readonly IPostLikeService postLikeService;

    public PostLikeMenu(IPostLikeService postLikeService)
    {
        this.postLikeService = postLikeService;
    }

    private async Task Add()
    {
        long userId = AnsiConsole.Ask<long>("[yellow]UserId: [/]");
        while (userId <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            userId = AnsiConsole.Ask<long>("[yellow]UserId: [/]");
        }
        long postId = AnsiConsole.Ask<long>("[blue]PostId: [/]");
        while (postId <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            postId = AnsiConsole.Ask<long>("[blue]PostId: [/]");
        }

        var like = new PostLikeCreationModel()
        {
            UserId = userId,
            PostId = postId,
        };

        try
        {
            var addedLike = await postLikeService.AddAsync(like);
            AnsiConsole.MarkupLine("[green]Successfully added...[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
        }
        Thread.Sleep(1500);
    }

    private async Task GetById()
    {
        long id = AnsiConsole.Ask<long>("[aqua]Id: [/]");
        while (id <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            id = AnsiConsole.Ask<long>("[aqua]Id: [/]");
        }

        try
        {
            var like = await postLikeService.GetByIdAsync(id);
            var table = new SelectionMenu().DataTable("PostLike", like);
            AnsiConsole.Write(table);
            AnsiConsole.MarkupLine("[blue]Enter to continue...[/]");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
            Thread.Sleep(1500);
        }
    }

    private async Task Delete()
    {
        long id = AnsiConsole.Ask<long>("[aqua]Id: [/]");
        while (id <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            id = AnsiConsole.Ask<long>("[aqua]Id: [/]");
        }

        try
        {
            bool isDeleted = await postLikeService.DeleteAsync(id);
            AnsiConsole.MarkupLine("[green]Successfully deleted...[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
        }
        Thread.Sleep(1500);
    }

    private async Task GetAll()
    {
        try
        {
            var likes = await postLikeService.GetAllAsync();
            var table = new SelectionMenu().DataTable("PostLikes", likes.ToArray());
            AnsiConsole.Write(table);
            AnsiConsole.MarkupLine("[blue]Enter to continue...[/]");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
            Thread.Sleep(1500);
        }
    }

    private async Task GetAllByUserId()
    {
        long userId = AnsiConsole.Ask<long>("[yellow]UserId: [/]");
        while (userId <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            userId = AnsiConsole.Ask<long>("[yellow]UserId: [/]");
        }

        try
        {
            var likes = await postLikeService.GetAllByUserIdAsync(userId);
            var table = new SelectionMenu().DataTable("PostLikes", likes.ToArray());
            AnsiConsole.Write(table);
            AnsiConsole.MarkupLine("[blue]Enter to continue...[/]");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
            Thread.Sleep(1500);
        }
    }

    private async Task GetAllByPostId()
    {
        long postId = AnsiConsole.Ask<long>("[yellow]PostId: [/]");
        while (postId <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            postId = AnsiConsole.Ask<long>("[yellow]PostId: [/]");
        }

        try
        {
            var likes = await postLikeService.GetAllByPostIdAsync(postId);
            var table = new SelectionMenu().DataTable("PostLikes", likes.ToArray());
            AnsiConsole.Write(table);
            AnsiConsole.MarkupLine("[blue]Enter to continue...[/]");
            Console.ReadKey();
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
            Thread.Sleep(1500);
        }
    }

    public async Task Display()
    {
        var circle = true;
        var selectionDisplay = new SelectionMenu();

        while (circle)
        {
            AnsiConsole.Clear();
            var selection = selectionDisplay.ShowSelectionMenu("Choose one of options",
                new string[] { "Add", "GetById", "Delete", "GetAll", "GetAllByUserId", "GetAllByPostId", "Back" });

            switch (selection)
            {
                case "Add":
                    await Add();
                    break;
                case "GetById":
                    await GetById();
                    break;
                case "Delete":
                    await Delete();
                    break;
                case "GetAll":
                    await GetAll();
                    break;
                case "GetAllByUserId":
                    await GetAllByUserId();
                    break;
                case "GetAllByPostId":
                    await GetAllByPostId();
                    break;
                case "Back":
                    circle = false;
                    break;
            }
        }
    }
}
