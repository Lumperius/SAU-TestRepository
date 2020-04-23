using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork_3
{
    abstract class Carriage : TrainElement, iInformative
    {
        public int _humanCapacity;
        protected int _baggageCapacity;


        public int GetHumanCapacity() => _humanCapacity;
        public int GetBaggageCapacity() => _baggageCapacity;

        public override abstract void GetInfo(int carNumber);

        public Carriage(int mass, int humanCapacity, int baggageCapacity) : base(mass)
        {
            _humanCapacity = humanCapacity;
            _baggageCapacity = baggageCapacity;
        }
    }
}
