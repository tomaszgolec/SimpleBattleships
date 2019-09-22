using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleBattleships;

namespace BattleshipsUnitTests
{
    [TestClass]
    public class BattlefieldUnitTests
    {
        [TestMethod]
        public void BattleFieldConstructor_When_PassTheSize_Then_CreateBattlefieldObjectWithArrayOfSameSizeAndEmptyFields()
        {
            Battlefield battlefield = new Battlefield(10);
            int expectedSize = 10;
            FieldState expectedFieldState = FieldState.Empty;
            int actualXSize = battlefield.GetArray().GetLength(0);
            int actualYSize = battlefield.GetArray().GetLength(1);

            
            Assert.AreEqual(expectedSize, actualXSize);
            Assert.AreEqual(expectedSize, actualYSize);
            for(int i = 0; i < actualXSize; i++)
            {
                for(int j = 0; j < actualYSize; j++)
                {
                    Assert.AreEqual(expectedFieldState, battlefield.GetArray()[i, j].State);
                }
            }
            
            
        }

        [TestMethod]
        public void AddMAstOfTheShip_When_PassIdOfShipAndCoordinates_Then_FunctionReturnNewBattlefieldWithAddedMast()
        {
            IBattleField battlefield = new Battlefield(10);
            int IdOfTheShip = 1;
            int xPostionOfTheMast = 5;
            int yPostionOfTheMast = 6;
             
            int? expectedIdOfTheShip = 1;
            FieldState expectedFieldState = FieldState.Ship;

            battlefield = battlefield.AddMastOfTheShip(IdOfTheShip, xPostionOfTheMast, yPostionOfTheMast);

            int? actutalIdOfTheShip = battlefield.GetArray()[xPostionOfTheMast, yPostionOfTheMast].IdOfTheShip;
            FieldState actualFieldState = battlefield.GetArray()[xPostionOfTheMast, yPostionOfTheMast].State;

            Assert.AreEqual(expectedIdOfTheShip, actutalIdOfTheShip);
            Assert.AreEqual(FieldState.Ship, actualFieldState);

        }

        [TestMethod]
        [ExpectedException(typeof(System.IndexOutOfRangeException), "The coordinates are out of the battlfield")]
        public void AddMAstOfTheShip_When_TryAddMastOutOfTheMap_Then_ThrowIndexOutOfRangeException()
        {
            Battlefield battlefield = new Battlefield(10);
            int IdOfTheShip = 1;
            int xPostionOfTheMast = 20;
            int yPostionOfTheMast = 20;

            battlefield.AddMastOfTheShip(IdOfTheShip, xPostionOfTheMast, yPostionOfTheMast);
        }

        [TestMethod]
        public void Shoot_When_CoordinatesAreTheSameLikeTheMas_Then_FunctionReturnsIdOfTheShip()
        {
            IBattleField battlefield = new Battlefield(10);

            battlefield = battlefield.AddMastOfTheShip(1, 5, 5);

            int? idOfTheShip = battlefield.Shoot(5, 5);
            int? expectedId = 1;

            Assert.AreEqual(idOfTheShip, expectedId);
        }


        [TestMethod]
        public void Shoot_When_CoordinatesAreOnTheEmptyField_Then_Return_IdOfTheShipLikeNull()
        {
            Battlefield battlefield = new Battlefield(10);

            battlefield.AddMastOfTheShip(1, 5, 5);

            int? idOfTheShip = battlefield.Shoot(6, 5);
            int? expectedId = null;

            Assert.AreEqual(idOfTheShip, expectedId);
        }

        [TestMethod]
        [ExpectedException(typeof(System.IndexOutOfRangeException), "The shoot is out of the map")]
        public void Shoot_When_TryShootOutOfTheMap_Then_ReturnIndexOutOfRangeException()
        {
            Battlefield battlefield = new Battlefield(10);
            battlefield.Shoot(20, 5);
        }

        [TestMethod]
        public void Clone_When_RunFunction_Then_ReturnNewCopyOfBattlefield()
        {
            Battlefield battlefield = new Battlefield(10);
            Battlefield newBattlefield = (Battlefield)battlefield.Clone();
            newBattlefield = (Battlefield)newBattlefield.AddMastOfTheShip(1, 2, 2);
            Assert.AreNotEqual(battlefield.GetArray()[2, 2].IdOfTheShip, newBattlefield.GetArray()[2, 2].IdOfTheShip);
            Assert.AreNotEqual(battlefield.GetArray()[2, 2].State, newBattlefield.GetArray()[2, 2].State);
        }
    }
}
