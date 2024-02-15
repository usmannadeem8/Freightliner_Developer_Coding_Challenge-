using System;

namespace RobotGame
{
    class Program
    {
        static int[,] board;
        static int robotX = -1, robotY = -1; // Robot's current position
        static string Direction = "";
        static bool FirstInstructionFlag = true; // Fisrt Instruction Validation Flag

        // I have used print statements frequently to understand the flow of the program. Also "PRINT" command is added for printing purpose.
        static void Main(string[] args)
        {
            InitializeBoard(3, 3); // Initialize the board with dimensions 3x3
            Console.WriteLine("Welcome to the Robot Game!");
            

            while (true)
            {
                Console.Write("Enter command (or 'EXIT' to end): ");
                string input = Console.ReadLine().ToUpper();

                if (input == "EXIT")
                {
                    Console.WriteLine("Exiting the game. Goodbye!");
                    break;
                }
                else if (FirstInstructionFlag == true)
                {
                    ValidateFirstCommand(input); // Validation of first command. Must be "PLACE" and accurate.
                }
                else
                {
                    ProcessCommand(input);
                }
                
            }
        }

        static void InitializeBoard(int rows, int columns)
        {
            board = new int[rows, columns];
        }

        static void ProcessCommand(string command)
        {
            string[] parts = command.Split(' ');

                switch (parts[0])
                {
                    case "PLACE":
                        if (parts.Length == 4)
                        {
                            int x, y;
                            if (int.TryParse(parts[1], out x) && int.TryParse(parts[2], out y))
                            {
                                if (IsValidPosition(x, y) && IsValidDirection(parts[3]))
                                {
                                   Place(x, y, parts[3]);
                                }
                                else
                                {
                                    Console.WriteLine("Invalid position. Please enter valid coordinates and direction.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid coordinates. Please enter valid integers for X and Y.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid command format. Use 'PLACE <X> <Y> <DIR>'");
                        }
                        break;

                    case "TURN":
                        if (parts.Length == 2)
                        {
                            Turn(parts[1]);
                        }
                        else
                        {
                            Console.WriteLine("Invalid command format. Use 'TURN <DIR>'");
                        }
                        break;

                    case "MOVE":
                        Move();
                        break;

                    case "PRINT":
                        Console.WriteLine($"Robot is at ({robotX}, {robotY}) facing {Direction}");
                        break;

                    default:
                        Console.WriteLine("Invalid instruction");
                        break;
                }
            

            
        }

        static void ValidateFirstCommand(string command)
        {
            string[] parts = command.Split(' ');

            if (parts.Length == 4 && parts[0] == "PLACE")
            {
                int x, y;
                if (int.TryParse(parts[1], out x) && int.TryParse(parts[2], out y))
                {
                    if (IsValidPosition(x, y) && IsValidDirection(parts[3]))
                    {
                        Place(x, y, parts[3]);
                        FirstInstructionFlag =false;
                        
                    }
                    else
                    {
                        Console.WriteLine("Invalid position. Please enter valid coordinates and direction.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid coordinates. Please enter valid integers for X and Y.");
                }
            }
            else
            {
                Console.WriteLine("Error: First instruction must be PLACE");
            }

            
        }

        static void Place(int x, int y, string direction)
        {
            robotX = x;
            robotY = y;
            Direction = direction;

            Console.WriteLine($"Robot placed at ({robotX}, {robotY}, {Direction})");
        }

        static void Turn(string direction)
        {
            if (IsValidDirection(direction))
            {
                Direction = direction;
                Console.WriteLine($"Robot turned to face {Direction}");
            }
            else
            {
                Console.WriteLine("Invalid direction. Use 'NORTH', 'SOUTH', 'EAST', or 'WEST'.");
            }
        }

        static void Move()
        {
            int newX = robotX, newY = robotY;

            switch (Direction)
            {
                case "NORTH":
                    newX++;
                    break;
                case "SOUTH":
                    newX--;
                    break;
                case "EAST":
                    newY++;
                    break;
                case "WEST":
                    newY--;
                    break;
            }

            if (IsValidPosition(newX, newY))
            {
                robotX = newX;
                robotY = newY;
                Console.WriteLine($"{robotX} {robotY} {Direction}");
            }
            else
            {
                Console.WriteLine("Stop! Going to fall!");
            }
        }

        static bool IsValidPosition(int x, int y)
        {
            return x >= 0 && x < board.GetLength(0) && y >= 0 && y < board.GetLength(1);
        }

        static bool IsValidDirection(string direction)
        {
            return direction == "NORTH" || direction == "SOUTH" || direction == "EAST" || direction == "WEST";
        }
    }
}
