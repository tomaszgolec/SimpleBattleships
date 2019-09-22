using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBattleships
{

    public interface IBattleField
    {
        Field[,] GetArray();
        IBattleField AddMastOfTheShip(int idOfTheShip, int x, int y);
        int? Shoot(int x, int y);
        Object Clone();
        string ReturnBattlefieldView();
    }
    
    public class Battleship
    {
        private List<Coordinates> coordinatesOfTheShip = new List<Coordinates>();
        private int id;
        private static int counter = 0;
        /// <summary>
        /// Create battleship with specific amount of masts
        /// </summary>
        /// <param name="sizeOfTheShip"></param>
        public Battleship(int sizeOfTheShip)
        {
            for (int i = 1; i <= sizeOfTheShip; i++)
            {
                this.coordinatesOfTheShip.Add(new Coordinates());
            }
            counter++;
            id = counter;
        }
        /// <summary>
        /// Returns how many mast has a battlship
        /// </summary>
        public int Size
        {
            get { return coordinatesOfTheShip.Count; }
        }
        public int Id
        {
            get { return this.id; }
        }
        /// <summary>
        /// Returns how many masts has a battlship
        /// </summary>
        public List<Coordinates> CoordinatesOfTheShip
        {
            get { return coordinatesOfTheShip; }
        }
        /// <summary>
        /// Function is checking coordinates correctness na is loading ship to the map.
        /// </summary>
        /// <param name="battlefield"></param>
        /// <param name="intitialXPostion"></param>
        /// <param name="initialYPosition"></param>
        /// <param name="directionOfTheShip"></param>
        /// <returns>Returns new battlefield with added ship and bool information about succeed ot the process</returns>
        public (IBattleField newBattleField, bool isSucceed) TryLoadBattleshipToTheBattlefield(IBattleField battlefield, int intitialXPostion, int initialYPosition, Direction directionOfTheShip)
        {
            bool isNoCollisions = this.TrySetCoordinatesForTheShip(battlefield, intitialXPostion, initialYPosition, directionOfTheShip);
            IBattleField newBattlefild = isNoCollisions ? this.LoadShipToBattlefield(battlefield) : null;
            return (newBattlefild , isSucceed : isNoCollisions);
        }

        /// <summary>
        /// The function try to set all coordinates of the masts. It will be possible only if is no collision with other ship on the map
        /// </summary>
        /// <param name="battlefield"></param>
        /// <param name="intitialXPostion"></param>
        /// <param name="initialYPosition"></param>
        /// <param name="directionOfTheShip"></param>
        /// <returns>Return true if opperation was succed and false when isn't</returns>
        public bool TrySetCoordinatesForTheShip(IBattleField battlefield, int intitialXPostion, int initialYPosition, Direction directionOfTheShip)
        {
            bool isNoCollisions = false;
            switch (directionOfTheShip)
            {
                case Direction.Rigth:
                    int distanceToRightBorder = battlefield.GetArray().GetLength(0) - intitialXPostion;
                    if (distanceToRightBorder >= this.Size)
                    {
                        List<Coordinates> TemporaryCoordinatesOfTheShip = new List<Coordinates>();
                        for (int i = 0; i < this.Size; i++)
                        {
                            TemporaryCoordinatesOfTheShip.Add(new Coordinates() { X = intitialXPostion + i, Y = initialYPosition });
                        }
                        if (IsNoColisionOnTHeMap(battlefield, TemporaryCoordinatesOfTheShip))
                        {
                            this.coordinatesOfTheShip = TemporaryCoordinatesOfTheShip;
                            isNoCollisions = true;
                        }
                        else
                        {
                            isNoCollisions = false;
                        }
                    }
                    else
                    {
                        isNoCollisions = false;
                    }
                    break;
                case Direction.Down:
                    int distanceToBottomBorder = battlefield.GetArray().GetLength(0) - initialYPosition;
                    if (distanceToBottomBorder >= this.Size)
                    {
                        List<Coordinates> TemporaryCoordinatesOfTheShip = new List<Coordinates>();
                        for (int i = 0; i < this.Size; i++)
                        {
                            TemporaryCoordinatesOfTheShip.Add(new Coordinates() { X = intitialXPostion, Y = initialYPosition + i });
                        }
                        if (IsNoColisionOnTHeMap(battlefield, TemporaryCoordinatesOfTheShip))
                        {
                            this.coordinatesOfTheShip = TemporaryCoordinatesOfTheShip;
                            isNoCollisions = true;
                        }
                    }
                    else
                    {
                        isNoCollisions = false;
                    }
                    break;
                case Direction.Left:
                    int distanceToLeftBorder = intitialXPostion + 1;
                    if (distanceToLeftBorder >= this.Size)
                    {
                        List<Coordinates> TemporaryCoordinatesOfTheShip = new List<Coordinates>();
                        for (int i = 0; i < this.Size; i++)
                        {
                            TemporaryCoordinatesOfTheShip.Add(new Coordinates() { X = intitialXPostion - i, Y = initialYPosition });
                        }
                        if (IsNoColisionOnTHeMap(battlefield, TemporaryCoordinatesOfTheShip))
                        {
                            this.coordinatesOfTheShip = TemporaryCoordinatesOfTheShip;
                            isNoCollisions = true;
                        }
                    }
                    else
                    {
                        isNoCollisions = false;
                    }
                    break;
                case Direction.Up:
                    int distanceToTopBorder = initialYPosition + 1;
                    if (distanceToTopBorder >= this.Size)
                    {
                        List<Coordinates> TemporaryCoordinatesOfTheShip = new List<Coordinates>();
                        for (int i = 0; i < this.Size; i++)
                        {
                            TemporaryCoordinatesOfTheShip.Add(new Coordinates() { X = intitialXPostion, Y = initialYPosition - i });
                        }
                        if (IsNoColisionOnTHeMap(battlefield, TemporaryCoordinatesOfTheShip))
                        {
                            this.coordinatesOfTheShip = TemporaryCoordinatesOfTheShip;
                            isNoCollisions = true;
                        }
                    }
                    else
                    {
                        isNoCollisions = false;
                    }
                    break;
            }

            return isNoCollisions;
        }


        /// <summary>
        /// The function load the battleship to the battlefield
        /// </summary>
        /// <param name="battlefield"></param>
        /// <returns>Returns new battlfield object with new battlship</returns>
        private IBattleField LoadShipToBattlefield(IBattleField battlefield)
        {
            IBattleField newBattleField = (IBattleField)battlefield.Clone();

            int xOfTheBattlefield = battlefield.GetArray().GetLength(0) - 1;
            int yOfTheBattlefield = battlefield.GetArray().GetLength(1) - 1;

            int? maxXcoordinate = this.CoordinatesOfTheShip.Max(a => a.X);
            int? maxYcoordinate = this.CoordinatesOfTheShip.Max(a => a.Y);

            int? minXcoordinate = this.CoordinatesOfTheShip.Min(a => a.X);
            int? minYcoordinate = this.CoordinatesOfTheShip.Min(a => a.Y);

            foreach (Coordinates mast in this.CoordinatesOfTheShip)
            {
                newBattleField = newBattleField.AddMastOfTheShip(this.id, Convert.ToInt32(mast.X), Convert.ToInt32(mast.Y));
            }

            return newBattleField;
        }

        /// <summary>
        /// The function is checking is no collision on the map with other ship.
        /// </summary>
        /// <param name="battlefield"></param>
        /// <param name="coordinatesForTheShip"></param>
        /// <returns></returns>
        private bool IsNoColisionOnTHeMap(IBattleField battlefield, List<Coordinates> coordinatesForTheShip)
        {
            foreach (Coordinates coordinates in coordinatesForTheShip)
            {
                FieldState fieldState = battlefield.GetArray()[Convert.ToInt32(coordinates.X), Convert.ToInt32(coordinates.Y)].State;
                if (fieldState != FieldState.Empty)
                    return false;
            }
            return true;
        }
        /// <summary>
        /// The function change state of the mast to Shooted
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>Returns whats happend after shoot, that can be Hit, Sunk or Miss</returns>
        public ShipResponse DestroyMast(int x, int y)
        {
            ShipResponse shipResponse;
            Coordinates hitedMast = coordinatesOfTheShip.Where(a => (a.X == x) && (a.Y == y)).FirstOrDefault();
            if (hitedMast != null)
            {
                hitedMast.shooted = true;
                if (this.CoordinatesOfTheShip.Any(a => a.shooted == false))
                    shipResponse = ShipResponse.Hit;
                else
                    shipResponse = ShipResponse.Sunk;
            }
            else
            {
                shipResponse = ShipResponse.Miss;
            }

            return shipResponse;
        }
        /// <summary>
        /// The function is checking is the shipe alive
        /// </summary>
        /// <returns>Returns true if is alive and false if isn't</returns>
        public bool IsTheShipAlive()
        {
            return this.CoordinatesOfTheShip.Any(a => a.shooted == false);
        }

        public class Coordinates
        {
            public int? X { get; set; }
            public int? Y { get; set; }
            public bool shooted = false;
        }

        public enum ShipResponse
        {
            Hit,
            Sunk,
            Miss
        }

        public enum Direction
        {
            Rigth = 1,
            Down = 2,
            Left = 3,
            Up = 4
        }

    }
   
}
