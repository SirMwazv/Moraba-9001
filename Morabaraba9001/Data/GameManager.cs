using System;
using System.Collections.Generic;
using System.Linq;
using Morabaraba9001.Display;

namespace Morabaraba9001.Data
{
    public interface IGameManager
    {
        void RunGame();
        void ShootACow();
        void RunPlacing();
        void RunMoving();
        void Won(IPlayer winner);
    }

    public class GameManager : IGameManager
    {       
        IPlayer X, Y;
        IBoard b;
        public bool replay = false; //represents if players wish to replay the game after it finished. 
        IPosition tmpPos;

        /// <summary>
        /// Declare new Game Manager with predefined game state 
        /// </summary>
        /// <param name="state">New Game State to manage</param>
        public GameManager()
        {
            X = new Player();
            Y = new Player();
            b = new Board(X,Y);
            
        }       

        public void init()
        {
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
                    X = new Player("Player 1", ConsoleColor.Red);
                    Y = new Player("Player 2", ConsoleColor.Green);
                    b.Turns = 0;
                    b.phase = Phase.Placing;                    
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
                                b.PrintErr("\nInvalid Selection! Please pick a valid number.");
                                Console.ReadLine();
                                break;
                        }
                    }
                    Player p1 = new Player(p1Name, p1Col);
                    X = p1;
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
                            b.PrintErr("\nInvalid Selection! NOTE: You can't have the same colour as your opponent!");
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
                                b.PrintErr("\nInvalid Selection! Please pick a valid number.");
                                Console.ReadLine();
                                break;
                        }
                    }
                    Player p2 = new Player(p2Name, p2Col);
                    Y = p2;
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
                            b.PrintErr("\nInvalid Selection! Please pick a valid number.");
                            break;
                    }
                    bg = b.defaultColor;
                    #endregion
                    break;
                case '3':
                    Console.WriteLine("");
                    b.OpenHighScore();
                    break;
            }

            if (replay)
                init();
        }

        /// <summary>
        /// Initializes Game and manages the game flow.
        /// </summary>
        public void RunGame()
        {
            RunPlacing();
            RunMoving();           
            Won(b.winner);
        }

        /// <summary>
        /// Inner method to shoot a cow
        /// </summary>
        public void ShootACow()    //inner method to shoot a cow
        {
            Console.Clear();
            b.PrintBoard();

            Console.WriteLine(String.Format("MILL! {0}, which cow do you want to shoot?", b.X.name));
            string shootMe = Console.ReadLine().ToUpper();
            if (b.IsValidInput(shootMe, Phase.Placing))   //validate input (criteria for validation is the same as placing phase)
            {
                Position shootMePos = (Position) tmpPos.GetPosition(shootMe);
                if (b.Y.Cows.Contains(shootMePos))   //check if cow belongs to opponent 
                {
                    if (b.Y.AllInAMill() || (!(b.Y.InMill(shootMePos)) && (b.Y.Cows.Contains(shootMePos))) )    //only shoot any cow if all opponent's cows are in a mill
                    {
                        b.Y.ShootCow(shootMePos);
                        b.Y.deadCows++;
                        if (b.Y.Cows.Count <= 3)
                            b.Y.FlyCows();           //fly players cows if they have 3 cows
                        b.PrintBoard();
                    }
                    else if (b.Y.InMill(shootMePos))
                    {
                        b.PrintErr("Can't shoot cows in a mill, unless all opponent's cows are in a mill!!");
                        Console.ReadLine();
                        ShootACow();
                    }                    
                }
                else
                {
                    b.PrintErr("Can Only Shoot Opponent Cows!!");
                    Console.ReadLine();
                    ShootACow();
                }
            }
            else
            {
                b.PrintErr("Enter Position in Correct format {XX} where XX is a position on the board.");
                Console.ReadLine();
                ShootACow();
            }
        }

        /// <summary>
        /// Internal Method to run placing phase
        /// </summary>
        public void RunPlacing()
        {
            while (!b.CheckPhase()) //keeps running placing phase till checkphase returns true (therefore game will be moved to the next phase)
            {
                Console.Clear();
                b.PrintBoard();

                Console.WriteLine(b.X.name + ", where would you like to place a cow");
                string input = Console.ReadLine().ToUpper();

                if (b.IsValidInput(input, b.phase))   //validate string format 
                {
                    //get the placing of new position 
                    Position newPos = (Position) tmpPos.GetPosition(input);

                    if (b.IsValidPosition(newPos))    //validate position is free
                    {
                        //check for and get  mills before adding or removing cows 
                        List<Position[]> mills = (List<Position[]>) b.X.GetMills(newPos);

                        b.X.Cows.Add(newPos);     //add new cow to cow list
                        b.X.placedCows++;         //increase placed cows count

                        b.PrintBoard(); //show placing of cows

                        //allow player to shootCow if a mill has been made
                        if (mills.Count > 0)
                        {
                            b.X.MyMills.AddRange(mills);  //add mills so that a player can't reuse mills or use more than one mill per turn                             
                            ShootACow();
                        }
                        b.SwapPlayers(); 

                    }
                    
                   
                    else
                    {
                        b.PrintErr("Can't move to a position already in use!");
                        Console.ReadLine();
                        continue;
                    }

                }

                else
                {
                    b.PrintErr("Input must be in correct format!");
                    Console.ReadLine();
                    continue;
                }
            }
        }

        /// <summary>
        /// Internal Method to run moving phase
        /// </summary>
        public void RunMoving()
        {
            while (!b.CheckPhase()) //keeps running moving phase till checkphase returns true (therefore game will be won)
            {
                Console.Clear();    
                b.PrintBoard();

                Console.WriteLine(b.X.name + ", which cow do you wish to move (Format: {XXYY} Where XX is starting position and YY is final position)");
                string input = Console.ReadLine().ToUpper();


                if (b.IsValidInput(input, b.phase))   //validate string format 
                {
                    //split input and get old position and new position 
                    Position oldPos = (Position) tmpPos.GetPosition(input.Substring(0,2));    
                    Position newPos = (Position) tmpPos.GetPosition(input.Substring(2, 2));

                    if (b.IsValidPosition(newPos) && b.X.Cows.Contains(oldPos))    //validate new position is free and player owns the old cow 
                    {
                        if (b.X.IsFlying() || tmpPos.GetAdjacentPositions(oldPos.pos).Contains(newPos))   //check for flying cows or if cows are adjacent 
                        {
                            //check for and get  mills before adding or removing cows 
                            b.X.Cows.Remove(oldPos); //temporarily remove old position before checking mills
                            List<Position[]> mills = (List<Position[]>) b.X.GetMills(newPos);
                            b.X.Cows.Add(oldPos);// add temp removed cow

                            b.X.MoveCow(oldPos, newPos);  //move the cows                            

                            b.PrintBoard(); //show move of cows

                            //allow player to shootCow if a mill has been made
                            if (mills.Count > 0)
                            {
                                b.X.MyMills.AddRange(mills);  //add mills so that a player can't reuse mills or use more than one mill per turn                             
                                ShootACow();
                            }
                            b.SwapPlayers();
                        }
                        else
                        {
                            b.PrintErr("Can't move to a position that is not adjacent, if cows are not flying!");
                            Console.ReadLine();
                            continue;
                        }
                    }
                    else
                    {
                        b.PrintErr("Can only move your cows to a free position!");
                        Console.ReadLine();
                        continue;
                    }


                }                  

                else
                {
                    b.PrintErr("Input must be in correct format!");
                    Console.ReadLine();
                    continue;
                }
            }
        }

        /// <summary>
        /// Internal Method to call when game is won 
        /// </summary>
        public void Won(IPlayer w)
        {
            Player winner = (Player)w;
            Console.WriteLine(winner.name + " is the winner!!! \n Play again? Y|N");
            if (char.ToUpper(Console.ReadKey().KeyChar) == 'Y')
                replay = true;
            
        }
    }
}
