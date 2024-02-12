using Anjeergram.Interfaces;
using Anjeergram.Models.PostCategories;
using Spectre.Console;

namespace Anjeergram.Display;

public class PostCategoryMenu
{
    private readonly IPostCategoryService postCategoryService;

    public PostCategoryMenu(IPostCategoryService postCategoryService)
    {
        this.postCategoryService = postCategoryService;
    }

    private async Task Add()
    {
        long postId = AnsiConsole.Ask<long>("[blue]PostId: [/]");
        while (postId <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            postId = AnsiConsole.Ask<long>("[blue]PostId: [/]");
        }
        long categoryId = AnsiConsole.Ask<long>("[yellow]CategoryId: [/]");
        while (categoryId <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            categoryId = AnsiConsole.Ask<long>("[yellow]CategoryId: [/]");
        }

        var postCategory = new PostCategoryCreationModel()
        {
            PostId = postId,
            CategoryId = categoryId,
        };

        try
        {
            var addedPostCategory = await postCategoryService.AddAsync(postCategory);
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
            var postCategory = await postCategoryService.GetByIdAsync(id);
            var table = new SelectionMenu().DataTable("PostCategory", postCategory);
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
            bool isDeleted = await postCategoryService.DeleteAsync(id);
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
            var postCategories = await postCategoryService.GetAllAsync();
            var table = new SelectionMenu().DataTable("PostCategories", postCategories.ToArray());
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

    private async Task GetAllByCategoryId()
    {
        long categoryId = AnsiConsole.Ask<long>("[yellow]CategoryId: [/]");
        while (categoryId <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            categoryId = AnsiConsole.Ask<long>("[yellow]CategoryId: [/]");
        }

        try
        {
            var postCategories = await postCategoryService.GetAllByCategoryIdAsync(categoryId);
            var table = new SelectionMenu().DataTable("PostCategories", postCategories.ToArray());
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
            var postCategories = await postCategoryService.GetAllByPostIdAsync(postId);
            var table = new SelectionMenu().DataTable("PostCategories", postCategories.ToArray());
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
                new string[] { "Add", "GetById", "Delete", "GetAll", "GetAllByCategoryId", "GetAllByPostId", "Back" });

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
                case "GetAllByCategoryId":
                    await GetAllByCategoryId();
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
