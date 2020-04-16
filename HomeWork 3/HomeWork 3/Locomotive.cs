using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork_3
{
    class Locomotive : TrainElement
    {
        private int _enginePower; 
        public int GetEnginePower() => _enginePower;
        Locomotive(int humanCapacity, int baggageCapacity, ) : base(humanCapacity, baggageCapacity)
        {

        }
    }
}
