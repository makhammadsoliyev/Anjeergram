using Anjeergram.Interfaces;
using Anjeergram.Models.Comments;
using Anjeergram.Models.Messages;
using Anjeergram.Services;
using Spectre.Console;

namespace Anjeergram.Display;

public class MessageMenu
{
    private readonly IMessageService messageService;

    public MessageMenu(IMessageService messageService)
    {
        this.messageService = messageService;
    }

    private async Task Add()
    {
        long sourceUserId = AnsiConsole.Ask<long>("[yellow]SourceUserId: [/]");
        while (sourceUserId <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            sourceUserId = AnsiConsole.Ask<long>("[yellow]SourceUserId: [/]");
        }
        long targetUserId = AnsiConsole.Ask<long>("[blue]TargetUserId: [/]");
        while (targetUserId <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            targetUserId = AnsiConsole.Ask<long>("[blue]TargetUserId: [/]");
        }
        string content = AnsiConsole.Ask<string>("[blue]Content: [/]");

        var message = new MessageCreationModel()
        {
            SourceUserId = sourceUserId,
            TargetUserId = targetUserId,
            Content = content,
        };

        try
        {
            var addedMessage = await messageService.AddAsync(message);
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
            var message = await messageService.GetByIdAsync(id);
            var table = new SelectionMenu().DataTable("Message", message);
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
        long sourceUserId = AnsiConsole.Ask<long>("[yellow]SourceUserId: [/]");
        while (sourceUserId <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            sourceUserId = AnsiConsole.Ask<long>("[yellow]SourceUserId: [/]");
        }
        long targetUserId = AnsiConsole.Ask<long>("[blue]TargetUserId: [/]");
        while (targetUserId <= 0)
        {
            AnsiConsole.MarkupLine($"[red]Invalid input.[/]");
            targetUserId = AnsiConsole.Ask<long>("[blue]TargetUserId: [/]");
        }
        string content = AnsiConsole.Ask<string>("[blue]Content: [/]");

        var message = new MessageUpdateModel()
        {
            SourceUserId = sourceUserId,
            TargetUserId = targetUserId,
            Content = content,
        };

        try
        {
            var updatedMessage = await messageService.UpdateAsync(id, message);
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
            bool isDeleted = await messageService.DeleteAsync(id);
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
            var messages = await messageService.GetAllAsync();
            var table = new SelectionMenu().DataTable("Messages", messages.ToArray());
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
            var messages = await messageService.GetAllAsync(userId);
            var table = new SelectionMenu().DataTable("Messages", messages.ToArray());
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
                new string[] { "Add", "GetById", "Delete", "Update", "GetAll", "GetAllByUserId", "Back" });

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
                case "Back":
                    circle = false;
                    break;
            }
        }
    }
}
