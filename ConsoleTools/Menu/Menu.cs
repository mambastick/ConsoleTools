namespace ConsoleTools.Menu;

public class Menu
{
    private int SelectedIndex;
    private List<Option> Options;
    private string Prompt;

    public Menu(string promt, IEnumerable<Option> options)
    {
        Prompt = promt;
        Options = options.ToList();
        SelectedIndex = 0;
    }

    private void DisplayOptions()
    {
        Console.Clear();
        Console.WriteLine(Prompt);

        var maxOptionNameLength = Options.Max(option => option.Name.Length);

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

    public string Run()
    {
        ConsoleKey key;
        do
        {
            DisplayOptions();
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

public class Option
{
    public readonly string Name;
    public readonly string? Description;

    internal Option(string name, string? description = null)
    {
        Name = name;
        Description = description;
    }
}