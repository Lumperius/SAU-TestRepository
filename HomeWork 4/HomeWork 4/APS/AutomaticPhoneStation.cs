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

        public List<Port> Ports;  //List of ports the statio have

        public List<string> Contracts;  //List of made contracts


        public static event StationDelegate MessageEvent;  //Station messages event

        public static void MessageHandler(string str)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(str);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public User Authentication(List<User> users)  //Authentication
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

        public User SignUp() //Sing up a new user
        {
            Console.WriteLine("Enter your name");
            string name = Console.ReadLine();

            Console.WriteLine($"Welcome {name}, you are gifted 50000 virtual coins to your acount");
            return new User(name, 50000);
        }


        public void TakePayment(object usersObj)  //function called at the end of months, takes user's debt with discount for employees, pays wage to employees
        {
            MessageEvent?.Invoke("\nEnd of the month\n");

            if (usersObj is List<User>)
            {
                foreach(User user in (usersObj as List<User>))
                {
                    if(user is Employee)  //Check for employees
                    {
                        int debtForMonth = user.CurrentDebt + user.CurrentTariff.MonthCost;
                        user.Account -= (int)(debtForMonth * 0.9);  //Company provides 10% discount for employees
                        user.CurrentDebt = 0;
                        user.Account += (user as Employee).Wage;
                        MessageEvent?.Invoke($"{user.Name} pays {debtForMonth} and gets {(user as Employee).Wage}, {user.Account} left.");
                    }
                    else 
                    {
                        int debtForMonth = user.CurrentDebt + user.CurrentTariff.MonthCost;
                        user.Account -= debtForMonth;
                        user.CurrentDebt = 0;
                        MessageEvent?.Invoke($"{user.Name} pays {debtForMonth}, {user.Account} left.");
                    }

                    user.TariffWasChanged = false;
                }
            }
        }

        public AutomaticPhoneStation(string name)
        {
            CompanyName = name;
            Ports = new List<Port>();
            Contracts = new List<string>();
        }


    }
}
