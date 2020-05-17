using System;
using System.Collections.Generic;
using System.Text;
using HomeWork_4.Interfaces;



namespace HomeWork_4.APS.Tariff
{
    class Tariff 
    {
        public string TariffName { get; }
        public int MonthCost { get; }
        public int CallCost { get; }
        public int MinuteCost { get; }


        public Tariff(string name, int monthCost, int callCost, int minuteCost)
        {
            TariffName = name;
            MonthCost = monthCost;
            CallCost = callCost;
            MinuteCost = minuteCost;
        }

        public void GetInfo()
        {
            Console.WriteLine($"Tariff's name is {TariffName}");
            Console.WriteLine($"Month cost is {MonthCost}");
            Console.WriteLine($"Call cost is {CallCost}");
            Console.WriteLine($"Minute cost is {MinuteCost}");
        }
    }
}
