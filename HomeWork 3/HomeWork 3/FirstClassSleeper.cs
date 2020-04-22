using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork_3
{
    class FirstClassSleeper : Carriage, iInformative
    {
        new public const byte ComfortLevel = 3;
        public FirstClassSleeper(int mass, int humanCpacity, int baggageCapacity) : base(mass, humanCpacity, baggageCapacity)
        {
        }

        new public void GetInfo(int carNumber)
        {
            Console.WriteLine("The car №" + carNumber + " is first class sleeper type.");
            Console.WriteLine("It's capacity is " + _humanCapacity + " people and " + _baggageCapacity + " kilogramms of baggage.");
        }
    }
}
