using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleBattleships;
using static SimpleBattleships.Battleship;

namespace BattleshipsUnitTests
{
    [TestClass]
    public class BattleshipUnitTest
    {
        [TestMethod]
        public void TryLoadBattleshipToTheBattlefield_When_PassBattlefieldAndProoperCoordinates_Then_ShipIsPresentOnReturnedBattlefieldWithProperCoordinatesAndFieldState()
        {
            IBattleField battlefield = new Battlefield(10);
            Battleship battleship = new Battleship(5);
            bool answer;
            (battlefield,answer) = battleship.TryLoadBattleshipToTheBattlefield(battlefield, 2, 2, Direction.Rigth);

            int expectedId = 1;
            bool expectedAnswer = true;
            FieldState expectedFieldState = FieldState.Ship;

            Assert.AreEqual(expectedAnswer, answer);
            Assert.AreEqual(expectedId,battlefield.GetArray()[2, 2].IdOfTheShip);
            Assert.AreEqual(expectedFieldState,battlefield.GetArray()[2, 2].State);
            Assert.AreEqual(expectedId, battlefield.GetArray()[3, 2].IdOfTheShip);
            Assert.AreEqual(expectedFieldState, battlefield.GetArray()[3, 2].State);
            Assert.AreEqual(expectedId, battlefield.GetArray()[4, 2].IdOfTheShip);
            Assert.AreEqual(expectedFieldState, battlefield.GetArray()[4, 2].State);
            Assert.AreEqual(expectedId, battlefield.GetArray()[5, 2].IdOfTheShip);
            Assert.AreEqual(expectedFieldState, battlefield.GetArray()[5, 2].State);
            Assert.AreEqual(expectedId, battlefield.GetArray()[6, 2].IdOfTheShip);
            Assert.AreEqual(expectedFieldState, battlefield.GetArray()[6, 2].State);
        }

        [TestMethod]
        public void TryLoadBattleshipToTheBattlefield_When_TryLoadShipOutOfTheMap_Then_RetutnBattleFileAsNullAndIsSucceedWithFalse()
        {
            IBattleField battlefield = new Battlefield(2);
            Battleship battleship = new Battleship(5);
            bool expectedResponse = false;
            bool isSucceed;
            (battlefield, isSucceed) = battleship.TryLoadBattleshipToTheBattlefield(battlefield, 5, 5, Direction.Rigth);
            Assert.AreEqual(expectedResponse, isSucceed);
        }

        [TestMethod]
        public void TryLoadBattleshipToTheBattlefield_When_TryLoadLoadShipToTheMaOnTheCollisionDirection_Then_RetutnBattleFileAsNullAndIsSucceedWithFalse()
        {
            IBattleField battlefield = new Battlefield(10);

            Battleship battleship = new Battleship(5);
            Battleship secondBattleship = new Battleship(5);

            bool isSucceed;
            bool ecpectedAnswer = false;

            (battlefield, isSucceed) = battleship.TryLoadBattleshipToTheBattlefield(battlefield, 2, 2, Direction.Rigth);
            (battlefield, isSucceed) = secondBattleship.TryLoadBattleshipToTheBattlefield(battlefield, 3, 1, Direction.Down);

            Assert.AreEqual(ecpectedAnswer, isSucceed);

        }


        [TestMethod]
        public void BattlshipConstructor_When_PassSize_ThenReturnShipWithTisSameSize()
        {
            Battleship battleship = new Battleship(5);
            int expectedSizeValue = 5;
            int actualSizeValue = battleship.Size;

            Assert.AreEqual(expectedSizeValue,actualSizeValue);
        }

