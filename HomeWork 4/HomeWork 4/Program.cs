using HomeWork_4.APS.Tariff;
using HomeWork_4.Interfaces;
using HomeWork_4.Users;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HomeWork_4
{
    class Program : IInputable
    {  
        static void Main(string[] args)
        {
            Console.WriteLine("Hello User!");
           
            
            List<Tariff> AvailableTariffs = new List<Tariff>()
            {
                new Tariff("Buisness Tariff", 1000, 0, 3),
                new Tariff("Social Tariff", 500, 5, 2),
                new Tariff("Home Tariff", 100, 10, 4)
            };

            List<Terminal> Terminals = new List<Terminal>()
            {
                new Terminal("Nokia"),
                new Terminal("Samsung"),
                new Terminal("Apple")
            };



            List<User> Users = new List<User>();
            Users.Add(new User("Bob"));
            Users.Add(new Employee("Danuel", "Production manager", 12000));
            Users.Add(new User("Elen"));
            Users.Add(new Employee("Mary", "Designer", 13000));
            Users.Add(new User("Jack"));

          
            
            for (int i = 0; i < Users.Count; i++)
            {
                Random rand = new Random();
                int RNDint = rand.Next(0, Users.Count);
                Users[i].CurrentTariff = AvailableTariffs[RNDint];
                RNDint = rand.Next(0, Users.Count);
                Users[i].CurrentTerminal = Terminals[RNDint];
            }



            while(true)
            { 
                  int UsersInput = RequestNumber();
                  switch(UsersInput)
                  {
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                  }
                
                
            }
        }
    }
}
