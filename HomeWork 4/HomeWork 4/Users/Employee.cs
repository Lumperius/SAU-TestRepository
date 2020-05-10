using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork_4.Users 
{
    class Employee : User
    {
        public string Position { get; }
        public int Wage { get; }

        public Employee(string name, string position, int wage) : base(name)
        {
            Position = position;
            Wage = wage;
        }
    }
}