        [TestMethod]
        public void TryLoadBattleshipToTheBattlefield_When_SetCorrectPositionInEveryDirection_Then_BattleshipHaveProperCooridnatesAndReturnTrue()
        {
            IBattleField battlefield = new Battlefield(10);
            Battleship battleship = new Battleship(5);

            bool isSucceed;
            (battlefield,isSucceed)= battleship.TryLoadBattleshipToTheBattlefield(battlefield, 2, 2, Direction.Rigth);

            Assert.AreEqual(true, isSucceed);
            Assert.AreEqual(battleship.CoordinatesOfTheShip[0].X, 2);
            Assert.AreEqual(battleship.CoordinatesOfTheShip[0].Y, 2);
            Assert.AreEqual(battleship.CoordinatesOfTheShip[1].X, 3);
            Assert.AreEqual(battleship.CoordinatesOfTheShip[1].Y, 2);
            Assert.AreEqual(battleship.CoordinatesOfTheShip[2].X, 4);
            Assert.AreEqual(battleship.CoordinatesOfTheShip[2].Y, 2);
            Assert.AreEqual(battleship.CoordinatesOfTheShip[3].X, 5);
            Assert.AreEqual(battleship.CoordinatesOfTheShip[3].Y, 2);
            Assert.AreEqual(battleship.CoordinatesOfTheShip[4].X, 6);
            Assert.AreEqual(battleship.CoordinatesOfTheShip[4].Y, 2);


            battlefield = new Battlefield(10);
            battleship = new Battleship(5);
            isSucceed = false;

            (battlefield, isSucceed) = battleship.TryLoadBattleshipToTheBattlefield(battlefield, 2, 2, Direction.Down);

            Assert.AreEqual(true, isSucceed);
            Assert.AreEqual(battleship.CoordinatesOfTheShip[0].X, 2);
            Assert.AreEqual(battleship.CoordinatesOfTheShip[0].Y, 2);
            Assert.AreEqual(battleship.CoordinatesOfTheShip[1].X, 2);
            Assert.AreEqual(battleship.CoordinatesOfTheShip[1].Y, 3);
            Assert.AreEqual(battleship.CoordinatesOfTheShip[2].X, 2);
            Assert.AreEqual(battleship.CoordinatesOfTheShip[2].Y, 4);
            Assert.AreEqual(battleship.CoordinatesOfTheShip[3].X, 2);
            Assert.AreEqual(battleship.CoordinatesOfTheShip[3].Y, 5);
            Assert.AreEqual(battleship.CoordinatesOfTheShip[4].X, 2);
            Assert.AreEqual(battleship.CoordinatesOfTheShip[4].Y, 6);


            battlefield = new Battlefield(10);
            battleship = new Battleship(5);
            isSucceed = false;
            (battlefield, isSucceed) = battleship.TryLoadBattleshipToTheBattlefield(battlefield, 8, 8, Direction.Left);

            Assert.AreEqual(true, isSucceed);
            Assert.AreEqual(battleship.CoordinatesOfTheShip[0].X, 8);
            Assert.AreEqual(battleship.CoordinatesOfTheShip[0].Y, 8);
            Assert.AreEqual(battleship.CoordinatesOfTheShip[1].X, 7);
            Assert.AreEqual(battleship.CoordinatesOfTheShip[1].Y, 8);
            Assert.AreEqual(battleship.CoordinatesOfTheShip[2].X, 6);
            Assert.AreEqual(battleship.CoordinatesOfTheShip[2].Y, 8);
            Assert.AreEqual(battleship.CoordinatesOfTheShip[3].X, 5);
            Assert.AreEqual(battleship.CoordinatesOfTheShip[3].Y, 8);
            Assert.AreEqual(battleship.CoordinatesOfTheShip[4].X, 4);
            Assert.AreEqual(battleship.CoordinatesOfTheShip[4].Y, 8);


            battlefield = new Battlefield(10);
            battleship = new Battleship(5);
            isSucceed = false;
            (battlefield, isSucceed) = battleship.TryLoadBattleshipToTheBattlefield(battlefield, 8, 8, Direction.Up);

            Assert.AreEqual(true, isSucceed);
            Assert.AreEqual(battleship.CoordinatesOfTheShip[0].X, 8);
            Assert.AreEqual(battleship.CoordinatesOfTheShip[0].Y, 8);
            Assert.AreEqual(battleship.CoordinatesOfTheShip[1].X, 8);
            Assert.AreEqual(battleship.CoordinatesOfTheShip[1].Y, 7);
            Assert.AreEqual(battleship.CoordinatesOfTheShip[2].X, 8);
            Assert.AreEqual(battleship.CoordinatesOfTheShip[2].Y, 6);
            Assert.AreEqual(battleship.CoordinatesOfTheShip[3].X, 8);
            Assert.AreEqual(battleship.CoordinatesOfTheShip[3].Y, 5);
            Assert.AreEqual(battleship.CoordinatesOfTheShip[4].X, 8);
            Assert.AreEqual(battleship.CoordinatesOfTheShip[4].Y, 4);
        }

