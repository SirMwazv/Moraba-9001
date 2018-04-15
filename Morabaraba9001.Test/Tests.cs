using System;
using NUnit.Framework;
using System.Linq;
using Morabaraba9001.Data;

namespace Morabaraba9001.Test
{
    [TestFixture]
    public class Tests
    {
        #region Placing
        [Test]
        public void EmptyBoardOnGameStart()
        {
            GameManager g;
        }
        [Test]
        public void PlayerWithDarkCowsPlaysFirst()
        {
        }
        [Test]
        public void CowsCanOnlyBePlacedOnEmptySpaces()
        {
        }
        [Test]
        public void MaximumOf12PlacementsPerPlayer()
        {
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
