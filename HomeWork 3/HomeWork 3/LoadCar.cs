using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork_3
{
    class LoadCar : TrainElement
    {
        protected int _maxLoadWeight;

        public int GetMaxLoadWeight() => _maxLoadWeight;
     
        public LoadCar(int mass, int maxLoadWeight) : base(mass)
        {
            _maxLoadWeight = maxLoadWeight;
        }
    }
}
