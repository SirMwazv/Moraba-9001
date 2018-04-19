using System;

using Morabaraba9001.Data;
using Morabaraba9001.Display;

//Static using directives so names don't have to be fully qualified 
using static Morabaraba9001.Data.Position;
using static Morabaraba9001.Display.Board;

namespace Morabaraba9001
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new GameManager(new GameState());    //initialize a new game instance
            string cowArt = @"            
                               /             \
                              ((__-^^-,-^^-__))    Moooo!
                               `-_---' `---_-'    /
                                <__|o` 'o|__>    /
                                   \  `  /      /
                                    ): :(
                                    :o_o:
                                     ";
            string title = "MORABARABA \n\n\n \t* Let the cow genocide begin! >:) * ";

            Console.WriteLine(String.Format("{0} \n{1}", title, cowArt));

            Console.WriteLine("Press 1 for Quickplay \nPress 2 to Setup Game \nPress 3 for HighScores \nPress any other button to Exit.");
            switch (Console.ReadKey().KeyChar)
            {
                case '1':
                    game.RunGame();
                    break;
                case '2':
                    #region Setup Player 1
                    Console.WriteLine("\nPlayer 1 please enter your name.");
                    var p1Name = Console.ReadLine();
                    ConsoleColor p1Col = new ConsoleColor();
                    bool validCol = false;
                    char p1ColChoice = 'X';
                    while (!validCol)
                    {
                        Console.Clear();
                        Console.WriteLine("\n" + p1Name + ", Choose your color:\n 1:Red \n 2:Blue\n 3:Green \n 4:Yellow");
                        p1ColChoice = Console.ReadKey().KeyChar;
                        switch (p1ColChoice)
                        {
                            case '1':
                                p1Col = ConsoleColor.Red;
                                validCol = true;
                                break;
                            case '2':
                                p1Col = ConsoleColor.Blue;
                                validCol = true;
                                break;
                            case '3':
                                p1Col = ConsoleColor.Green;
                                validCol = true;
                                break;
                            case '4':
                                p1Col = ConsoleColor.Yellow;
                                validCol = true;
                                break;
                            default:
                                PrintErr("\nInvalid Selection! Please pick a valid number.");
                                Console.ReadLine();
                                break;
                        }
                    }
                    Player p1 = new Player(p1Name, p1Col);
                    #endregion

                    #region Setup Player 2
                    Console.WriteLine("\nPlayer 2 please enter your name.");
                    var p2Name = Console.ReadLine();
                    ConsoleColor p2Col = new ConsoleColor();
                    validCol = false;
                    char p2ColChoice = 'X';
                    while (!validCol)
                    {
                        Console.Clear();
                        Console.WriteLine("\n" + p2Name + ", Choose your color:\n 1:Red \n 2:Blue\n 3:Green \n 4:Yellow");
                        p2ColChoice = Console.ReadKey().KeyChar;
                        if (p1ColChoice == p2ColChoice)
                        {
                            PrintErr("\nInvalid Selection! NOTE: You can't have the same colour as your opponent!");
                            Console.ReadLine();
                            continue;
                        }

                        switch (p2ColChoice)
                        {
                            case '1':
                                p2Col = ConsoleColor.Red;
                                validCol = true;
                                break;
                            case '2':
                                p2Col = ConsoleColor.Blue;
                                validCol = true;
                                break;
                            case '3':
                                p2Col = ConsoleColor.Green;
                                validCol = true;
                                break;
                            case '4':
                                p2Col = ConsoleColor.Yellow;
                                validCol = true;
                                break;
                            default:
                                PrintErr("\nInvalid Selection! Please pick a valid number.");
                                Console.ReadLine();
                                break;
                        }
                    }
                    Player p2 = new Player(p2Name, p2Col);
                    #endregion

                    #region Setup Console Color
                    ConsoleColor bg = new ConsoleColor();
                    Console.WriteLine("\nPlease Choose a Deafult Color for the console (background)\n 1:Gray \n 2:White \n 3:Cyan \n 4:DarkCyan");
                    switch (Console.ReadKey().KeyChar)
                    {
                        case '1':
                            bg = ConsoleColor.Gray;
                            validCol = true;
                            break;
                        case '2':
                            bg = ConsoleColor.White;
                            validCol = true;
                            break;
                        case '3':
                            bg = ConsoleColor.Cyan;
                            validCol = true;
                            break;
                        case '4':
                            bg = ConsoleColor.DarkCyan;
                            validCol = true;
                            break;
                        default:
                            PrintErr("\nInvalid Selection! Please pick a valid number.");
                            break;
                    }
                    #endregion

                    game = new GameManager(new GameState(p1, p2, bg));
                    game.RunGame();


                    break;
                case '3':
                    Console.WriteLine("");
                    game.state.OpenHighScore();
                    break;
            }

            if (game.replay)    //allow replay of game 
                Main(args);




        }
    }
}

