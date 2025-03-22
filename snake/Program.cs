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

        const int InputDelay = 150;
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
            Point snakeHead = new Point(consoleWidth / 2, consoleHeight / 2, ConsoleColor.Red);
            Direction currentDirection = Direction.RIGHT;
            List<Point> bodyPositions = new List<Point>();
            Point berry = new Point(randomGenerator.Next(1, consoleWidth - 1), randomGenerator.Next(1, consoleHeight - 1), ConsoleColor.Cyan);
            DateTime lastInputTime = DateTime.Now;
            DateTime currentTime = DateTime.Now;
            bool inputPressed = false;

            // Main game loop
            while (true)
            {
                Console.Clear();

                // Check for collision with walls
                if (IsCollisionWithWalls(snakeHead, consoleWidth, consoleHeight))
                {
                    isGameOver = 1;
                }

                // Draw the game borders
                ConsoleRenderer.DrawBorders(consoleWidth, consoleHeight);

                // Check for berry collision
                if (IsBerryEaten(berry, snakeHead))
                {
                    snakeLength++;
                    berry = new Point(randomGenerator.Next(1, consoleWidth - 1), randomGenerator.Next(1, consoleHeight - 1), ConsoleColor.Cyan);
                }

                // Draw the snake's body and check for self-collision
                ConsoleRenderer.DrawSnakeBody(bodyPositions);

                // Check if the game is over
                if (isGameOver == 1)
                {
                    DisplayGameOver(snakeLength, consoleWidth, consoleHeight);
                    break;
                }

                // Draw the snake's head
                ConsoleRenderer.DrawSnakeHead(snakeHead.X, snakeHead.Y);

                // Draw the berry
                ConsoleRenderer.DrawBerry(berry.X, berry.Y);

                // Handle movement input
                lastInputTime = DateTime.Now;
                inputPressed = false;
                while (true)
                {
                    currentTime = DateTime.Now;
                    if (currentTime.Subtract(lastInputTime).TotalMilliseconds > InputDelay) { break; } // Reduced delay for faster movement
                    if (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo keyPress = Console.ReadKey(true);
                        HandleInput(keyPress, ref currentDirection, ref inputPressed);
                    }
                }

                // Update the snake's position
                UpdateSnakeBody(ref bodyPositions, snakeHead);
                UpdatePosition(ref snakeHead, currentDirection);

                // Maintain the length of the snake
                MaintainSnakeLength(ref bodyPositions, snakeLength);
            }
        }

        // Method to check for collision with the walls
        static bool IsCollisionWithWalls(Point snake, int consoleWidth, int consoleHeight)
        {
            return snake.X == consoleWidth - 1 || snake.X == 0 || snake.Y == consoleHeight - 1 || snake.Y == 0;
        }

        // Method to check if the berry is eaten
        static bool IsBerryEaten(Point berry, Point snake)
        {
            return berry.X == snake.X && berry.Y == snake.Y;
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
        static void UpdatePosition(ref Point snakeHead, Direction currentDirection)
        {
            switch (currentDirection)
            {
                case Direction.UP:
                    snakeHead.Y--;
                    break;
                case Direction.DOWN:
                    snakeHead.Y++;
                    break;
                case Direction.LEFT:
                    snakeHead.X--;
                    break;
                case Direction.RIGHT:
                    snakeHead.X++;
                    break;
            }
        }

        // Method to update the snake's body position
        static void UpdateSnakeBody(ref List<Point> bodyPositions, Point snakeHead)
        {
            bodyPositions.Add(snakeHead);
        }

        // Method to maintain the length of the snake by removing the last segment if necessary
        static void MaintainSnakeLength(ref List<Point> bodyPositions, int snakeLength)
        {
            if (bodyPositions.Count > snakeLength)
            {
                bodyPositions.RemoveAt(0);
            }
        }

        // Method to display the game over message
        static void DisplayGameOver(int snakeLength, int consoleWidth, int consoleHeight)
        {
            snakeLength = snakeLength - 5;
            Console.SetCursorPosition(consoleWidth / 5, consoleHeight / 2);
            Console.WriteLine("Game over, Score: " + snakeLength);
            Console.SetCursorPosition(consoleWidth / 5, consoleHeight / 2 + 1);
        }
    }
}
