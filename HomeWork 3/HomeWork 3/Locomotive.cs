using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork_3
{
    class Locomotive : TrainElement
    {
        private int _enginePower; 
        public int GetEnginePower() => _enginePower;
        public Locomotive(int mass, int enginePower) : base (mass) 
        {
            _enginePower = enginePower;
        }
    }
}
