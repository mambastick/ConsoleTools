namespace ConsoleTools.Menu;

public class Menu
{
    private int SelectedIndex;
    private List<Option> Options;
    private string Prompt;
    private MenuPosition Position;

    public Menu(string promt, IEnumerable<Option> options, MenuPosition position = MenuPosition.Center)
    {
        Prompt = promt;
        Options = options.ToList();
        SelectedIndex = 0;
        Position = position;
    }

    private (int x, int y) GetCoordinates(MenuPosition position)
    {
        var windowHeight = Console.WindowHeight;
        var windowWidth = Console.WindowWidth;
        var maxOptionNameLength = Options.Max(option => option.Name.Length);

        var verticalPosition = 0;
        var horizontalPosition = 0;

        switch (position)
        {
            case MenuPosition.TopLeft:
                verticalPosition = 0;
                horizontalPosition = 0;
                break;
            case MenuPosition.TopCenter:
                verticalPosition = 0;
                horizontalPosition = (windowWidth - maxOptionNameLength) / 2;
                break;
            case MenuPosition.TopRight:
                verticalPosition = 0;
                horizontalPosition = windowWidth - maxOptionNameLength;
                break;
            case MenuPosition.CenterLeft:
                verticalPosition = (windowHeight - Options.Count) / 2;
                horizontalPosition = 0;
                break;
            case MenuPosition.Center:
                verticalPosition = (windowHeight - Options.Count) / 2;
                horizontalPosition = (windowWidth - maxOptionNameLength) / 2;
                break;
            case MenuPosition.CenterRight:
                verticalPosition = (windowHeight - Options.Count) / 2;
                horizontalPosition = windowWidth - maxOptionNameLength;
                break;
            case MenuPosition.BottomLeft:
                verticalPosition = windowHeight - Options.Count;
                horizontalPosition = 0;
                break;
            case MenuPosition.BottomCenter:
                verticalPosition = windowHeight - Options.Count;
                horizontalPosition = (windowWidth - maxOptionNameLength) / 2;
                break;
            case MenuPosition.BottomRight:
                verticalPosition = windowHeight - Options.Count;
                horizontalPosition = windowWidth - maxOptionNameLength;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(position), position, null);
        }

        return (horizontalPosition, verticalPosition);
    }

    private void DisplayOptions(MenuPosition position)
    {
        Console.Clear();
        var maxOptionNameLength = Options.Max(option => option.Name.Length);

        var coordinates = GetCoordinates(position);
        var horizontalPosition = coordinates.x;
        var verticalPosition = coordinates.y;

        Console.SetCursorPosition(horizontalPosition, verticalPosition);

        for (var i = 0; i < Options.Count; i++)
        {
            var option = Options[i];
            var isSelected = (i == SelectedIndex);

            Console.BackgroundColor = isSelected ? ConsoleColor.White : Console.BackgroundColor;
            Console.ForegroundColor = isSelected ? ConsoleColor.Black : Console.ForegroundColor;

            Console.Write(isSelected ? "* " : "  ");
            Console.Write(option.Name.PadRight(maxOptionNameLength));

            if (isSelected && !string.IsNullOrEmpty(option.Description))
            {
                Console.Write(" -> " + option.Description);
            }

            Console.ResetColor();
            Console.WriteLine();
        }
    }

    private void MoveUp()
    {
        if (Options.Count == 0) return;

        SelectedIndex--;
        if (SelectedIndex < 0)
            SelectedIndex = Options.Count - 1;
    }

    private void MoveDown()
    {
        if (Options.Count == 0) return;

        SelectedIndex++;
        if (SelectedIndex >= Options.Count)
            SelectedIndex = 0;
    }

    private Option GetCurrentOption() => Options.Count == 0 ? null : Options[SelectedIndex];

    public string Show()
    {
        ConsoleKey key;
        do
        {
            DisplayOptions(Position);
            key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                    MoveUp();
                    break;
                case ConsoleKey.DownArrow:
                    MoveDown();
                    break;
                case ConsoleKey.Enter:
                    var selectedOption = GetCurrentOption();
                    if (selectedOption != null)
                        return selectedOption.Name;
                    break;
            }
        } while (key != ConsoleKey.Escape);

        return "Escape";
    }

    public void Close() => Console.Clear();
}

public enum MenuPosition
{
    TopLeft,
    TopCenter,
    TopRight,
    CenterLeft,
    Center,
    CenterRight,
    BottomLeft,
    BottomCenter,
    BottomRight
}

public class Option
{
    public readonly string Name;
    public readonly string? Description;

    public Option(string name, string? description = null)
    {
        Name = name;
        Description = description;
    }
}