using System;
using NUnit.Framework;
using System.Linq;
using Morabaraba9001.Data;
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
