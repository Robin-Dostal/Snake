using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Snake
{
    class Program
    {
        enum Direction { UP, DOWN, LEFT, RIGHT } 
        static void Main(string[] args)
        {
            // Set up the console window size
            Console.WindowHeight = 16;
            Console.WindowWidth = 32;
            int consoleWidth = Console.WindowWidth;
            int consoleHeight = Console.WindowHeight;

            // Initialize game variables
            Random randomGenerator = new Random();
            int snakeLength = 5;
            int isGameOver = 0;
            SnakeHead snakeHead = new SnakeHead
            {
                XPosition = consoleWidth / 2,
                YPosition = consoleHeight / 2,
                Color = ConsoleColor.Red
            };
            Direction currentDirection = Direction.RIGHT;
            List<int> bodyXPositions = new List<int>();
            List<int> bodyYPositions = new List<int>();
            int berryXPosition = randomGenerator.Next(1, consoleWidth - 1);
            int berryYPosition = randomGenerator.Next(1, consoleHeight - 1);
            DateTime lastInputTime = DateTime.Now;
            DateTime currentTime = DateTime.Now;
            bool inputPressed = false;


            // Main game loop
            while (true)
            {
                Console.Clear();

                // Check for collision with walls
                if (snakeHead.XPosition == consoleWidth - 1 || snakeHead.XPosition == 0 || snakeHead.YPosition == consoleHeight - 1 || snakeHead.YPosition == 0)
                {
                    isGameOver = 1;
                }

                // Draw the game borders
                DrawBorders(consoleWidth, consoleHeight);

                // Check for berry collision
                if (berryXPosition == snakeHead.XPosition && berryYPosition == snakeHead.YPosition)
                {
                    snakeLength++;
                    berryXPosition = randomGenerator.Next(1, consoleWidth - 1);
                    berryYPosition = randomGenerator.Next(1, consoleHeight - 1);
                }

                // Draw the snake's body and check for self-collision
                for (int i = 0; i < bodyXPositions.Count; i++)
                {
                    Console.SetCursorPosition(bodyXPositions[i], bodyYPositions[i]);
                    Console.Write("■");
                    if (bodyXPositions[i] == snakeHead.XPosition && bodyYPositions[i] == snakeHead.YPosition)
                    {
                        isGameOver = 1;
                    }
                }

                // Check if the game is over
                if (isGameOver == 1)
                {
                    DisplayGameOver(snakeLength, consoleWidth, consoleHeight);
                    break;
                }

                // Draw the snake's head
                Console.SetCursorPosition(snakeHead.XPosition, snakeHead.YPosition);
                Console.ForegroundColor = snakeHead.Color;
                Console.Write("■");

                // Draw the berry
                Console.SetCursorPosition(berryXPosition, berryYPosition);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("■");

                // Handle movement input
                lastInputTime = DateTime.Now;
                inputPressed = false;
                while (true)
                {
                    currentTime = DateTime.Now;
                    if (currentTime.Subtract(lastInputTime).TotalMilliseconds > 150) { break; } // Reduced delay for faster movement
                    if (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo keyPress = Console.ReadKey(true);
                        HandleInput(keyPress, ref currentDirection, ref inputPressed);
                    }
                }

                // Update the snake's position
                bodyXPositions.Add(snakeHead.XPosition);
                bodyYPositions.Add(snakeHead.YPosition);
                UpdatePosition(ref snakeHead, currentDirection);

                // Maintain the length of the snake
                if (bodyXPositions.Count > snakeLength)
                {
                    bodyXPositions.RemoveAt(0);
                    bodyYPositions.RemoveAt(0);
                }
            }
        }

        // Method to draw the game borders
        static void DrawBorders(int consoleWidth, int consoleHeight)
        {
            for (int i = 0; i < consoleWidth; i++)
            {
                Console.SetCursorPosition(i, 0);
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
        }

        // Method to handle user input for movement
        static void HandleInput(ConsoleKeyInfo keyPress, ref Direction currentDirection, ref bool inputPressed)
        {
            if (inputPressed) return;
            if (keyPress.Key == ConsoleKey.UpArrow && currentDirection != Direction.DOWN)
            {
                currentDirection = Direction.UP;
            }
            else if (keyPress.Key == ConsoleKey.DownArrow && currentDirection != Direction.UP)
            {
                currentDirection = Direction.DOWN;
            }
            else if (keyPress.Key == ConsoleKey.LeftArrow && currentDirection != Direction.RIGHT)
            {
                currentDirection = Direction.LEFT;
            }
            else if (keyPress.Key == ConsoleKey.RightArrow && currentDirection != Direction.LEFT)
            {
                currentDirection = Direction.RIGHT;
            }
            inputPressed = true;
        }

        // Method to update the snake's position based on movement
        static void UpdatePosition(ref SnakeHead snakeHead, Direction currentDirection)
        {
            switch (currentDirection)
            {
                case Direction.UP:
                    snakeHead.YPosition--;
                    break;
                case Direction.DOWN:
                    snakeHead.YPosition++;
                    break;
                case Direction.LEFT:
                    snakeHead.XPosition--;
                    break;
                case Direction.RIGHT:
                    snakeHead.XPosition++;
                    break;
            }
        }
        
        // Method to display the game over message
        static void DisplayGameOver(int snakeLength, int consoleWidth, int consoleHeight)
        {
            Console.SetCursorPosition(consoleWidth / 5, consoleHeight / 2);
            Console.WriteLine("Game over, Score: " + snakeLength);
            Console.SetCursorPosition(consoleWidth / 5, consoleHeight / 2 + 1);
        }

        // Class to represent the snake's head
        class SnakeHead
        {
            public int XPosition { get; set; }
            public int YPosition { get; set; }
            public ConsoleColor Color { get; set; }
        }
    }
}