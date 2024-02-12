using Anjeergram.Interfaces;
using Anjeergram.Models.Posts;
using Spectre.Console;

namespace Anjeergram.Display;

public class PostMenu
{
    private readonly IPostService postService;

    public PostMenu(IPostService postService)
    {
        this.postService = postService;
    }

    private async Task Add()
    {
        long userId = AnsiConsole.Ask<long>("[yellow]UserId: [/]");
        while (userId <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            userId = AnsiConsole.Ask<long>("[yellow]UserId: [/]");
        }
        string title = AnsiConsole.Ask<string>("[blue]Title: [/]");
        string description = AnsiConsole.Ask<string>("[cyan2]Description: [/]");
        string content = AnsiConsole.Ask<string>("[blue]Content: [/]");
        string pictureUrl = AnsiConsole.Ask<string>("[cyan1]PictureUrl: [/]");

        var post = new PostCreationModel()
        {
            Title = title,
            UserId = userId,
            Content = content,
            PictureUrl = pictureUrl,
            Description = description,
        };

        try
        {
            var addedPost = await postService.AddAsync(post);
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
            var post = await postService.GetByIdAsync(id);
            var table = new SelectionMenu().DataTable("Post", post);
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
        string title = AnsiConsole.Ask<string>("[blue]Title: [/]");
        string description = AnsiConsole.Ask<string>("[cyan2]Description: [/]");
        string content = AnsiConsole.Ask<string>("[blue]Content: [/]");
        string pictureUrl = AnsiConsole.Ask<string>("[cyan1]PictureUrl: [/]");

        var post = new PostUpdateModel()
        {
            Title = title,
            UserId = userId,
            Content = content,
            PictureUrl = pictureUrl,
            Description = description,
        };

        try
        {
            var updatedPost = await postService.UpdateAsync(id, post);
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
            bool isDeleted = await postService.DeleteAsync(id);
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
            var posts = await postService.GetAllAsync();
            var table = new SelectionMenu().DataTable("Posts", posts.ToArray());
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
            var posts = await postService.GetAllAsync(userId);
            var table = new SelectionMenu().DataTable("Posts", posts.ToArray());
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
                new string[] { "Add", "GetById", "Update", "Delete", "GetAll", "GetAllByUserId", "Back" });

            switch (selection)
            {
                case "Add":
                    await Add();
                    break;
                case "GetById":
                    await GetById();
                    break;
                case "Update":
                    await Update();
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
                case "Back":
                    circle = false;
                    break;
            }
        }
    }
}
