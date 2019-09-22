using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBattleships
{
    public class Battlefield : IBattleField, ICloneable
    {

        private Field[,] Array;

        /// <summary>
        /// Is creating new square battfield
        /// </summary>
        /// <param name="battlefieldSize"></param>
        public Battlefield(int battlefieldSize)
        {
            this.Array = new Field[battlefieldSize, battlefieldSize];
            for (int i = 0; i < battlefieldSize; i++)
            {
                for (int j = 0; j < battlefieldSize; j++)
                {
                    Array[i, j] = new Field();
                }
            }
        }
        /// <summary>
        /// Is getting array of the feields
        /// </summary>
        /// <returns>array of fields</returns>
        public Field[,] GetArray()
        {
            return this.Array;
        }

        /// <summary>
        /// Is adding mast of the ship to the battlefield
        /// </summary>
        /// <param name="idOfTheShip"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>Returns new battlefield object with added mast</returns>
        public IBattleField AddMastOfTheShip(int idOfTheShip, int x, int y)
        {
            Battlefield newBattlefield = (Battlefield)this.Clone();
            if (newBattlefield.Array[x, y] != null)
            {
                newBattlefield.Array[x, y] = new Field(idOfTheShip, FieldState.Ship);
            }
            else
            {
                throw new IndexOutOfRangeException("The coordinates are out of the battlfield");
            }

            return newBattlefield;
        }
        /// <summary>
        /// The function generates view based fields array
        /// </summary>
        /// <returns>Returns string with fields represenation</returns>
        public string ReturnBattlefieldView()
        {
            string view = string.Empty;

            for (int i = 0; i < this.Array.GetLength(0); i++)
            {
                if (i == 0)
                {
                    if (i == 0)
                    {
                        view += "  ";
                        for (int k = 0; k < this.Array.GetLength(0); k++)
                        {
                            view += k.ToString() + " ";
                        }
                        view += "\n";
                    }

                }


                for (int j = 0; j < this.Array.GetLength(1); j++)
                {
                    if (j == 0)
                        view += i.ToString();


                    if (Array[j, i].State == FieldState.Empty || Array[j, i].State == FieldState.Ship)
                    {

                        view += "|_";
                    }
                    else if (Array[j, i].State == FieldState.EmptyShooted)
                    {
                        view += "|o";
                    }
                    else if (Array[j, i].State == FieldState.ShipShooted)
                    {
                        view += "|X";
                    }
                }
                view += "\n";
            }

            return view;
        }
        /// <summary>
        /// The function mark on the battlefields shooted fields
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>If on the field was a ship then it returns id of the ship</returns>
        public int? Shoot(int x, int y)
        {
            if (this.Array[x, y] != null)
            {
                if (this.Array[x, y].State != FieldState.Empty)
                    return this.Array[x, y].IdOfTheShip;
                else
                    return null;
            }
            else
            {
                throw new IndexOutOfRangeException("The shoot is out of the map");
            }
        }

        /// <summary>
        /// The function is copying the instance of the class
        /// </summary>
        /// <returns>New copy of object</returns>
        public object Clone()
        {
            Battlefield newBattlefield = new Battlefield(this.Array.GetLength(0));
            for(int i=0; i < this.Array.GetLength(0); i++)
            {
                for(int j=0; j <this.Array.GetLength(1); j++)
                {
                    newBattlefield.Array[i, j] = (Field)this.Array[i, j].Clone();
                }
            }

            return newBattlefield;
        }
    }
}
