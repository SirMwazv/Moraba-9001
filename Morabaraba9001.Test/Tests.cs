using System;
using NUnit.Framework;
using System.Linq;
using Morabaraba9001.Data;
using System.Collections.Generic;
using NSubstitute;
using static Morabaraba9001.Display.Board;
using static Morabaraba9001.Data.Position;

namespace Morabaraba9001.Test //These tests apply to the QuickPlay Feature as our mock board.
{
    [TestFixture]
    public class Tests
    {
        #region Placing
        [Test]
        public void EmptyBoardOnGameStart()
        {           
            
        }
        [Test]
        public void PlayerWithDarkCowsPlaysFirst() //current is our equivalent of the player with dark pieces(dark colour being red)
        {
            GameState colour = new GameState();
            Assert.That(colour.current.playerColor == ConsoleColor.Red);
        }   
        [Test]
        public void CowsCanOnlyBePlacedOnEmptySpaces()
        {
        }
        [Test]
        public void MaximumOf12PlacementsPerPlayer()
        {
            GameState placement = new GameState();
            Assert.That(placement.current.Cows.Count <= 12);
            Assert.That(placement.opponent.Cows.Count <= 12);
        }
        [Test]
        public void CowsCannotBeMovedDuringPlacement()
        {
        }
        #endregion
        
        #region Moving
        [Test]
        public void CowCanOnlyMoveToAdjacentSpace()
        {
        }
        [Test]
        public void CowCanOnlyMoveOnEmptySpace()
        {
        }
        [Test]
        public void MovingDoesNotIncreaseOrDecreaseNumberOfCowsOnTheBoard()
        {
        }
        #endregion
        
        #region Flying
        [Test]
        public void CowsCanOnlyMoveToEmptySpaceIfThreeOfThoseCowsRemain()
        {
        }
        #endregion
        
        #region In General
        [Test]
        public void MillFormedByThreeOfTheSameColourCowInALine()
        {
            //Arrange
            Position[] incompleteMills = new Position[] { A7, D7, C5, D5, E4, F4, B2, D2 };
            Player testPlay = new Player();
            testPlay.Cows = incompleteMills.ToList();
            List<Position[]> mill1 = new List<Position[]>() { new Position[] { A7, D7, G7 } }.ToList();
            List<Position[]> mill2 = new List<Position[]>() { new Position[] { C5, D5, E5 } }.ToList();
            List<Position[]> mill3 = new List<Position[]>() { new Position[] { E4, F4, G4 } }.ToList();
            List<Position[]> mill4 = new List<Position[]>() { new Position[] { B2, D2, F2 } }.ToList();

            //Act
            var test1 = testPlay.GetMills(G7).ToList()[0].ToList();
            var test2 = testPlay.GetMills(E5).ToList()[0].ToList();
            var test3 = testPlay.GetMills(G4).ToList()[0].ToList();
            var test4 = testPlay.GetMills(F2).ToList()[0].ToList();

            //Assert
            Assert.That(test1[0] == mill1[0][0] && test1[1] == mill1[0][1] && test1[2] == mill1[0][2]);
            Assert.That(test2[0] == mill2[0][0] && test2[1] == mill2[0][1] && test2[2] == mill2[0][2]);
            Assert.That(test3[0] == mill3[0][0] && test3[1] == mill3[0][1] && test3[2] == mill3[0][2]);
            Assert.That(test4[0] == mill4[0][0] && test4[1] == mill4[0][1] && test4[2] == mill4[0][2]);
        }
        [Test]
        public void MillNotFormedByDifferentColouredCowsInALine()
        {
            //Arrange
            Position[] player1Cows = new Position[] { A7, C5, E4, B2 };
            Position[] player2Cows = new Position[] { D7, D5, F4, D2 };
            Player testPlayer1 = new Player();
            testPlayer1.Cows = player1Cows.ToList();
            Player testPlayer2 = new Player();
            testPlayer2.Cows = player2Cows.ToList();

            List<Position[]> mill1 = new List<Position[]>() { new Position[] { A7, D7, G7 } };
            List<Position[]> mill2 = new List<Position[]>() { new Position[] { C5, D5, E5 } };
            List<Position[]> mill3 = new List<Position[]>() { new Position[] { E4, F4, G4 } };
            List<Position[]> mill4 = new List<Position[]>() { new Position[] { B2, D2, F2 } };

            //Act
            var test1 = testPlayer1.GetMills(G7);
            var test2 = testPlayer1.GetMills(E5);
            var test3 = testPlayer1.GetMills(G4);
            var test4 = testPlayer1.GetMills(F2);

            //Assert
            Assert.That(test1.Count() == 0);
            Assert.That(test2.Count() == 0);
            Assert.That(test3.Count() == 0);
            Assert.That(test4.Count() == 0);
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
        public void CanOnlyShootOnTurnThatMillIsCompleted()//come back to this
        {
            //Arrange
            Position[] playerCows = new Position[] { A7, C5, E4, B2 };
            Player testPlay = new Player();
            testPlay.Cows = playerCows.ToList();
            List<Position[]> mill1 = new List<Position[]>() { new Position[] { A7, D7, G7 } }.ToList();
            List<Position[]> mill2 = new List<Position[]>() { new Position[] { C5, D5, E5 } }.ToList();
            List<Position[]> mill3 = new List<Position[]>() { new Position[] { E4, F4, G4 } }.ToList();
            List<Position[]> mill4 = new List<Position[]>() { new Position[] { B2, D2, F2 } }.ToList();

            //Act

            //Assert

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
