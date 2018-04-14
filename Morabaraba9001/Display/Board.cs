using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Morabaraba2.Data;
using static Morabaraba2.Data.Position;

namespace Morabaraba2.Display
{
    class Board
    {
        static List<Position> CowList1; //list of cows for player 1
        static List<Position> CowList2; //list of cows for player 2

        /// <summary>
        /// Function to print the board to the console 
        /// </summary>
        /// <param name="state">Game State variable holding all necesarry data about the game</param>
        public static void  PrintBoard(GameState state)
        {
            Console.Clear();
            CowList1 = state.current.Cows;
            CowList2 = state.opponent.Cows;

            /// Takes a position and returns the correct color to print it in. 
            ConsoleColor myColor(Position pos)
            {
                //iterate through current players cows
                for (int i = 0; i < CowList1.Count; i++)
                {
                    if (CowList1[i].ToString() == pos.ToString())
                        return state.current.playerColor;   //cow found return color
                }

                //iterate through opponent players cows
                for (int i = 0; i < CowList2.Count; i++)
                {
                    if (CowList2[i].ToString() == pos.ToString())
                        return state.opponent.playerColor;  //cow found return color
                }               

                return state.defaultColor;   //else return default console color
            }

            //takes a position and prints it to console in the correct color

            void P(Position pos)
            {
                Console.ForegroundColor = myColor(pos);
                Console.Write(String.Format("{0}",pos));
            }

            void B(string board)

            {                
                Console.ForegroundColor = state.defaultColor;
                Console.Write(String.Format("{0}", board));
            }
            void PrintPlayer(Player x)
            {
                Console.ForegroundColor = x.playerColor;
                string HUD = string.Format("{0} --> Placed Cows: {1} \t Dead Cows: {2} Cow State: {3}", x.name,x.placedCows,x.deadCows, x.cowState);
                Console.WriteLine(HUD);
                Console.ForegroundColor = state.defaultColor;
            }

            PrintPlayer(state.current);
            PrintPlayer(state.opponent);
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
        public static void PrintErr(string msg)
        {
            Console.WriteLine(String.Format("Error!:\t{0}. {1}",msg,"Press Enter To Continue."));
        }
    }
}
