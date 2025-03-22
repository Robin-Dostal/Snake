using System;
using System.Collections.Generic;

public class ConsoleRenderer
{
    // Method to draw the game borders
    public static void DrawBorders(int consoleWidth, int consoleHeight)
    {
        for (int i = 0; i < consoleWidth; i++)
        {
            Console.SetCursorPosition(i, 0);
            Console.ForegroundColor = ConsoleColor.White;  // Border color
            Console.Write("■");
            Console.SetCursorPosition(i, consoleHeight - 1);
            Console.Write("■");
        }
        for (int i = 0; i < consoleHeight; i++)
        {
            Console.SetCursorPosition(0, i);
            Console.Write("■");
            Console.SetCursorPosition(consoleWidth - 1, i);
            Console.Write("■");
        }
        Console.ResetColor();
    }

    // Method to draw the snake's head
    public static void DrawSnakeHead(int x, int y)
    {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = ConsoleColor.Red;  // Snake head color in red
        Console.Write("■");
        Console.ResetColor();
    }

    // Method to draw the snake's body
    public static void DrawSnakeBody(List<Point> bodyPositions)
    {
        foreach (var body in bodyPositions)
        {
            Console.SetCursorPosition(body.X, body.Y);
            Console.ForegroundColor = ConsoleColor.Green;  // Snake body color in green
            Console.Write("■");
            Console.ResetColor();
        }
    }

    // Method to draw the berry
    public static void DrawBerry(int x, int y)
    {
        Console.SetCursorPosition(x, y);
        Console.ForegroundColor = ConsoleColor.Cyan;  // Berry color in cyan
        Console.Write("■");
        Console.ResetColor();
    }

    // Method to reset console color after everything is drawn
    public static void ResetColor()
    {
        Console.ResetColor();
    }
}