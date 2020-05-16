using System;
using System.Collections.Generic;
using HomeWork_4.Users;
using System.Text;
using HomeWork_4.Interfaces;
using HomeWork_4.APS;
using System.Threading;

namespace HomeWork_4
{
    public delegate void Handler();

    class AutomaticPhoneStation : IInputable
    {
        public string CompanyName { get; set; }

        public List<Port> Ports;

        public List<string> Contracts;


        public event Handler messageEvent;

        public User Authentication(List<User> users)
        {
            for (int i = 0; i < users.Count; i++)
            {
                if(users[i] is Employee) { Console.WriteLine($"{i + 1}: {users[i].Name} {(users[i] as Employee).Position}");}
                else { Console.WriteLine($"{i + 1}: {users[i].Name}"); }
            }
            while (true) { 
            int Input = RequestNumber();

                if (Input <= users.Count && Input > 0) { return users[Input - 1]; }
                else { Console.WriteLine("Incorrect number, try again"); }
                         }
        }

        public User SignUp()
        {
            Console.WriteLine("Enter your name");
            string name = Console.ReadLine();

            Console.WriteLine($"Welcome {name}, you are gifted 50000 virtual coins to your acount");
            return new User(name, 50000);
        }


        public void TakePayment(object obj)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nEnd of the month!\n");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public AutomaticPhoneStation(string name)
        {
            CompanyName = name;
            Ports = new List<Port>();
            Contracts = new List<string>();
        }


    }
}
