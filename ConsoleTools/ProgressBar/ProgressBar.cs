using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace ConsoleTools.ProgressBar;

public class ProgressBar
{
    public int Total;
    private int Current;
    private int BarLength;
    private char BarSymbol;
    private ConsoleColor BarColor;
    private IEnumerable<char> BorderChar;
    private ConsoleColor BorderColor;
    private string Information;
    private string Title;
    private ProgressBarPosition TitlePosition;
    private ProgressBarPosition BarPosition;

    public ProgressBar(string title, ProgressBarPosition titlePosition, ProgressBarPosition barPosition,
        int total = 100,
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
        Title = title;
        TitlePosition = titlePosition;
        BarPosition = barPosition;
    }

    public void Next(int value)
    {
        if (value < 0 || value > Total)
            throw new ArgumentOutOfRangeException(nameof(value), "Value must be between 0 and total.");

        Current = value;
    }

    private (int x, int y) GetTitlePosition(int width, int height)
    {
        int x = 0;
        int y = 0;

        switch (TitlePosition)
        {
            case ProgressBarPosition.Above:
                y = 0;
                break;
            case ProgressBarPosition.Below:
                y = height - 1;
                break;
            case ProgressBarPosition.Center:
                y = height / 2;
                break;
        }

        return (x, y);
    }

    private (int x, int y) GetBarPosition(int width, int height)
    {
        var x = 0;
        var y = 0;

        switch (BarPosition)
        {
            case ProgressBarPosition.Above:
                y = 1;
                break;
            case ProgressBarPosition.Below:
                y = height - 1;
                break;
            case ProgressBarPosition.Center:
                y = height / 2;
                break;
            case ProgressBarPosition.Left:
                x = 0;
                break;
            case ProgressBarPosition.Right:
                x = width - BarLength;
                break;
            case ProgressBarPosition.TopLeft:
                x = 0;
                y = 1;
                break;
            case ProgressBarPosition.TopRight:
                x = width - BarLength;
                y = 1;
                break;
            case ProgressBarPosition.BottomLeft:
                x = 0;
                y = height - 1;
                break;
            case ProgressBarPosition.BottomRight:
                x = width - BarLength;
                y = height - 1;
                break;
        }

        return (x, y);
    }

    private void DrawTitle(int width, int height)
    {
        var (x, y) = GetTitlePosition(width, height);
        SetCursorPosition(x, y);
        SetColors(BorderColor);
        Write(Title);
    }

    private void DrawProgressBar(int width, int height)
    {
        var (x, y) = GetBarPosition(width, height);
        SetCursorPosition(x, y);
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

    public void Draw()
    {
        Clear();

        var width = WindowWidth;
        var height = WindowHeight;

        DrawTitle(width, height);
        DrawProgressBar(width, height);
    }

    private static void SetColors(ConsoleColor color) => ForegroundColor = color;

    public void SetInformation(string info) => Information = info;
}

public enum ProgressBarPosition
{
    Above,
    Below,
    Center,
    Left,
    Right,
    TopLeft,
    TopRight,
    BottomLeft,
    BottomRight
}