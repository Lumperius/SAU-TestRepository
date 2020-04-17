using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork_3
{
    class Carriage : TrainElement
    {
        protected int _humanCapacity;
        protected int _baggageCapacity;

        public int GetHumanCapacity() => _humanCapacity;
        public int GetBaggageCapacity() => _baggageCapacity;

        public Carriage(int mass, int humanCapacity, int baggageCapacity) : base(mass)
        {
            _humanCapacity = humanCapacity;
            _baggageCapacity = baggageCapacity;
        }
    }
}
