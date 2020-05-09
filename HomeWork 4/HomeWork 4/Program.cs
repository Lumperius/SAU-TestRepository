using HomeWork_4.APS.Tariff;
using System;
using System.Collections.Generic;

namespace HomeWork_4
{
    class Program
    {

        public static int RequestNumber() //Function requsts line, transforms it to int and returns the result
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


        static void Main(string[] args)
        {
            Console.WriteLine("Hello User!");
            List<Tariff> AvailableTariffs = new List<Tariff>()
            {
                {new Tariff("Buisness Tarrif", 1000, 0, 3)},
                {new Tariff("Social Tarrif", 500, 5, 2)},
                {new Tariff("Home Tarrif", 100, 10, 4)}
            };
        }
    }
}
