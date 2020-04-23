using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork_3
{
    class FirstClassSleeper : Carriage, iInformative
    {
        public FirstClassSleeper(int mass, int humanCpacity, int baggageCapacity) : base(mass, humanCpacity, baggageCapacity)
        {
            comfortLevel = 3;
        }

        public override void GetInfo(int carNumber)
        {
            Console.WriteLine("The car №" + carNumber + " is first class sleeper type.");
            Console.WriteLine("It's capacity is " + _humanCapacity + " people and " + _baggageCapacity + " kilogramms of baggage.");
        }
    }
}
