using Anjeergram.Models.Categories;
using Anjeergram.Models.PostLikes;
using Anjeergram.Models.Posts;
using Anjeergram.Models.Users;
using Spectre.Console;

namespace Anjeergram.Display;

public class SelectionMenu
{
    public Table DataTable(string title, params CategoryViewModel[] categories)
    {
        var table = new Table();

        table.Title(title.ToUpper())
            .BorderColor(Color.Blue)
            .AsciiBorder();

        table.AddColumn("ID");
        table.AddColumn("Name");
        table.AddColumn("Description");

        foreach (var user in categories)
            table.AddRow(user.Id.ToString(), user.Name, user.Description);

        table.Border = TableBorder.Rounded;
        table.Centered();

        return table;
    }

    public Table DataTable(string title, params PostLikeViewModel[] likes)
    {
        var table = new Table();

        table.Title(title.ToUpper())
            .BorderColor(Color.Blue)
            .AsciiBorder();

        table.AddColumn("ID");
        table.AddColumn("User");
        table.AddColumn("Post");
        table.AddColumn("Date");

        foreach (var t in likes)
            table.AddRow(t.Id.ToString(), $"{t.User.FirstName} {t.User.LastName}", t.Post.Title, t.Date.ToString());

        table.Border = TableBorder.Rounded;
        table.Centered();

        return table;
    }

    public Table DataTable(string title, params PostViewModel[] posts)
    {
        var table = new Table();

        table.Title(title.ToUpper())
            .BorderColor(Color.Blue)
            .AsciiBorder();

        table.AddColumn("ID");
        table.AddColumn("User");
        table.AddColumn("Title");
        table.AddColumn("Description");
        table.AddColumn("Content");
        table.AddColumn("PictureUrl");
        table.AddColumn("Likes");
        table.AddColumn("EditedAt");
        table.AddColumn("Date");

        foreach (var user in posts)
            table.AddRow(user.Id.ToString(), user.User.UserName, user.Title, user.Description, user.Content, user.PictureUrl, user.Likes.ToString(), user.EditedAt.ToString(), user.Date.ToString());

        table.Border = TableBorder.Rounded;
        table.Centered();

        return table;
    }

    public Table DataTable(string title, params UserViewModel[] users)
    {
        var table = new Table();

        table.Title(title.ToUpper())
            .BorderColor(Color.Blue)
            .AsciiBorder();

        table.AddColumn("ID");
        table.AddColumn("FirstName");
        table.AddColumn("LastName");
        table.AddColumn("UserName");
        table.AddColumn("Email");
        table.AddColumn("PictureUrl");
        table.AddColumn("Followers");
        table.AddColumn("Followings");
        table.AddColumn("Date");

        foreach (var user in users)
            table.AddRow(user.Id.ToString(), user.FirstName, user.LastName, user.UserName, user.Email, user.PictureUrl, user.Followers.ToString(), user.Followings.ToString(), user.Date.ToString());

        table.Border = TableBorder.Rounded;
        table.Centered();

        return table;
    }

    public string ShowSelectionMenu(string title, string[] options)
    {
        var selection = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title(title)
                .PageSize(5) // Number of items visible at once
                .AddChoices(options)
                .HighlightStyle(new Style(foreground: Color.Cyan1, background: Color.Blue))
        );

        return selection;
    }
}
