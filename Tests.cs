using System;
using NUnit.Framework;
using System.Linq;
using Morabaraba9001.Data;
using System.Collections.Generic;
using NSubstitute;
using static Morabaraba9001.Display.Board;
using static Morabaraba9001.Data.Position;

namespace Morabaraba9001.Test 
{
    [TestFixture]
    public class Tests
    {
        #region Placing
        [Test]
        public void EmptyBoardOnGameStart()//if opponent and current have no placed cows, board is empty
        {
            //Arrange
            GameState test = new GameState();
           

            //Act
            Player testplayer1 = test.current;
            Player testplayer2 = test.opponent;

            //Assert
            Assert.That(testplayer1.placedCows == 0 && testplayer2.placedCows == 0);


        }
        [Test]
        public void PlayerWithDarkCowsPlaysFirst() //current is our equivalent of the player with dark pieces(dark colour being red) and the player who plays first in the QuickPlay Feature
        {
            //Arrange
            GameState test = new GameState();
            Player testplayer1 = new Player();

            //Act
            testplayer1 = test.current;

            //Assert
            Assert.That(testplayer1.playerColor == ConsoleColor.Red);
        }   
        [Test]
        public void CowsCanOnlyBePlacedOnEmptySpaces()
        {
            //Arrange
            GameState test = new GameState();
            Player testplayer1 = new Player();
            Player testplayer2 = new Player();
            testplayer1 = test.current;
            testplayer2 = test.opponent;
            Position[] player1 = new Position[] { A7, D7, C5 };
            testplayer1.Cows = player1.ToList();
            Position input = A7;

            //Act           
            if (test.IsValidPosition(input) == true)
            {
                testplayer2.Cows.Add(input);
            }
            
            //Assert
            Assert.That(!testplayer2.Cows.Contains(A7));

        }
        [Test]
        public void MaximumOf12PlacementsPerPlayer()//Our method of preventing more than 12 placements per player is by switching to the moving phase once both players reach 12 cows.
        {
            //Arrange
            GameState test = new GameState
            {
                phase = Phase.Placing
            };
            test.current.placedCows = 12;
            test.opponent.placedCows = 12;

            //Act
            bool result = test.CheckPhase(test);

            //Assert
            Assert.That(result != false);
   
        }
        [Test]
        public void CowsCannotBeMovedDuringPlacement()
        {
            //Arrange
            //Act
            //Assert
        }
        #endregion
        
        #region Moving
        [Test]
        public void CowCanOnlyMoveToAdjacentSpace()
        {
            //Arrange
            GameState test = new GameState();

            string oldpos1 = "A7";
            Position newpos1 = D7;

            string oldpos2 = "A7";
            Position newpos2 = A4;          

            //Act
            bool isitadjacent1 = StaticGetAdjacentPositions(oldpos1).Contains(newpos1);
            bool isitadjacent2 = StaticGetAdjacentPositions(oldpos2).Contains(newpos2);     
            
            //Assert
            Assert.That(isitadjacent1 = true);
            Assert.That(isitadjacent2 = true);
           
        }
        [Test]
        public void CowCanOnlyMoveOnEmptySpace()
        {
            //Arrange
            GameState test = new GameState
            {
                phase = Phase.Moving
            };
            Player testplayer1 = new Player();
            Player testplayer2 = new Player();
            testplayer1 = test.current;
            testplayer2 = test.opponent;
            Position[] player1 = new Position[] { A7, D7, C5 };
            testplayer1.Cows = player1.ToList();
            string oldpos = "A4";
            Position newpos = A7;

            //Act                      
            if (test.IsValidPosition(newpos) == true) 
            {
                testplayer2.Cows.Add(newpos);
            }

            //Assert
            Assert.That(!testplayer2.Cows.Contains(A7));
        } //needs to be looked at
        [Test]
        public void MovingDoesNotIncreaseOrDecreaseNumberOfCowsOnTheBoard()
        {
            //Arrange
            Position[] incompleteMills = new Position[] { A7, D7, C5, D5, E4, F4, B2 };
            Player testPlay = new Player();
            testPlay.Cows = incompleteMills.ToList();
            int CowsBfore = testPlay.Cows.Count;
            int CowsAfter = 0;

            //Act
            testPlay.MoveCow(B2, D2);
            CowsAfter = testPlay.Cows.Count;
            //Assert
            Assert.That(CowsBfore == CowsAfter);



        }
        #endregion
        
        #region Flying
        [Test]
        public void CowsCanOnlyMoveToNoneAdjacentSpacesIfFlying()
        {
            //Arrange
            Player p = new Player();
            p.cowState = "Flying";
            Position[] cows = new Position[] { A7, D7, C5};
            p.Cows = cows.ToList();

            //Act
            p.MoveCow(A7, E5);

            //Assert
            Assert.That(p.Cows[0] == D7);
            Assert.That(p.Cows[1] == C5);
            Assert.That(p.Cows[2] == E5);
        }
        #endregion

