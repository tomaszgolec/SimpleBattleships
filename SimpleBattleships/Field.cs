using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBattleships
{
    public class Field : ICloneable
    {
        private int? idOfTheship;
        private FieldState state;

        public Field(int? idOfTheShip, FieldState fieldState)
        {
            this.IdOfTheShip = idOfTheShip;
            this.State = fieldState;
        }

        public Field()
        {
            this.IdOfTheShip = null;
            this.State = FieldState.Empty;
        }

        public int? IdOfTheShip 
        {
            get { return idOfTheship; }
            private set { this.idOfTheship = value; }
        }

        public FieldState State
        {
            get { return state; }
            set { this.state = value; }
        }

        public object Clone()
        {
            Field newField = new Field(this.IdOfTheShip, this.state);
            return newField;
        }
    }
}
