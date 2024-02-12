using Anjeergram.Interfaces;
using Anjeergram.Models.Users;
using Spectre.Console;
using System.Text.RegularExpressions;

namespace Anjeergram.Display;

public class UserMenu
{
    private readonly IUserService userService;

    public UserMenu(IUserService userService)
    {
        this.userService = userService;
    }

    private async Task Add()
    {
        string firstName = AnsiConsole.Ask<string>("[blue]FirstName: [/]");
        string lastName = AnsiConsole.Ask<string>("[cyan2]LastName: [/]");
        string userName = AnsiConsole.Ask<string>("[blue]UserName: [/]");
        while (!Regex.IsMatch(userName, @"^[a-zA-Z0-9_]{3,20}$"))
        {
            AnsiConsole.MarkupLine("[red]Invalid input.[/]");
            userName = AnsiConsole.Ask<string>("[cyan1]UserName: [/]");
        }
        string email = AnsiConsole.Ask<string>("[cyan1]Email: [/]");
        while (!Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
        {
            AnsiConsole.MarkupLine("[red]Invalid input.[/]");
            email = AnsiConsole.Ask<string>("[cyan1]Email: [/]");
        }
        string password = AnsiConsole.Prompt<string>(new TextPrompt<string>("Enter your password:").Secret());
        while (!Regex.IsMatch(password, @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$"))
        {
            AnsiConsole.MarkupLine("[red]Invalid input.[/]");
            password = AnsiConsole.Prompt<string>(new TextPrompt<string>("Enter your password:").Secret());
        }
        string pictureUrl = AnsiConsole.Ask<string>("[cyan3]PictureUrl: [/]");

        var user = new UserCreationModel()
        {
            Email = email,
            Password = password,
            UserName = userName,
            LastName = lastName,
            FirstName = firstName,
            PictureUrl = pictureUrl,
        };

        try
        {
            var addedUser = await userService.AddAsync(user);
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
            var user = await userService.GetByIdAsync(id);
            var table = new SelectionMenu().DataTable("User", user);
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
        string firstName = AnsiConsole.Ask<string>("[blue]FirstName: [/]");
        string lastName = AnsiConsole.Ask<string>("[cyan2]LastName: [/]");
        string email = AnsiConsole.Ask<string>("[cyan1]Email: [/]");
        while (!Regex.IsMatch(email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$"))
        {
            AnsiConsole.MarkupLine("[red]Invalid input.[/]");
            email = AnsiConsole.Ask<string>("[cyan1]Email: [/]");
        }
        string password = AnsiConsole.Prompt<string>(new TextPrompt<string>("Enter your password:").Secret());
        while (!Regex.IsMatch(password, @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$"))
        {
            AnsiConsole.MarkupLine("[red]Invalid input.[/]");
            password = AnsiConsole.Prompt<string>(new TextPrompt<string>("Enter your password:").Secret());
        }
        string pictureUrl = AnsiConsole.Ask<string>("[cyan3]PictureUrl: [/]");

        var user = new UserUpdateModel()
        {
            Email = email,
            Password = password,
            LastName = lastName,
            FirstName = firstName,
            PictureUrl = pictureUrl,
        };

        try
        {
            var updatedUser = await userService.UpdateAsync(id, user);
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
            bool isDeleted = await userService.DeleteAsync(id);
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
        var users = await userService.GetAllAsync();
        var table = new SelectionMenu().DataTable("Users", users.ToArray());
        AnsiConsole.Write(table);
        AnsiConsole.MarkupLine("[blue]Enter to continue...[/]");
        Console.ReadKey();
    }

    public async Task Display()
    {
        var circle = true;
        var selectionDisplay = new SelectionMenu();

        while (circle)
        {
            AnsiConsole.Clear();
            var selection = selectionDisplay.ShowSelectionMenu("Choose one of options",
                new string[] { "Add", "GetById", "Update", "Delete", "GetAll", "Back" });

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
                case "Back":
                    circle = false;
                    break;
            }
        }
    }
}
