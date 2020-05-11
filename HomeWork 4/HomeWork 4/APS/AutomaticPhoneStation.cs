using System;
using System.Collections.Generic;
using HomeWork_4.Users;
using System.Text;
using HomeWork_4.Interfaces;

namespace HomeWork_4
{
    class AutomaticPhoneStation : IInputable
    {
        public string CompanyName { get; set; }

        List<User> Contracts;

        public User Authentication(List<User> users)
        {
            for (int i = 0; i < users.Count; i++)
            {
                if(users[i] is Employee) { Console.WriteLine($"{i + 1}: {users[i].Name} {(users[i] as Employee).Position}");}
                else { Console.WriteLine($"{i + 1}: {users[i].Name}"); }
            }
            while (true) { 
            int Input = RequestNumber();

                if (Input <= users.Count) { return users[Input - 1]; }
                else { Console.WriteLine("Incorrect number, try again"); }
                         }
        }

        public User SignUp()
        {
            Console.WriteLine("Enter your name");
            string name = Console.ReadLine();

            return new User(name);
        }

        public AutomaticPhoneStation(string name)
        {
            CompanyName = name;
        }

    }
}
