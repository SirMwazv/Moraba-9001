using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Morabaraba9001.Data
{
    public interface IPosition
    {
        IPosition GetPosition(string pos);
        string ToString();
        IEnumerable<IPosition> GetAdjacentPositions(string pos);
        IEnumerable<IEnumerable<IPosition>> GetPossibleMills(string pos);
    }

    public class Position : IPosition
    {
        public string pos;

        //List of all possible mill combinations represented as an array of Positions
        public static List<Position[]> MillCombos = new List<Position[]>
        {
            new Position[] {A7,D7,G7},
            new Position[] {B6,D6,F6},
            new Position[] {C5,D5,E5},
            new Position[] {A4,B4,C4},
            new Position[] {E4,F4,G4},
            new Position[] {C3,D3,E3},
            new Position[] {B2,D2,F2},
            new Position[] {A1,D1,G1},
            new Position[] {A7,A4,A1},
            new Position[] {B6,B4,B2},
            new Position[] {C5,C4,C3},
            new Position[] {D7,D6,D5},
            new Position[] {D3,D2,D1},
            new Position[] {E5,E4,E3},
            new Position[] {F6,F4,F2},
            new Position[] {G7,G4,G1},
            new Position[] {A7,B6,C5},
            new Position[] {A1,B2,C3},
            new Position[] {E5,F6,G7},
            new Position[] {E3,F2,G1}

        };

        public Position(string pos) //Paramatized constructor --> use to make new positions 
        {
            this.pos = pos;
        }
        /// <summary>
        /// Method to convert string representation of a Cow into a 'Position' class instance
        /// </summary>
        /// <param name="pos">Cow to convert into Position</param>
        /// <returns>Position representation of a cow</returns>
        public IPosition GetPosition(string pos)
        {
            switch (pos)
            {
                case "A1":
                    return A1;

                case "A4":
                    return A4;

                case "A7":
                    return A7;

                case "B2":
                    return B2;

                case "B4":
                    return B4;

                case "B6":
                    return B6;

                case "C3":
                    return C3;

                case "C4":
                    return C4;

                case "C5":
                    return C5;

                case "D1":
                    return D1;

                case "D2":
                    return D2;

                case "D3":
                    return D3;

                case "D5":
                    return D5;

                case "D6":
                    return D6;

                case "D7":
                    return D7;

                case "E3":
                    return E3;

                case "E4":
                    return E4;

                case "E5":
                    return E5;

                case "F2":
                    return F2;

                case "F4":
                    return F4;

                case "F6":
                    return F6;

                case "G1":
                    return G1;

                case "G4":
                    return G4;

                case "G7":
                    return G7;
                default:
                    return XX;
            }
            //return StaticGetPosition(pos);
        }
        /// <summary>
        /// Takes in a position and return a list of all the positions on the board adjacent to it 
        /// </summary>
        /// <param name="pos">Position to get adjacent moves for</param>
        /// <returns>List of adjacent positions</returns>
        public IEnumerable<IPosition> GetAdjacentPositions(string pos)
        {
            List<Position> ret = new List<Position>();

            switch (pos)
            {
                case "A1":
                    List<Position> a1 = new List<Position> { D1, A4, B2 };
                    ret = a1;
                    break;

                case "A4":
                    List<Position> a4 = new List<Position> { A1, B4, A7 };
                    ret = a4;
                    break;

                case "A7":
                    List<Position> a7 = new List<Position> { A4, B6, D7 };
                    ret = a7;
                    break;

                case "B2":
                    List<Position> b2 = new List<Position> { A1, D2, C3, B4 };
                    ret = b2;
                    break;

                case "B4":
                    List<Position> b4 = new List<Position> { B2, A4, C4, B6 };
                    ret = b4;
                    break;

                case "B6":
                    List<Position> b6 = new List<Position> { B4, C5, D6, A7 };
                    ret = b6;
                    break;

                case "C3":
                    List<Position> c3 = new List<Position> { B2, C4, D3 };
                    ret = c3;
                    break;

                case "C4":
                    List<Position> c4 = new List<Position> { C3, B4, C5 };
                    ret = c4;
                    break;

                case "C5":
                    List<Position> c5 = new List<Position> { C4, D5, B6 };
                    ret = c5;
                    break;

                case "D1":
                    List<Position> d1 = new List<Position> { A1, G1, D2 };
                    ret = d1;
                    break;

                case "D2":
                    List<Position> d2 = new List<Position> { D1, F2, D3, B2 };
                    ret = d2;
                    break;

                case "D3":
                    List<Position> d3 = new List<Position> { D2, E3, C3 };
                    ret = d3;
                    break;

                case "D5":
                    List<Position> d5 = new List<Position> { E5, D6, C5 };
                    ret = d5;
                    break;

                case "D6":
                    List<Position> d6 = new List<Position> { D5, F6, D7, B6 };
                    ret = d6;
                    break;

                case "D7":
                    List<Position> d7 = new List<Position> { D6, G7, A7 };
                    ret = d7;
                    break;

                case "E3":
                    List<Position> e3 = new List<Position> { F2, E4, D3 };
                    ret = e3;
                    break;

                case "E4":
                    List<Position> e4 = new List<Position> { E3, F4, E5 };
                    ret = e4;
                    break;

                case "E5":
                    List<Position> e5 = new List<Position> { E4, F6, D5 };
                    ret = e5;
                    break;

                case "F2":
                    List<Position> f2 = new List<Position> { G1, F4, E3, D2 };
                    ret = f2;
                    break;

                case "F4":
                    List<Position> f4 = new List<Position> { F2, G4, F6, E4 };
                    ret = f4;
                    break;

                case "F6":
                    List<Position> f6 = new List<Position> { F4, G7, D6, E5 };
                    ret = f6;
                    break;

                case "G1":
                    List<Position> g1 = new List<Position> { D1, G4, F2 };
                    ret = g1;
                    break;

                case "G4":
                    List<Position> g4 = new List<Position> { G1, F4, G7 };
                    ret = g4;
                    break;

                case "G7":
                    List<Position> g7 = new List<Position> { G4, F6, D7 };
                    ret = g7;
                    break;

            }
            return ret;
            //return StaticGetAdjacentPositions(pos);
        }
        /// <summary>
        /// Takes in a position and return all possible mill combos involving that position 
        /// </summary>
        /// <param name="pos">Position to get mill combos for</param>
        /// <returns>List of all mill combos with that position as a Position Array</returns>
        public IEnumerable<IEnumerable<IPosition>> GetPossibleMills(string pos)
        {
            List<Position[]> result = new List<Position[]>();

            switch (pos)
            {
                case "A1":
                    List<Position[]> a1 = new List<Position[]>
                    {
                        new Position[] {A1,D1,G1},
                        new Position[] {A7,A4,A1},
                        new Position[] {A1,B2,C3},
                    };
                    result = a1;
                    break;

                case "A4":
                    List<Position[]> a4 = new List<Position[]>
                        {
                            new Position[] {A4,B4,C4},
                            new Position[] {A7,A4,A1},
                        };
                    result = a4;
                    break;

                case "A7":
                    List<Position[]> a7 = new List<Position[]>
                        {
                            new Position[] {A7,D7,G7},
                            new Position[] {A7,A4,A1},
                            new Position[] {A7,B6,C5},
                        };
                    result = a7;
                    break;

                case "B2":
                    List<Position[]> b2 = new List<Position[]>
                        {
                            new Position[] {B2,D2,F2},
                            new Position[] {B6,B4,B2},
                            new Position[] {A1,B2,C3},
                        };
                    result = b2;
                    break;

                case "B4":
                    List<Position[]> b4 = new List<Position[]>
                        {
                            new Position[] {A4,B4,C4},
                            new Position[] {B6,B4,B2},
                        };
                    result = b4;
                    break;

                case "B6":
                    List<Position[]> b6 = new List<Position[]>
                        {
                            new Position[] {B6,D6,F6},
                            new Position[] {B6,B4,B2},
                            new Position[] {A7,B6,C5},
                        };
                    result = b6;
                    break;

                case "C3":
                    List<Position[]> c3 = new List<Position[]>
                    {
                        new Position[] {C3,D3,E3},
                        new Position[] {C5,C4,C3},
                        new Position[] {A1,B2,C3},
                    };
                    result = c3;
                    break;

                case "C4":
                    List<Position[]> c4 = new List<Position[]>
                    {
                        new Position[] {A4,B4,C4},
                        new Position[] {C5,C4,C3},
                    };
                    result = c4;
                    break;

                case "C5":
                    List<Position[]> c5 = new List<Position[]>
                   {
                       new Position[] {C5,D5,E5},
                       new Position[] {C5,C4,C3},
                       new Position[] {A7,B6,C5},
                   };
                    result = c5;
                    break;

                case "D1":
                    List<Position[]> d1 = new List<Position[]>
                   {
                       new Position[] {A1,D1,G1},
                       new Position[] {D3,D2,D1},
                   };
                    result = d1;
                    break;

                case "D2":
                    List<Position[]> d2 = new List<Position[]>
                   {
                        new Position[] {B2,D2,F2},
                        new Position[] {D3,D2,D1},
                   };
                    result = d2;
                    break;

                case "D3":
                    List<Position[]> d3 = new List<Position[]>
                   {
                        new Position[] {C3,D3,E3},
                        new Position[] {D3,D2,D1},
                   };
                    result = d3;
                    break;

                case "D5":
                    List<Position[]> d5 = new List<Position[]>
                   {
                       new Position[] {C5,D5,E5},
                       new Position[] {D7,D6,D5},
                   };
                    result = d5;
                    break;

                case "D6":
                    List<Position[]> d6 = new List<Position[]>
                   {
                       new Position[] {B6,D6,F6},
                       new Position[] {D7,D6,D5},
                   };
                    result = d6;
                    break;

                case "D7":
                    List<Position[]> d7 = new List<Position[]>
                   {
                       new Position[] {A7,D7,G7},
                       new Position[] {D7,D6,D5},
                   };
                    result = d7;
                    break;

                case "E3":
                    List<Position[]> e3 = new List<Position[]>
                   {
                       new Position[] {C3,D3,E3},
                       new Position[] {E5,E4,E3},
                       new Position[] {E3,F2,G1}
                   };
                    result = e3;
                    break;

                case "E4":
                    List<Position[]> e4 = new List<Position[]>
                   {
                       new Position[] {E4,F4,G4},
                       new Position[] {E5,E4,E3},
                   };
                    result = e4;
                    break;

                case "E5":
                    List<Position[]> e5 = new List<Position[]>
                   {
                       new Position[] {C5,D5,E5},
                       new Position[] {E5,E4,E3},
                        new Position[] {E5,F6,G7},
                   };
                    result = e5;
                    break;

                case "F2":
                    List<Position[]> f2 = new List<Position[]>
                   {
                       new Position[] {B2,D2,F2},
                       new Position[] {F6,F4,F2},
                       new Position[] {E3,F2,G1}
                   };
                    result = f2;
                    break;

                case "F4":
                    List<Position[]> f4 = new List<Position[]>
                   {
                       new Position[] {E4,F4,G4},
                       new Position[] {F6,F4,F2},
                   };
                    result = f4;
                    break;

                case "F6":
                    List<Position[]> f6 = new List<Position[]>
                   {
                       new Position[] {B6,D6,F6},
                       new Position[] {F6,F4,F2},
                       new Position[] {E5,F6,G7},
                   };
                    result = f6;
                    break;

                case "G1":
                    List<Position[]> g1 = new List<Position[]>
                   {
                       new Position[] {A1,D1,G1},
                       new Position[] {G7,G4,G1},
                       new Position[] {E3,F2,G1}
                   };
                    result = g1;
                    break;

                case "G4":
                    List<Position[]> g4 = new List<Position[]>
                   {
                       new Position[] {E4,F4,G4},
                       new Position[] {G7,G4,G1},
                   };
                    result = g4;
                    break;

                case "G7":
                    List<Position[]> g7 = new List<Position[]>
                   {
                       new Position[] {A7,D7,G7},
                       new Position[] {G7,G4,G1},
                       new Position[] {E5,F6,G7},
                   };
                    result = g7;
                    break;

            }

            return result;
            //return StaticGetPossibleMills(pos);
        }

        public override string ToString()   //This is called when using String.Format to print board positions 
        {
            return pos;
        }

        //static versions of methods to call

       


        

      

        #region All Possible Positions on the board 
        public static Position A1 = new Position("A1");
        public static Position A4 = new Position("A4");
        public static Position A7 = new Position("A7");

        public static Position B2 = new Position("B2");
        public static Position B4 = new Position("B4");
        public static Position B6 = new Position("B6");

        public static Position C3 = new Position("C3");
        public static Position C4 = new Position("C4");
        public static Position C5 = new Position("C5");

        public static Position D1 = new Position("D1");
        public static Position D2 = new Position("D2");
        public static Position D3 = new Position("D3");
        public static Position D5 = new Position("D5");
        public static Position D6 = new Position("D6");
        public static Position D7 = new Position("D7");

        public static Position E3 = new Position("E3");
        public static Position E4 = new Position("E4");
        public static Position E5 = new Position("E5");

        public static Position F2 = new Position("F2");
        public static Position F4 = new Position("F4");
        public static Position F6 = new Position("F6");

        public static Position G1 = new Position("G1");
        public static Position G4 = new Position("G4");
        public static Position G7 = new Position("G7");

        public static Position XX = new Position("XX"); //denotes invalid position 
        #endregion

    }
}
