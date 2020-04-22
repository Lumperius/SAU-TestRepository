using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork_3
{
    class CompartmentSleeper : Carriage, iInformative
    {
        new public const byte ComfortLevel = 2;

        public CompartmentSleeper(int mass, int humanCapacity, int baggageCapacity) : base(mass, humanCapacity, baggageCapacity)
        {
        }

        new public void GetInfo(int carNumber)
        {
            Console.WriteLine("The car №" + carNumber + " is compartment sleeper type.");
            Console.WriteLine("It's capacity is " + _humanCapacity + " people and " + _baggageCapacity + " kilogramms of baggage.");
        }
    }
}
