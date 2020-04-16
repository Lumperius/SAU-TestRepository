using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork_3
{
    class TrainElement

    {
        private int _humanCapacity;
        private int _baggageCapacity;

        public int GetHumanCapacity() => _humanCapacity;
        public int GetBaggageCapacity() => _baggageCapacity;

        public TrainElement(int humanCapacity, int baggageCapacity)
        {
            _humanCapacity = humanCapacity;
            _baggageCapacity = baggageCapacity;
        }

    }
}