        [TestMethod]
        public void TryLoadBattleshipToTheBattlefield_When_PassPositionExceedingTheMapBorder_Then_ReturnFalse()
        {
            IBattleField battlefield = new Battlefield(10);
            Battleship battleship = new Battleship(5);
            bool isSucceed;
            
            (battlefield,isSucceed) = battleship.TryLoadBattleshipToTheBattlefield(battlefield, 8, 2, Direction.Rigth);
            Assert.AreEqual(false, isSucceed);

            battlefield = new Battlefield(10);
            battleship = new Battleship(5);
            isSucceed = false;

            (battlefield, isSucceed) = battleship.TryLoadBattleshipToTheBattlefield(battlefield, 8, 8, Direction.Down);
            Assert.AreEqual(false, isSucceed);

            battlefield = new Battlefield(10);
            battleship = new Battleship(5);
            isSucceed = false;

            (battlefield, isSucceed) = battleship.TryLoadBattleshipToTheBattlefield(battlefield, 0, 8, Direction.Left);
            Assert.AreEqual(false, isSucceed);

            battlefield = new Battlefield(10);
            battleship = new Battleship(5);
            isSucceed = false;

            (battlefield, isSucceed) = battleship.TryLoadBattleshipToTheBattlefield(battlefield, 0, 0, Direction.Up);
            Assert.AreEqual(false, isSucceed);

            battlefield = new Battlefield(10);
            battleship = new Battleship(5);
            isSucceed = false;

            (battlefield, isSucceed) = battleship.TryLoadBattleshipToTheBattlefield(battlefield, 20, 20, Direction.Rigth);
            Assert.AreEqual(false, isSucceed);
        }

        [TestMethod]
        public void DestroyMast_When_DestroyAllFiveTimes_Then_BattleshipResponseIsSunk()
        {
            IBattleField battlefield = new Battlefield(10);
            Battleship battleship = new Battleship(5);
            battleship.TryLoadBattleshipToTheBattlefield(battlefield, 2, 2, Direction.Rigth);

            ShipResponse shipResponse = battleship.DestroyMast(3, 2);

            Assert.AreEqual(shipResponse, ShipResponse.Hit);

            battleship = new Battleship(5);
            battleship.TryLoadBattleshipToTheBattlefield(battlefield, 2, 2, Direction.Rigth);
            shipResponse = battleship.DestroyMast(2, 5);
            Assert.AreEqual(shipResponse, ShipResponse.Miss);

            shipResponse = battleship.DestroyMast(20, 20);
            Assert.AreEqual(shipResponse, ShipResponse.Miss);

            shipResponse = battleship.DestroyMast(2, 2);
            Assert.AreEqual(shipResponse, ShipResponse.Hit);
            shipResponse = battleship.DestroyMast(3, 2);
            Assert.AreEqual(shipResponse, ShipResponse.Hit);
            shipResponse = battleship.DestroyMast(4, 2);
            Assert.AreEqual(shipResponse, ShipResponse.Hit);
            shipResponse = battleship.DestroyMast(5, 2);
            Assert.AreEqual(shipResponse, ShipResponse.Hit);
            shipResponse = battleship.DestroyMast(6, 2);

            Assert.AreEqual(shipResponse, ShipResponse.Sunk);

        }

        [TestMethod]
        public void DestroyMast_When_TryDestroyMastOutOfTheShip_Then_BattleshipResponseIsMiss()
        {
            IBattleField battlefield = new Battlefield(10);
            Battleship battleship = new Battleship(5);
            battleship.TryLoadBattleshipToTheBattlefield(battlefield, 2, 2, Direction.Rigth);

            ShipResponse shipResponse = battleship.DestroyMast(3, 2);

            Assert.AreEqual(shipResponse, ShipResponse.Hit);

            battleship = new Battleship(5);
            battleship.TryLoadBattleshipToTheBattlefield(battlefield, 2, 2, Direction.Rigth);
            shipResponse = battleship.DestroyMast(2, 5);
            Assert.AreEqual(shipResponse, ShipResponse.Miss);

            shipResponse = battleship.DestroyMast(20, 20);
            Assert.AreEqual(shipResponse, ShipResponse.Miss);
        }

        [TestMethod]
        public void IsShipAlive_When_OneMastIsDestroyed_Then_ResponseIsTrue()
        {
            IBattleField battlefield = new Battlefield(10);
            Battleship battleship = new Battleship(5);
            battleship.TryLoadBattleshipToTheBattlefield(battlefield, 2, 2, Direction.Rigth);

            battleship.DestroyMast(2, 2);

            bool expectedResponse = true;
            bool actualResponse = battleship.IsTheShipAlive();

            Assert.AreEqual(expectedResponse, actualResponse);
        }

        [TestMethod]
        public void IsShipAlive_When_AllMastIsDestroyed_Then_ResponseIsFalse()
        {
            IBattleField battlefield = new Battlefield(10);
            Battleship battleship = new Battleship(5);
            battleship.TryLoadBattleshipToTheBattlefield(battlefield, 2, 2, Direction.Rigth);

            battleship.DestroyMast(2, 2);
            battleship.DestroyMast(3, 2);
            battleship.DestroyMast(4, 2);
            battleship.DestroyMast(5, 2);
            battleship.DestroyMast(6, 2);

            bool expectedResponse = false;
            bool actualResponse = battleship.IsTheShipAlive();

            Assert.AreEqual(expectedResponse, actualResponse);
        }
    }
}
