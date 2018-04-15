using System;
using System.Collections.Generic;
using System.Linq;

using static Morabaraba2.Display.Board;

namespace Morabaraba2.Data
{
    public interface IGameManager
    {
        void RunGame();
        void ShootACow();
        void RunPlacing();
        void RunMoving();
        void Won(Player winner);
    }

    class GameManager : IGameManager
    {
     
        

        public GameState state;     //holds all the data of the current game state therefore access all player and game state variables through this field 
        public bool replay = false; //represents if players wish to replay the game after it finished. 
        Position tmpPos = new Position("");
        /// <summary>
        /// Declare new Game Manager with predefined game state 
        /// </summary>
        /// <param name="state">New Game State to manage</param>
        public GameManager(GameState state)
        {
            this.state = state;
        }

        /// <summary>
        /// Initializes Game and manages the game flow.
        /// </summary>
        public void RunGame()
        {
            RunPlacing();
            RunMoving();           
            Won(state.winner);
        }

        /// <summary>
        /// Inner method to shoot a cow
        /// </summary>
        public void ShootACow()    //inner method to shoot a cow
        {
            Console.Clear();
            PrintBoard(state);

            Console.WriteLine(String.Format("MILL! {0}, which cow do you want to shoot?", state.current.name));
            string shootMe = Console.ReadLine().ToUpper();
            if (state.IsValidInput(shootMe, Phase.Placing))   //validate input (criteria for validation is the same as placing phase)
            {
                Position shootMePos = (Position) tmpPos.GetPosition(shootMe);
                if (state.opponent.Cows.Contains(shootMePos))   //check if cow belongs to opponent 
                {
                    if (state.opponent.AllInAMill() || (!(state.opponent.InMill(shootMePos)) && (state.opponent.Cows.Contains(shootMePos))) )    //only shoot any cow if all opponent's cows are in a mill
                    {
                        state.opponent.ShootCow(shootMePos);
                        state.opponent.deadCows++;
                        if (state.opponent.Cows.Count <= 3)
                            state.opponent.FlyCows();           //fly players cows if they have 3 cows
                        PrintBoard(state);
                    }
                    else if (state.opponent.InMill(shootMePos))
                    {
                        PrintErr("Can't shoot cows in a mill, unless all opponent's cows are in a mill!!");
                        Console.ReadLine();
                        ShootACow();
                    }                    
                }
                else
                {
                    PrintErr("Can Only Shoot Opponent Cows!!");
                    Console.ReadLine();
                    ShootACow();
                }
            }
            else
            {
                PrintErr("Enter Position in Correct format {XX} where XX is a position on the board.");
                Console.ReadLine();
                ShootACow();
            }
        }

        /// <summary>
        /// Internal Method to run placing phase
        /// </summary>
        public void RunPlacing()
        {
            while (!state.CheckPhase(state)) //keeps running placing phase till checkphase returns true (therefore game will be moved to the next phase)
            {
                Console.Clear();
                PrintBoard(state);

                Console.WriteLine(state.current.name + ", where would you like to place a cow");
                string input = Console.ReadLine().ToUpper();

                if (state.IsValidInput(input, state.phase))   //validate string format 
                {
                    //get the placing of new position 
                    Position newPos = (Position) tmpPos.GetPosition(input);

                    if (state.IsValidPosition(newPos))    //validate position is free
                    {
                        //check for and get  mills before adding or removing cows 
                        List<Position[]> mills = (List<Position[]>) state.current.GetMills(newPos);

                        state.current.Cows.Add(newPos);     //add new cow to cow list
                        state.current.placedCows++;         //increase placed cows count

                        PrintBoard(state); //show placing of cows

                        //allow player to shootCow if a mill has been made
                        if (mills.Count > 0)
                        {
                            state.current.MyMills.AddRange(mills);  //add mills so that a player can't reuse mills or use more than one mill per turn                             
                            ShootACow();
                        }
                        state.SwapPlayers(state); 

                    }
                    
                   
                    else
                    {
                        PrintErr("Can't move to a position already in use!");
                        Console.ReadLine();
                        continue;
                    }

                }

                else
                {
                    PrintErr("Input must be in correct format!");
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
            while (!state.CheckPhase(state)) //keeps running moving phase till checkphase returns true (therefore game will be won)
            {
                Console.Clear();    
                PrintBoard(state);

                Console.WriteLine(state.current.name + ", which cow do you wish to move (Format: {XXYY} Where XX is starting position and YY is final position)");
                string input = Console.ReadLine().ToUpper();


                if (state.IsValidInput(input, state.phase))   //validate string format 
                {
                    //split input and get old position and new position 
                    Position oldPos = (Position) tmpPos.GetPosition(input.Substring(0,2));    
                    Position newPos = (Position) tmpPos.GetPosition(input.Substring(2, 2));

                    if (state.IsValidPosition(newPos))    //validate position is free
                    {
                        if (state.current.IsFlying() || tmpPos.GetAdjacentPositions(oldPos.pos).Contains(newPos))   //check for flying cows or if cows are adjacent 
                        {
                            //check for and get  mills before adding or removing cows 
                            state.current.Cows.Remove(oldPos); //temporarily remove old position before checking mills
                            List<Position[]> mills = (List<Position[]>) state.current.GetMills(newPos);
                            state.current.Cows.Add(oldPos);// add temp removed cow

                            state.current.MoveCow(oldPos, newPos);  //move the cows                            

                            PrintBoard(state); //show move of cows

                            //allow player to shootCow if a mill has been made
                            if (mills.Count > 0)
                            {
                                state.current.MyMills.AddRange(mills);  //add mills so that a player can't reuse mills or use more than one mill per turn                             
                                ShootACow();
                            }
                            state.SwapPlayers(state);
                        }
                        else
                        {
                            PrintErr("Can't move to a position that is not adjacent, if cows are not flying!");
                            Console.ReadLine();
                            continue;
                        }
                    }
                    else
                    {
                        PrintErr("Can't move to a position already in use!");
                        Console.ReadLine();
                        continue;
                    }


                }                  

                else
                {
                    PrintErr("Input must be in correct format!");
                    Console.ReadLine();
                    continue;
                }
            }
        }

        /// <summary>
        /// Internal Method to call when game is won 
        /// </summary>
        public void Won(Player winner)
        {           
            Console.WriteLine(winner.name + " is the winner!!! \n Play again? Y|N");
            if (char.ToUpper(Console.ReadKey().KeyChar) == 'Y')
                replay = true;
            
        }
    }
}
