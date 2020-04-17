using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork_3
{
    class Load_Car : TrainElement
    {
        protected int _maxLoadWeight;

        public int getMaxLoadWeight => _maxLoadWeight;
     
        public Load_Car(int mass, int maxLoadWeight) : base(mass)
        {
            _maxLoadWeight = maxLoadWeight;
        }
    }
}
