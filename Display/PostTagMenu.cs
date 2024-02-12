using Anjeergram.Interfaces;
using Anjeergram.Models.PostTags;
using Spectre.Console;

namespace Anjeergram.Display;

public class PostTagMenu
{
    private readonly IPostTagService postTagService;

    public PostTagMenu(IPostTagService postTagService)
    {
        this.postTagService = postTagService;
    }

    private async Task Add()
    {
        long postId = AnsiConsole.Ask<long>("[blue]PostId: [/]");
        while (postId <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            postId = AnsiConsole.Ask<long>("[blue]PostId: [/]");
        }
        long tagId = AnsiConsole.Ask<long>("[yellow]CategoryId: [/]");
        while (tagId <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            tagId = AnsiConsole.Ask<long>("[yellow]CategoryId: [/]");
        }

        var postTag = new PostTagCreationModel()
        {
            PostId = postId,
            TagId = tagId,
        };

        try
        {
            var addedPostTag = await postTagService.AddAsync(postTag);
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
            var postTag = await postTagService.GetByIdAsync(id);
            var table = new SelectionMenu().DataTable("PostTag", postTag);
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
            bool isDeleted = await postTagService.DeleteAsync(id);
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
            var postTags = await postTagService.GetAllAsync();
            var table = new SelectionMenu().DataTable("PostTags", postTags.ToArray());
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
            var postTags = await postTagService.GetAllByPostIdAsync(postId);
            var table = new SelectionMenu().DataTable("PostTags", postTags.ToArray());
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

    private async Task GetAllByTagId()
    {
        long tagId = AnsiConsole.Ask<long>("[yellow]TagId: [/]");
        while (tagId <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            tagId = AnsiConsole.Ask<long>("[yellow]TagId: [/]");
        }

        try
        {
            var postTags = await postTagService.GetAllByTagIdAsync(tagId);
            var table = new SelectionMenu().DataTable("PostTags", postTags.ToArray());
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
                new string[] { "Add", "GetById", "Delete", "GetAll", "GetAllByTagId", "GetAllByPostId", "Back" });

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
                case "GetAllByTagId":
                    await GetAllByTagId();
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
