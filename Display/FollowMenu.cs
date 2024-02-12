using Anjeergram.Interfaces;
using Anjeergram.Models.Follows;
using Anjeergram.Models.Messages;
using Anjeergram.Services;
using Spectre.Console;

namespace Anjeergram.Display;

public class FollowMenu
{
    private readonly IFollowService followService;

    public FollowMenu(IFollowService followService)
    {
        this.followService = followService;
    }

    private async Task Add()
    {
        long followingUserId = AnsiConsole.Ask<long>("[yellow]FollowingUserId: [/]");
        while (followingUserId <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            followingUserId = AnsiConsole.Ask<long>("[yellow]FollowingUserId: [/]");
        }
        long followedUserId = AnsiConsole.Ask<long>("[blue]FollowedUserId: [/]");
        while (followedUserId <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            followedUserId = AnsiConsole.Ask<long>("[blue]FollowedUserId: [/]");
        }

        var follow = new FollowCreationModel()
        {
            FollowedUserId = followedUserId,
            FollowingUserId = followingUserId,
        };

        try
        {
            var addedFollow = await followService.AddAsync(follow);
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
            var follow = await followService.GetByIdAsync(id);
            var table = new SelectionMenu().DataTable("Follow", follow);
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
            bool isDeleted = await followService.DeleteAsync(id);
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
            var follows = await followService.GetAllAsync();
            var table = new SelectionMenu().DataTable("Follows", follows.ToArray());
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

    private async Task GetAllFollowingsByUserId()
    {
        long userId = AnsiConsole.Ask<long>("[yellow]UserId: [/]");
        while (userId <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            userId = AnsiConsole.Ask<long>("[yellow]UserId: [/]");
        }

        try
        {
            var follows = await followService.GetAllFollowingsByUserIdAsync(userId);
            var table = new SelectionMenu().DataTable("Follows", follows.ToArray());
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

    private async Task GetAllFollowersByUserIdAsync()
    {
        long userId = AnsiConsole.Ask<long>("[yellow]UserId: [/]");
        while (userId <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            userId = AnsiConsole.Ask<long>("[yellow]UserId: [/]");
        }

        try
        {
            var follows = await followService.GetAllFollowersByUserIdAsync(userId);
            var table = new SelectionMenu().DataTable("Follows", follows.ToArray());
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
                new string[] { "Add", "GetById", "Delete", "GetAll", "GetAllFollowingsByUserId", "GetAllFollowersByUserIdAsync", "Back" });

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
                case "GetAllFollowingsByUserId":
                    await GetAllFollowingsByUserId();
                    break;
                case "GetAllFollowersByUserIdAsync":
                    await GetAllFollowersByUserIdAsync();
                    break;
                case "Back":
                    circle = false;
                    break;
            }
        }
    }
}