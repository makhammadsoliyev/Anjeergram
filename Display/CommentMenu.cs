using Anjeergram.Interfaces;
using Anjeergram.Models.Categories;
using Anjeergram.Models.Comments;
using Anjeergram.Services;
using Spectre.Console;

namespace Anjeergram.Display;

public class CommentMenu
{
    private readonly ICommentService commentService;

    public CommentMenu(ICommentService commentService)
    {
        this.commentService = commentService;
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
        string content = AnsiConsole.Ask<string>("[blue]Content: [/]");

        var comment = new CommentCreationModel()
        {
            UserId = userId,
            PostId = postId,
            Content = content,
        };

        try
        {
            var addedComment = await commentService.AddAsync(comment);
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
            var comment = await commentService.GetByIdAsync(id);
            var table = new SelectionMenu().DataTable("Comment", comment);
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

    private async Task Update()
    {
        long id = AnsiConsole.Ask<long>("[aqua]Id: [/]");
        while (id <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            id = AnsiConsole.Ask<long>("[aqua]Id: [/]");
        }
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
        string content = AnsiConsole.Ask<string>("[blue]Content: [/]");

        var comment = new CommentUpdateModel()
        {
            UserId = userId,
            PostId = postId,
            Content = content,
        };

        try
        {
            var updatedComment = await commentService.UpdateAsync(id, comment);
            AnsiConsole.MarkupLine("[green]Successfully updated...[/]");
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
        }
        Thread.Sleep(1500);
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
            bool isDeleted = await commentService.DeleteAsync(id);
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
            var comments = await commentService.GetAllAsync();
            var table = new SelectionMenu().DataTable("Comments", comments.ToArray());
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
            var likes = await commentService.GetAllByUserIdAsync(userId);
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
            var comments = await commentService.GetAllByPostIdAsync(postId);
            var table = new SelectionMenu().DataTable("Comments", comments.ToArray());
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
                new string[] { "Add", "GetById", "Delete", "Update", "GetAll", "GetAllByUserId", "GetAllByPostId", "Back" });

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
                case "Update":
                    await Update();
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
