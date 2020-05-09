using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork_4.Users 
{
    class Worker : User
    {
        public string Position { get; }
        public int Wage { get; }

        public Worker(string name, string position, int wage) : base(name)
        {
            Position = position;
            Wage = wage;
        }
    }
}
