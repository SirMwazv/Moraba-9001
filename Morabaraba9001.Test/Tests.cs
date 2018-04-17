using System;
using NUnit.Framework;
using System.Linq;
using Morabaraba9001.Data;
using System.Collections.Generic;
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
            Position[] incompleteMills = new Position[] { A7, D7, C5, D5, E4, F4, B2, D2 };
            Player testPlay = new Player("test", ConsoleColor.Black);
            testPlay.Cows = incompleteMills.ToList();

            List<Position[]> mill1 = new List<Position[]>() { new Position[] { A7, D7, G7 } };
            List<Position[]> mill2 = new List<Position[]>() { new Position[] { C5, D5, E5 } };
            List<Position[]> mill3 = new List<Position[]>() { new Position[] { E4, F4, G4 } };
            List<Position[]> mill4 = new List<Position[]>() { new Position[] { B2, D2, F2 } };

            var test1 = testPlay.GetMills(G7);
            var test2 = testPlay.GetMills(E5);
            var test3 = testPlay.GetMills(G4);
            var test4 = testPlay.GetMills(F2);

            Assert.That(test1 == mill1);
            Assert.That(test2 == mill2);
            Assert.That(test3 == mill3);
            Assert.That(test4 == mill4);
        }
        [Test]
        public void MillNotFormedByDifferentColouredCowsInALine()
        {
        }
        [Test]
        public void ConnectedSpacesWithCowsDoNotFormALine()
        {
        }
        [Test]
        public void CanOnlyShootOnTurnThatMillIsCompleted()
        {
        }
        [Test]
        public void CannotShootACowInAMillWhenNonMillCowsExist()
        {
        }
        [Test]
        public void CanShootACowInAMillWhenNonMillCowsDoNotExist()
        {
        }
        [Test]
        public void PlayerCannotShootOwnCows()
        {
        }
        #endregion
     


    }
}
