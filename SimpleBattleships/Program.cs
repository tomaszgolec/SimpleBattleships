using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using static SimpleBattleships.Battleship;

namespace SimpleBattleships
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

            IBattleField battlefield = new Battlefield(10);
            List<Battleship> allShips = new List<Battleship>();
            List<Battleship> battleships = new List<Battleship>();
            List<Battleship> destroyers = new List<Battleship>();

            (battlefield, battleships) = CreateAndLoadBattleshipsToBattlefield(battlefield);
            (battlefield, destroyers) = CreateAndLoadDestroyersToBattlefield(battlefield);

            allShips.AddRange(battleships);
            allShips.AddRange(destroyers);

            do
            {
                Console.Clear();
                Console.WriteLine(battlefield.ReturnBattlefieldView());

                (int x, int y) = GetCoorinatesFromUser(battlefield);

                int? idOfTheShip = battlefield.Shoot(x, y);
                if (idOfTheShip != null)
                {
                    var ship = allShips.Where(a => a.Id == idOfTheShip).FirstOrDefault();
                    Battleship.ShipResponse response = ship.DestroyMast(x, y);
                    switch (response)
                    {
                        case Battleship.ShipResponse.Hit:
                            Console.WriteLine("Hit");
                            battlefield.GetArray()[x, y].State = FieldState.ShipShooted;
                            break;
                        case Battleship.ShipResponse.Miss:
                            Console.WriteLine("Miss");
                            battlefield.GetArray()[x, y].State = FieldState.EmptyShooted;
                            break;
                        case Battleship.ShipResponse.Sunk:
                            Console.WriteLine("Sunk");
                            battlefield.GetArray()[x, y].State = FieldState.ShipShooted;
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Miss");
                    battlefield.GetArray()[x, y].State = FieldState.EmptyShooted;
                }

                Thread.Sleep(700);

            } while (allShips.Any(a => a.IsTheShipAlive()));

            Console.WriteLine("You win !");
            Console.ReadKey();
            }
            catch(Exception ex)
            {
                Console.WriteLine("An unexpected problem has occurred.");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// This function create list of battleships with random positions and add them to the battlfields.
        /// </summary>
        /// <param name="battlefield"></param>
        /// <returns>New battlefield object with added destroyers and also list of the ships.</returns>

        public static (IBattleField battlefield, List<Battleship> ships) CreateAndLoadBattleshipsToBattlefield(IBattleField battlefield)
        {
            List<Battleship> battleships = new List<Battleship>();
            Battleship battleship = new Battleship(5);
            (int x, int y, Direction direction) coordinates;
            IBattleField NewBattleField = (IBattleField)battlefield.Clone();
            bool isSucceed;
            do
            {
                coordinates = RandomInitialCoordinates();
                IBattleField battlefieldWihtNewShips;
                (battlefieldWihtNewShips, isSucceed) = battleship.TryLoadBattleshipToTheBattlefield(NewBattleField, coordinates.x, coordinates.y, coordinates.direction);
                if (isSucceed == true)
                    NewBattleField = battlefieldWihtNewShips;

            } while (isSucceed == false);

            battleships.Add(battleship);
            return (battlefield: NewBattleField,  ships : battleships);
        }

        /// <summary>
        /// This function create list of destroyers with random positions and add them to the battlfield
        /// </summary>
        /// <param name="battlefield"></param>
        /// <returns>New battlefield object with added destroyers and also list of the ships</returns>
        public static (IBattleField battlefield, List<Battleship> destroyers) CreateAndLoadDestroyersToBattlefield(IBattleField battlefield)
        {
            List<Battleship> destroyers = new List<Battleship>();
            IBattleField newBattlefield = (IBattleField)battlefield.Clone();
            (int x, int y, Direction direction) coordinates;
            bool isSucceed;
            for (int i = 1; i <= 2; i++)
            {
                Battleship destroyer = new Battleship(4);
                do
                {
                    coordinates = RandomInitialCoordinates();
                    IBattleField battlefieldWihtNewShips;
                    (battlefieldWihtNewShips, isSucceed) = destroyer.TryLoadBattleshipToTheBattlefield(newBattlefield, coordinates.x, coordinates.y, coordinates.direction);
                    if (isSucceed == true)
                        newBattlefield = battlefieldWihtNewShips;
                } while (isSucceed == false);

                destroyers.Add(destroyer);
            }
            return (battlefield: newBattlefield, destroyers: destroyers);
        }

        /// <summary>
        /// This function get coordinates from the user.
        /// </summary>
        /// <returns>Two coordinates x and y</returns>
        public static (int x, int y) GetCoorinatesFromUser(IBattleField battlefield )
        {
            Console.Write("x:");
            int x;
            while (Int32.TryParse(Console.ReadLine(), out x) == false || battlefield.GetArray().GetLength(0) <= x || x < 0)
            {
                Console.WriteLine("The coordinate is wrong. You should type a number from 0 to 9");
                Console.Write("x:");
            }

            Console.Write("y:");
            int y;
            while (Int32.TryParse(Console.ReadLine(), out y) == false || battlefield.GetArray().GetLength(1) <= y || y < 0)
            {
                Console.WriteLine("The coordinate is wrong. You should type a number from 0 to 9");
                Console.Write("y:");
            }

            return ( x : x, y: y);
        }

        /// <summary>
        /// This function radnom initial coordinate for the ship (x and y) and direction of the ship
        /// </summary>
        /// <returns>Intital coordinates x,y and initial direction</returns>
        public static (int x, int y, Direction direction) RandomInitialCoordinates()
        {
            int initX;
            int initY;
            int direction;
            Random rand = new Random();
            initX = rand.Next(0, 9);
            initY = rand.Next(0, 9);
            direction = rand.Next(1, 4);
            return (x: initX, y: initY, direction: (Direction)direction);
        }

    }
}
