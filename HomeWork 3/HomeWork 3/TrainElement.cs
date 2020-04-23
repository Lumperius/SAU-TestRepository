using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork_3
{
    abstract class TrainElement : iInformative

    {
        protected int _mass;
        public byte comfortLevel;
        public int getMass() => _mass;

        public TrainElement(int mass)
        {
            _mass = mass;
        }

        public abstract void GetInfo(int carNumber);
        

    }
}

