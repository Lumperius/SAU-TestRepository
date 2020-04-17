using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork_3
{
    class TrainElement

    {
        protected int _mass;

        public int getMass() => _mass;
        public TrainElement(int mass)
        {
            _mass = mass;
        }

    }
}

