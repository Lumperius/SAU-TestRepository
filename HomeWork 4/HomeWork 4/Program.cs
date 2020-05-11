using HomeWork_4.APS.Tariff;
using HomeWork_4.Interfaces;
using HomeWork_4.Terminals;
using HomeWork_4.Users;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace HomeWork_4
{
    class Program : IInputable
    {  
        static void Main(string[] args)
        {
            AutomaticPhoneStation Station = new AutomaticPhoneStation("ReCall");
                    
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
                int RNDint;

                RNDint = rand.Next(0, AvailableTariffs.Count);
                Users[i].CurrentTariff = AvailableTariffs[RNDint];
                
                RNDint = rand.Next(0, Terminals.Count);
                Users[i].CurrentTerminal = Terminals[RNDint];
                
                Users[i].CallList = new CallList();
            }

            User CurrentUser = default;


            Console.WriteLine("Who are you?");
            CurrentUser = Station.Authentication( Users );
            Console.WriteLine($"Hello {CurrentUser.Name}");
           


            while (true)
            {
                Console.WriteLine("1: Choose another user");
                Console.WriteLine("2: Make a call");
                Console.WriteLine("3: Watch call history");
                Console.WriteLine("0: Sign up a new user");

                int Input = RequestNumber();

                switch (Input)
                  {
                    case 1:
                        CurrentUser = Station.Authentication(Users);
                        Console.WriteLine($"Hello {CurrentUser.Name}");
                        break;

                    case 2:
                        {
                            Console.WriteLine("How many seconds should the call last?");
                            int duration = RequestNumber();

                            Console.WriteLine("And who are you calling to?");
                            for (int i = 0; i < Users.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}: {Users[i].Name}");
                            }

                            Input = RequestNumber();
                            User Reciever = Users[Input - 1];
                            if (Reciever == CurrentUser) { Console.WriteLine("You cannot call yourself"); break; }

                            Task<CallList> task = Task<CallList>.Factory.StartNew(() => CurrentUser.CurrentTerminal.MakeCall(CurrentUser.Name, Reciever.Name, duration));

                            CallList temp = task.Result;

                            CurrentUser.CallList.CallReciever.Add(Reciever.Name);
                            CurrentUser.CallList.CallSender.Add(CurrentUser.Name);
                            CurrentUser.CallList.Duration.Add(duration);
                            CurrentUser.CallList.TariffAtTime.Add(CurrentUser.CurrentTariff.TariffName);

                            int CallPrice = duration * CurrentUser.CurrentTariff.MinuteCost / 60 + CurrentUser.CurrentTariff.CallCost;
                            if(CurrentUser is Employee) { CallPrice = (int)(0.9 * CallPrice); }
                            CurrentUser.CallList.TotalCost.Add(CallPrice);
                        }
                        break;

                    case 3:
                        if(CurrentUser.CallList.TotalCost != null)
                        { 
                        for(int i = 0; i < CurrentUser.CallList.TotalCost.Count; i++)
                        {
                            Console.WriteLine($"{i} -- CallReciever: {CurrentUser.CallList.CallReciever[i]}");
                            Console.WriteLine($"     CallSender: {CurrentUser.CallList.CallSender[i]}");
                            Console.WriteLine($"     Duration: {CurrentUser.CallList.Duration[i]}");
                            Console.WriteLine($"     TariffAtTime: {CurrentUser.CallList.TariffAtTime[i]}");
                            Console.WriteLine($"     TotalCost: {CurrentUser.CallList.TotalCost[i]}");
                        }
                        }
                        else { Console.WriteLine("No calls made"); }
                        break;

                    case 0:
                        Users.Add(Station.SignUp());
                        break;

                    default:
                        Console.WriteLine("Invalid input");
                        break;
                  };
                
                
            }
        }
    }
}
