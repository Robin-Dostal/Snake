using System;

public struct Point
{
    public int X;
    public int Y;
    public ConsoleColor Color;  // Add color property

    public Point(int x, int y, ConsoleColor color)
    {
        X = x;
        Y = y;
        Color = color;
    }
}