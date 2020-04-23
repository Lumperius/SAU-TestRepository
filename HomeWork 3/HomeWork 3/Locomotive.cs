using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork_3
{
    class Locomotive : TrainElement, iInformative
    {
        private int _enginePower; 
        public int GetEnginePower() => _enginePower;
        public Locomotive(int mass, int enginePower) : base (mass) 
        {
            _enginePower = enginePower;
            comfortLevel = 1;
        }

        public override void GetInfo(int carNumber)
        {
            Console.WriteLine("The car №" + carNumber + " locomotive.");
            Console.WriteLine("It's head car of the train, that moves it with power of " + _enginePower + " watt.");
        }
    }
}
