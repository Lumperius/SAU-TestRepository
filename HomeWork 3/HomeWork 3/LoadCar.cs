using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork_3
{
    class LoadCar : TrainElement, iInformative
    {
        protected int _maxLoadWeight;
        public int GetMaxLoadWeight() => _maxLoadWeight;
     
        public LoadCar(int mass, int maxLoadWeight) : base(mass)
        {
            _maxLoadWeight = maxLoadWeight;
            comfortLevel = 0;
        }

        public override void GetInfo(int carNumber)
        {
            Console.WriteLine("The car №" + carNumber + " load car.");
            Console.WriteLine("It used to transport loads with maximum weight " + _maxLoadWeight + "kilogramms.");
        }
    }
}
