using System;
using System.Collections.Generic;
using System.IO;
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
        int Turns { get; set; }
        IPlayer winner { get; }
        IPlayer loser { get; }
        IPlayer X { get; set; }
        IPlayer Y { get; set; }
        Phase phase { get; set; }
        ConsoleColor defaultColor { get; set; }
        IPosition Positions { get; set; }
        void RecordHighScore();
        void OpenHighScore();
    }
    public enum Phase { Placing, Moving, Won, Draw }
    public class Board : IBoard
    {
        IPlayer x, y, Winner, Loser;
        ConsoleColor myDefaultColor;
        IList<Position> CowList1; //list of cows for player 1
        IList<Position> CowList2; //list of cows for player 2
        Phase myPhase;
        int turns;
        public IPosition myPositions = new Position("XX");    //global to refer to all positions

        public IPosition Positions { get { return myPositions; } set { myPositions = value; } }
        public int Turns { get { return turns; } set { turns = value; } }

        public ConsoleColor defaultColor {get { return myDefaultColor; } set { myDefaultColor = value; } }
        public IPlayer winner { get { return Winner; } set { Winner = value; } }
        public IPlayer loser { get { return Loser; } set { Loser = value; } }
        public IPlayer X { get { return x; } set { x = value; } }
        public IPlayer Y { get { return y; } set { y = value; } }

        public Phase phase { get { return myPhase; } set { myPhase = value; } }

        public Board()
        {

        } //for testing 

        public Board(IPlayer x, IPlayer y, ConsoleColor defCol = ConsoleColor.Gray)
        {
            X = x;
            Y = y;
            defaultColor = defCol;
            winner = new Player();
            loser = new Player();
        }

        /// <summary>
        /// Function to print the board to the console 
        /// </summary>
        /// <param name="state">Game State variable holding all necesarry data about the game</param>
        public void PrintBoard()
        { 
            Console.Clear();
            CowList1 = X.Cows;
            CowList2 = Y.Cows;

            /// Takes a position and returns the correct color to print it in. 
            ConsoleColor myColor(Position pos)
            {
                //iterate through X players cows
                for (int i = 0; i < CowList1.Count; i++)
                {
                    if (CowList1[i].ToString() == pos.ToString())
                        return X.playerColor;   //cow found return color
                }

                //iterate through Y players cows
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
                Console.Write(String.Format("{0}", pos));
            }

            void B(string board)

            {
                Console.ForegroundColor = defaultColor;
                Console.Write(String.Format("{0}", board));
            }
            void PrintPlayer(IPlayer x)
            {
                //Player x = (Player)p;
                Console.ForegroundColor = x.playerColor;
                string HUD = string.Format("{0} --> Placed Cows: {1} \t Dead Cows: {2} Cow State: {3}", x.name, x.placedCows, x.deadCows, x.cowState);
                Console.WriteLine(HUD);
                Console.ForegroundColor = defaultColor;
            }

            PrintPlayer(X);
            PrintPlayer(Y);
            B("\n");
            Console.WriteLine("Game Phase: " + phase);
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
            Console.WriteLine(String.Format("Error!:\t{0}. {1}", msg, "Press Enter To Continue."));
        }

        /// <summary>
        /// Method that checks if given position is a valid move by checking if position has already been taken by either player
        /// </summary>
        /// <param name="inputPos">Position player want to move to</param>
        /// <returns>True if position is free else returns false</returns>
        public bool IsValidPosition(Position inputPos)
        {
            if (X.Cows.Contains(inputPos) || Y.Cows.Contains(inputPos))
                return false;
            else
                return true;
        }

        /// <summary>
        /// Method to swap players by making 'X' player into 'Y' and vice versa
        /// </summary>
        /// <param name="state">X Game State</param>
        public void SwapPlayers()
        {
            Player tmp = (Player)X;
            X = Y;
            Y = tmp;
            Turns++;

        }

        /// <summary>
        /// Method that takes in input form console and gamestate phase and determines if input is in correct format
        /// </summary>
        /// <param name="str">Input from console</param>
        /// <param name="phase">X phase of the game state</param>
        /// <returns>True if input in correct format, otherwise false</returns>
        public bool IsValidInput(string str, Phase phase)
        {
            if (phase == Phase.Placing)
            {
                if (str.Length == 2 && Char.IsLetter(str[0]) == true && Char.IsDigit(str[1]) == true)
                    if (Positions.GetPosition(str) != Position.XX)
                        return true;
            }

            if (phase == Phase.Moving)
            {
                if (str.Length == 4 && Char.IsLetter(str[0]) == true && Char.IsDigit(str[1]) == true && Char.IsLetter(str[2]) == true && Char.IsDigit(str[3]) == true)
                {
                    if (Positions.GetPosition(str.Substring(0, 2)) != Position.XX && Positions.GetPosition(str.Substring(2, 2)) != Position.XX)
                        return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Method that checks if game should move to next phase 
        /// </summary>
        /// <param name="state">X Game State</param>
        /// <returns>True if game should move to next phase otherwise returns false</returns>
        public bool CheckPhase()
        {
            switch (phase)
            {
                case Phase.Placing:
                    if (X.placedCows == 12 && Y.placedCows == 12)
                    {
                        phase = Phase.Moving;
                        return true;
                    }
                    break;

                case Phase.Moving:

                    if (X.Cows.Count == 2)
                    {
                        winner = Y;
                        loser = X;
                        phase = Phase.Won;
                        return true;
                    }

                    if (Y.Cows.Count == 2)
                    {
                        winner = X;
                        loser = Y;
                        phase = Phase.Won;
                        return true;
                    }

                    break;

                default:
                    return false;

            }
            return false;
        }

        public void RecordHighScore()
        {
            if (!File.Exists("Highscore.txt"))  //create file if not exists
                File.CreateText("Highscore.txt");

            using (StreamWriter score = new StreamWriter("Highscore.txt"))  //automatically dispose of stream after use
            {
                score.Write(String.Format("Winner: {0}, Loser: {1}, Number of Turns: {2} \n", winner.name, loser.name, Turns));
            }
        }

        public void OpenHighScore()
        {
            try
            {
                using (StreamReader score = File.OpenText("Highscore.txt"))
                {
                    while (!score.EndOfStream)
                    {
                        Console.WriteLine(score.ReadLine());
                    }
                }
                Console.ReadLine();
            }
            catch (FileNotFoundException x)
            {
                Console.WriteLine(x.Message + "\nNo HighScores Exist! Play The game to make new highscores.");
                Console.ReadLine();
            }
        }
    }
}
