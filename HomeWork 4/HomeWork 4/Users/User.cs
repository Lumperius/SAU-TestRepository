using HomeWork_4.APS.Tariff;
using HomeWork_4.Interfaces;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;


namespace HomeWork_4
{
    class User : IInputable
    {
       

        public string Name { get; set; }
        public Guid ID { get; }
        public int Account { get; set; }


        public object CurrentTariff = default;


        public void Add (int summ)      => Account += summ;
        public void Withdraw (int summ) => Account -= summ;
       
        public void SetTariff(List<Tariff> tariffList)
        {
            Console.WriteLine("Available Tariffs:");
            for(int i = 0; i < tariffList.Count; i++)
                {
                Console.WriteLine($"{i+1} - {tariffList[i].TariffName}");
                }
            int input = RequestNumber();
            if (input >= 0 && input < tariffList.Count ) 
                 { CurrentTariff = tariffList[input]; }
            else { Console.WriteLine("Incorrect input"); }
        }

        public User(string name)
        {
            Name = name;
        }

    }
}
