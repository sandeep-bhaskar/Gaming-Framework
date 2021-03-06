using System;
using GamingFramework.ConnectFour.Logic;
namespace GamingFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("-------------------Welcome to Gaming Framework by Sandeep.-------------------\n");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Game Options (Press any key at any time)  \n'N'  to start a new game \n'S' to save the game \n'R' to resume the game \n'H' for help\n'E' to Exit \n");

            var game = new GameFramework();
            Console.ForegroundColor = ConsoleColor.White;
            game.DisplayUserMenu();

            Console.WriteLine("\nGoodbye and thanks for playing!");

        }
    }

    class GameFramework
    {
        
        Game game;
        public void DisplayUserMenu()
        {
            while (true)
            {
                string userSelection = null;

                while (string.IsNullOrEmpty(userSelection))
                {
                    Console.WriteLine("---------------Games Available--------------\n1. Connect 4 \n");
                    Console.WriteLine("Please select a game from above options.");

                    userSelection = Console.ReadLine();

                    switch (userSelection)
                    {
                        case "S":
                        case "s":
                        case "R":
                        case "r":
                            if (game == null)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkRed;
                                Console.WriteLine("Game is not yet started \n");
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            break;
                        case "E":
                        case "e":
                            Environment.Exit(-1);
                            return;
                        case "H":
                        case "h":
                            Helpers.OpenBrowser(Helpers.HelpGamingURL);
                            break;
                        case "1":
                            var isDimensionsValid = false;
                            int rows = 0, columns = 0;
                            var isComputerPlaying = false;
                            while (!isDimensionsValid)
                            {
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                Console.WriteLine("Please enter the board dimensions  (number of rows space number of columns).");
                                Console.ForegroundColor = ConsoleColor.White;
                                var userInputDimensions = Console.ReadLine();
                                var inputArray = userInputDimensions.Split(' ');

                                if (inputArray.Length >= 2 && int.TryParse(inputArray[0], out rows) && int.TryParse(inputArray[1], out columns))
                                {
                                    isDimensionsValid = true;
                                    var isModeValid = false;
                                    while (!isModeValid)
                                    {
                                        Console.WriteLine("Please select the mode of game.\n 1.Computer vs Human\n 2.Human vs Human");
                                        var userModeInput = Console.ReadLine();
                                        if (userModeInput == "1" || userModeInput == "2")
                                        {
                                            isModeValid = true;
                                            isComputerPlaying = userModeInput == "1";
                                        }
                                        else
                                        {
                                            Console.ForegroundColor = ConsoleColor.DarkRed;
                                            Console.WriteLine("Please enter valid mode..\n");
                                            Console.ForegroundColor = ConsoleColor.White;
                                        }
                                    }
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.DarkRed;
                                    Console.WriteLine("Please enter valid dimensions..\n");
                                    Console.ForegroundColor = ConsoleColor.White;
                                }
                            }
                            game = Game.CreateConsoleGame(DifficultyLevel.Hard, ActivePlayer.Yellow, false, isComputerPlaying, rows, columns);
                            game.Play();
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.DarkRed;
                            Console.WriteLine("Invalid input..\n");
                            Console.ForegroundColor = ConsoleColor.White;
                            break;

                    }
                }
            }
        }
    }
}