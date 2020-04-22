using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork_3
{
    class EconomyClassSleeper : Carriage, iInformative
    {
        new public const byte ComfortLevel = 1;
        public EconomyClassSleeper(int mass, int humanCapacity, int baggageCapacity) : base(mass, humanCapacity, baggageCapacity)
        {
        }

        new public void GetInfo(int carNumber)
        {
            Console.WriteLine("The car №" + carNumber + " is economy class sleeper.");
            Console.WriteLine("It's capacity is " + _humanCapacity + " people and " + _baggageCapacity + " kilogramms of baggage.");
        }
    }
}