        #region In General
        Position[] incompleteMills = new Position[] {
                A7, D7,
                B6,D6,
                C5, D5,
                A4,B4,
                E4, F4,
                C3,D3,
                B2, D2,
                A1,D1,
                A7,A4,
                B6,B4,
                C5,C4,
                D7,D6,
                D3,D2,
                E5,E4,
                F6,F4,
                G7,G4,
                A7,B6,
                A1,B2,
                E5,F6,
                E3,F2
            };
        List<Position> missingCows = new List<Position>()
        {
            G7, F6, E5, C4 ,G4 ,E3, F2, G1 ,A1, B2, C3, D5, D1, E3, F2, G1 ,C5, C3, G7 ,G1
        };  
        [Test]
        public void MillFormedByThreeOfTheSameColourCowInALine()
        {
            //Arrange            
            Player testPlay = new Player("test",ConsoleColor.Black);
            testPlay.Cows = incompleteMills.ToList();
            bool valid = true;

            //Act
            foreach (var cow in missingCows)
            {
                if (!(testPlay.GetMills(cow).Count() > 0))
                    valid = false;
            }

            //Assert
            Assert.That(valid);
        }
        [Test]
        public void MillNotFormedByDifferentColouredCowsInALine()
        {
            //Arrange            
            Player testPlay1 = new Player("test", ConsoleColor.Black);
            Player testPlay2 = new Player("test", ConsoleColor.White);
            testPlay1.Cows = incompleteMills.ToList();
            bool valid = true;

            //Act
            foreach (var cow in missingCows)
            {
                if (!(testPlay2.GetMills(cow).Count() == 0))
                    valid = false;
            }

            //Assert
            Assert.That(valid);
        }
        [Test]
        public void ConnectedSpacesWithCowsDoNotFormALine()
        {
            //Arrange
            List<Position> incompleteMills = new List<Position>() {A7,G7,C5,E5,E4,G4,B2,F2 };
            Player testPlayer = new Player();
            testPlayer.Cows = incompleteMills;


            //Act
            var test1 = testPlayer.GetMills(G7);
            var test2 = testPlayer.GetMills(E5);
            var test3 = testPlayer.GetMills(G4);
            var test4 = testPlayer.GetMills(F2);

            //Assert
            Assert.That(test1.Count() == 0);
            Assert.That(test2.Count() == 0);
            Assert.That(test3.Count() == 0);
            Assert.That(test4.Count() == 0);
        }
        [Test]
        public void MultipleMillsAreDetectedInTheSameTurn()//come back to this
        {
            //Arrange
            Position[] playerCows = new Position[] { A4, A1, D7, G7 };
            Player testPlay = new Player();
            testPlay.Cows = playerCows.ToList();
            var mill1 = new Position[] { A7, D7, G7 };
            var mill2 = new Position[] { A7, A4, A1 };

            //Act
            List<Position[]> x = (List<Position[]>) testPlay.GetMills(A7);

            //Assert
            Assert.That( x[0][0] == mill1[0] && x[0][1] == mill1[1] && x[0][2] == mill1[2]);
            Assert.That(x[1][0] == mill2[0] && x[1][1] == mill2[1] && x[1][2] == mill2[2]);


        }
        [Test]
        public void CannotShootACowInAMillWhenNonMillCowsExist()
        {
            //Arrange
            Position[] player1Cows = new Position[] { C5, D5, E5, E4};
            Position[] player2Cows = new Position[] { A7, D7, G7, B2 };
            Player testPlayer1 = new Player();
            testPlayer1.Cows = player1Cows.ToList();
            Player testPlayer2 = new Player();
            testPlayer2.Cows = player2Cows.ToList();
            testPlayer2.MyMills = new List<Position[]> { new Position[] { A7, D7, G7 } };

            //Act
            bool check = testPlayer2.AllInAMill();

            //Assert
            Assert.That(!check);    //should be true if all cows are not in a mill
        }
        [Test]
        public void CanShootACowInAMillWhenNonMillCowsDoNotExist()
        {
            
            //Arrange
            Position[] player1Cows = new Position[] { C5, D5, E5, E4, B2 };
            Position[] player2Cows = new Position[] { A7, D7, G7 };
            Player testPlayer1 = new Player();
            testPlayer1.Cows = player1Cows.ToList();
            Player testPlayer2 = new Player();
            testPlayer2.Cows = player2Cows.ToList();
            testPlayer2.MyMills = new List<Position[]> { new Position[] { A7, D7, G7 } };

            //Act
            bool check = testPlayer2.AllInAMill();

            //Assert
            Assert.That(check);    //should be true if all cows are in a mill
        }
        [Test]
        public void PlayerIsResponsibleForShootingOwnCows()
        {
            //Arrange
            Position[] incompleteMills = new Position[] { A7, D7, C5, D5, E4, F4, B2, D2 };
            Player testPlay = new Player();
            testPlay.Cows = incompleteMills.ToList();

            //Act
            testPlay.ShootCow(A7);

            //Assert
            Assert.That(!testPlay.Cows.Contains(A7));
        }
        #endregion



    }
}
