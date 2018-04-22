using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Morabaraba9001.Data;
using static Morabaraba9001.Data.Position;

namespace Morabaraba9001.Display
{
    public interface IBoard
    {
        void PrintBoard();
        void PrintErr(string msg);
        bool IsValidPosition(Position pos);
        void SwapPlayers();
        bool IsValidInput(string str, Phase phase);
        bool CheckPhase();
    }

    public class Board : IBoard
    {
        IPlayer X;
        IPlayer Y;
        ConsoleColor defaultColor;
        IList<IPosition> CowList1; //list of cows for player 1
        IList<IPosition> CowList2; //list of cows for player 2

        public Board(IPlayer x, IPlayer y, ConsoleColor defCol = ConsoleColor.Gray)
        {
            X = x;
            Y = y;
            defaultColor = defCol;
        }

        /// <summary>
        /// Function to print the board to the console 
        /// </summary>
        /// <param name="state">Game State variable holding all necesarry data about the game</param>
        public void  PrintBoard()
        {
            X = new Player();
            Console.Clear();
            CowList1 =  X.Cows;
            CowList2 =  Y.Cows;

            /// Takes a position and returns the correct color to print it in. 
            ConsoleColor myColor(Position pos)
            {
                //iterate through current players cows
                for (int i = 0; i < CowList1.Count; i++)
                {
                    if (CowList1[i].ToString() == pos.ToString())
                        return X.playerColor;   //cow found return color
                }

                //iterate through opponent players cows
                for (int i = 0; i < CowList2.Count; i++)
                {
                    if (CowList2[i].ToString() == pos.ToString())
                        return Y.playerColor;  //cow found return color
                }               

                return defaultColor;   //else return default console color
            }

            //takes a position and prints it to console in the correct color

            void P(Position pos)
            {
                Console.ForegroundColor = myColor(pos);
                Console.Write(String.Format("{0}",pos));
            }

            void B(string board)

            {                
                Console.ForegroundColor = defaultColor;
                Console.Write(String.Format("{0}", board));
            }
            void PrintPlayer(IPlayer p)
            {
                Player x = (Player) p;
                Console.ForegroundColor = x.playerColor;
                string HUD = string.Format("{0} --> Placed Cows: {1} \t Dead Cows: {2} Cow State: {3}", x.name,x.placedCows,x.deadCows, x.cowState);
                Console.WriteLine(HUD);
                Console.ForegroundColor = defaultColor;
            }

            PrintPlayer(X);
            PrintPlayer(Y);
            B("\n");
            Console.WriteLine("Game Phase: " + state.phase);
            B("\n");
            B("\n");
            P(A7); B("----------"); P(D7); B("----------"); P(G7);
            B("\n| `.        |         /' |");
            B("\n|   "); P(B6); B("------"); P(D6); B("------"); P(F6); B("   |");
            B("\n|   | `.     |    /' |   |");
            B("\n|   |   "); P(C5); B("--"); P(D5); B("--"); P(E5); B("   |   |");
            B("\n|   |   |        |   |   |");
            B("\n"); P(A4); B("--"); P(B4); B("--"); P(C4); B("      "); P(E4); B("--"); P(F4); B("--"); P(G4);
            B("\n|   |   |        |   |   |");
            B("\n|   |   "); P(C3); B("--"); P(D3); B("--"); P(E3); B("   |   |");
            B("\n|   | /'    |     `. |   |");
            B("\n|   "); P(B2); B("------"); P(D2); B("------"); P(F2); B("   |");
            B("\n| /'         |        `. |");
            B("\n"); P(A1); B("----------"); P(D1); B("----------"); P(G1);
            B("\n");
        }


        /// <summary>
        /// Method to print generic error to console 
        /// </summary>
        /// <param name="msg">Message to print to console</param>
        public void PrintErr(string msg)
        {
            Console.WriteLine(String.Format("Error!:\t{0}. {1}",msg,"Press Enter To Continue."));
        }

        /// <summary>
        /// Method that checks if given position is a valid move by checking if position has already been taken by either player
        /// </summary>
        /// <param name="inputPos">Position player want to move to</param>
        /// <returns>True if position is free else returns false</returns>
        public bool IsValidPosition(Position inputPos)
        {
            if (current.Cows.Contains(inputPos) || opponent.Cows.Contains(inputPos))
                return false;
            else
                return true;
        }

        /// <summary>
        /// Method to swap players by making 'current' player into 'opponent' and vice versa
        /// </summary>
        /// <param name="state">Current Game State</param>
        public void SwapPlayers()
        {
            Player tmp = state.current;
            state.current = state.opponent;
            state.opponent = tmp;
            state.Turns++;

        }

        /// <summary>
        /// Method that takes in input form console and gamestate phase and determines if input is in correct format
        /// </summary>
        /// <param name="str">Input from console</param>
        /// <param name="phase">Current phase of the game state</param>
        /// <returns>True if input in correct format, otherwise false</returns>
        public  bool IsValidInput(string str, Phase phase)
        {
            if (phase == Phase.Placing)
            {
                if (str.Length == 2 && Char.IsLetter(str[0]) == true && Char.IsDigit(str[1]) == true)
                    if (tmpPos.GetPosition(str) != Position.XX)
                        return true;
            }

            if (phase == Phase.Moving)
            {
                if (str.Length == 4 && Char.IsLetter(str[0]) == true && Char.IsDigit(str[1]) == true && Char.IsLetter(str[2]) == true && Char.IsDigit(str[3]) == true)
                {
                    if (tmpPos.GetPosition(str.Substring(0, 2)) != Position.XX && tmpPos.GetPosition(str.Substring(2, 2)) != Position.XX)
                        return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Method that checks if game should move to next phase 
        /// </summary>
        /// <param name="state">Current Game State</param>
        /// <returns>True if game should move to next phase otherwise returns false</returns>
        public bool CheckPhase()
        {
            switch (state.phase)
            {
                case Phase.Placing:
                    if (state.current.placedCows == 12 && state.opponent.placedCows == 12)
                    {
                        state.phase = Phase.Moving;
                        return true;
                    }
                    break;

                case Phase.Moving:

                    if (state.current.Cows.Count == 2)
                    {
                        state.winner = state.opponent;
                        state.loser = state.current;
                        state.phase = Phase.Won;
                        return true;
                    }

                    if (state.opponent.Cows.Count == 2)
                    {
                        state.winner = state.current;
                        state.loser = state.opponent;
                        state.phase = Phase.Won;
                        return true;
                    }

                    break;

                default:
                    return false;

            }
            return false;
        }
    }
}
