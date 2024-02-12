using Anjeergram.Interfaces;
using Anjeergram.Models.CommentLikes;
using Spectre.Console;

namespace Anjeergram.Display;

public class CommentLikeMenu
{
    private readonly ICommentLikeService commentLikeService;

    public CommentLikeMenu(ICommentLikeService commentLikeService)
    {
        this.commentLikeService = commentLikeService;
    }

    private async Task Add()
    {
        long userId = AnsiConsole.Ask<long>("[yellow]UserId: [/]");
        while (userId <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            userId = AnsiConsole.Ask<long>("[yellow]UserId: [/]");
        }
        long commentId = AnsiConsole.Ask<long>("[blue]CommentId: [/]");
        while (commentId <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            commentId = AnsiConsole.Ask<long>("[blue]CommentId: [/]");
        }

        var like = new CommentLikeCreationModel()
        {
            UserId = userId,
            CommentId = commentId,
        };

        try
        {
            var addedLike = await commentLikeService.AddAsync(like);
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
            var like = await commentLikeService.GetByIdAsync(id);
            var table = new SelectionMenu().DataTable("CommentLike", like);
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
            bool isDeleted = await commentLikeService.DeleteAsync(id);
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
            var likes = await commentLikeService.GetAllAsync();
            var table = new SelectionMenu().DataTable("CommentLikes", likes.ToArray());
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
            var likes = await commentLikeService.GetAllByUserIdAsync(userId);
            var table = new SelectionMenu().DataTable("CommentLikes", likes.ToArray());
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

    private async Task GetAllByCommentId()
    {
        long commentId = AnsiConsole.Ask<long>("[yellow]CommentId: [/]");
        while (commentId <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            commentId = AnsiConsole.Ask<long>("[yellow]CommentId: [/]");
        }

        try
        {
            var likes = await commentLikeService.GetAllByCommentIdAsync(commentId);
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
                new string[] { "Add", "GetById", "Delete", "GetAll", "GetAllByUserId", "GetAllByCommentId", "Back" });

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
                case "GetAllByCommentId":
                    await GetAllByCommentId();
                    break;
                case "Back":
                    circle = false;
                    break;
            }
        }
    }
}
