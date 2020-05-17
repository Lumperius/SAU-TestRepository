using System;
using System.Collections.Generic;
using HomeWork_4.Users;
using System.Text;
using HomeWork_4.Interfaces;
using HomeWork_4.APS;
using System.Threading;

namespace HomeWork_4
{

    delegate void StationDelegate(string str);


    class AutomaticPhoneStation : IInputable
    {
        public string CompanyName { get; set; }

        public List<Port> Ports;

        public List<string> Contracts;


        public static event StationDelegate MessageEvent;

        public static void MessageHandler(string str)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(str);
            Console.ForegroundColor = ConsoleColor.White;
        }

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


        public void TakePayment(object usersObj)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            MessageEvent?.Invoke("\nEnd of the month\n");

            if (usersObj is List<User>)
            {
                foreach(User user in (usersObj as List<User>))
                {
                    if(user is Employee)
                    {
                        int debtForMonth = user.CurrentDebt + user.CurrentTariff.MonthCost;
                        user.Account -= debtForMonth;
                        user.Account += (user as Employee).Wage;
                        MessageEvent?.Invoke($"{user.Name} pays {debtForMonth} and gets {(user as Employee).Wage}, {user.Account} left.");
                    }
                    else 
                    {
                        int debtForMonth = user.CurrentDebt + user.CurrentTariff.MonthCost;
                        user.Account -= debtForMonth;
                        MessageEvent?.Invoke($"{user.Name} pays {debtForMonth}, {user.Account} left.");
                    }
                }
            }

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
