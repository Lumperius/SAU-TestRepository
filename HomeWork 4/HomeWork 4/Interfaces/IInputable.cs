using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork_4.Interfaces
{
    class IInputable 
    {
        public static int RequestNumber() //Function requests line, transforms it into int and returns the result
        {
            var collectedLine = Console.ReadLine();
            int collectedNumber;

            for (bool i = false; i == false;)
            {
                try { collectedNumber = Convert.ToInt32(collectedLine); i = true; }
                catch
                {
                    Console.WriteLine("This input is invalid, try again!");
                    collectedLine = Console.ReadLine(); i = false;
                }
            }
            collectedNumber = Convert.ToInt32(collectedLine);
            return collectedNumber;
        }

    }
}
