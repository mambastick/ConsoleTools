using static System.Console;

namespace ConsoleTools.ProgressBar;

public class ProgressBar
{
    private int Total;
    private int Current;
    private int BarLength;
    private ConsoleColor BarColor;
    private ConsoleColor BorderColor;
    private string Information;

    public ProgressBar(int total, int barLength = 40, ConsoleColor barColor = ConsoleColor.Green,
        ConsoleColor borderColor = ConsoleColor.Gray)
    {
        Total = total;
        Current = 0;
        BarLength = barLength;
        BarColor = barColor;
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
        var progress = (double)Current / Total;
        var progressBarLength = (int)(progress * BarLength);

        Write("[");
        for (var i = 0; i < BarLength; i++)
        {
            if (i < progressBarLength)
            {
                SetColors(BarColor);
                Write("■");
            }
            else
            {
                SetColors(BorderColor);
                Write(" ");
            }
        }

        SetColors(BorderColor);
        Write($"] {Current}/{Total} {Information}");
        ResetColor();
    }

    private static void SetColors(ConsoleColor color) => ForegroundColor = color;
    
    public void SetInformation(string info) => Information = info;
}