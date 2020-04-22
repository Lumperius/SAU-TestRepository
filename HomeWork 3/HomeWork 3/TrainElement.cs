using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork_3
{
    class TrainElement : iInformative

    {
        protected int _mass;
        public byte ComfortLevel;

        public int getMass() => _mass;

        public TrainElement(int mass)
        {
            _mass = mass;
        }

        public void GetInfo(int carNumber)
        { }

    }
}

