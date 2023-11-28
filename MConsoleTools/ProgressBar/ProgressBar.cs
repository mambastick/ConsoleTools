using static System.Console;

namespace MConsoleTools.ProgressBar;

public class ProgressBar
{
    private int Total;
    private int Current;
    private int BarLength;
    private char BarSymbol;
    private ConsoleColor BarColor;
    private IEnumerable<char> BorderChar;
    private ConsoleColor BorderColor;
    private string Information;

    public ProgressBar(int total = 100, 
        int barLength = 40,
        char barSymbol = '■',
        ConsoleColor barColor = ConsoleColor.Green,
        IEnumerable<char>? borderChar = null,
        ConsoleColor borderColor = ConsoleColor.Gray)
    {
        Total = total;
        Current = 0;
        BarLength = barLength;
        BarSymbol = barSymbol;
        BarColor = barColor;
        BorderChar = borderChar ?? new List<char> { '[', ']' };
        BorderColor = borderColor;
        Information = string.Empty;
    }
    
    public void Next(int value)
    {
        if (value < 0 || value > Total)
            throw new ArgumentOutOfRangeException(nameof(value), "Value must be between 0 and total.");

        Current = value;
    }

    public void Draw()
    {
        SetCursorPosition(0, CursorTop);
        var progress = (double)Current / Total;
        var progressBarLength = (int)(progress * BarLength);

        SetColors(BorderColor);
        Write(BorderChar.ToList()[0]);
        for (var i = 0; i < BarLength; i++)
        {
            if (i < progressBarLength)
            {
                SetColors(BarColor);
                Write(BarSymbol);
            }
            else
            {
                SetColors(BorderColor);
                Write(" ");
            }
        }

        SetColors(BorderColor);
        Write($"{BorderChar.ToList()[1]} {Current}/{Total} {Information}");
        ResetColor();
    }

    private static void SetColors(ConsoleColor color) => ForegroundColor = color;
    
    public void SetInformation(string info) => Information = info;
}