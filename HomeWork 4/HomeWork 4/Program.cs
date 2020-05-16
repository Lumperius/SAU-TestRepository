using HomeWork_4.APS.Tariff;
using HomeWork_4.Interfaces;
using HomeWork_4.Terminals;
using HomeWork_4.Users;
using HomeWork_4.APS;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace HomeWork_4
{
    class Program : IInputable
    {  

        static Task Main(string[] args)
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
            Users.Add(new User("Bob", 100000)); 
            Users.Add(new Employee("Danuel", 120000, "Production manager",  14000));
            Users.Add(new User("Elen", 90000));
            Users.Add(new Employee("Mary", 150000, "Designer", 13000));
            Users.Add(new User("Jack", 80000));

        
            for (int i = 0; i < Users.Count; i++)
            {
                Random rand = new Random();
                int RNDint;

                RNDint = rand.Next(0, AvailableTariffs.Count);
                Users[i].CurrentTariff = AvailableTariffs[RNDint];
                
                RNDint = rand.Next(0, Terminals.Count);
                Users[i].CurrentTerminal = Terminals[RNDint];
                Users[i].SwitchPortState();
                
                Users[i].CallList = new CallList();
            }

            foreach (User user in Users)
            {
                user.CurrentTerminal.CurrentPortID = Guid.NewGuid();
                Station.Ports.Add(item: new Port(user.CurrentTerminal.CurrentPortID));
                Station.Contracts.Add($"{user.Name} made contract with {Station.CompanyName}");
            }


            User CurrentUser = default;




            Console.WriteLine("Who are you?");
            CurrentUser = Station.Authentication( Users );
            Console.WriteLine($"Hello {CurrentUser.Name}");

            TimerCallback tcb = new TimerCallback(Station.TakePayment);
            Timer timer = new Timer(tcb, 1, 5000, 50000);


            while (true)
            {
                Console.WriteLine("\n1: Choose another user");
                Console.WriteLine("2: Make a call");
                Console.WriteLine("3: Watch user's info and call history");
                Console.WriteLine("4: Connect/disconnect terminal");
                Console.WriteLine("5: Show balance sheet");
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
                            if (CurrentUser.CurrentTerminal.IsConnected == TerminalState.off) { Console.WriteLine("Termianl is disconected."); break; }
                            if (CurrentUser.CurrentTerminal.IsConnected == TerminalState.calling) { Console.WriteLine("Cant't execute this during call."); break; }


                            Console.WriteLine("How many seconds should the call last?");
                            int duration = RequestNumber();

                            Console.WriteLine("And who are you calling to?");
                            for (int i = 0; i < Users.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}: {Users[i].Name}");
                            }

                            Input = RequestNumber();
                            User Reciever = Users[Input - 1];
                            if (Reciever.CurrentTerminal.IsConnected != TerminalState.connected) { Console.WriteLine("Can't call this user."); break; }


                            if (Reciever == CurrentUser) { Console.WriteLine("You can't call yourself"); break; }

                            Task<CallList> task = Task<CallList>.Factory.StartNew(() => CurrentUser.CurrentTerminal.MakeCall(CurrentUser.Name, Reciever.Name, duration));

                            CurrentUser.CallList.CallReciever.Add(Reciever.Name);
                            CurrentUser.CallList.CallSender.Add(CurrentUser.Name);
                            CurrentUser.CallList.Duration.Add(duration);
                            CurrentUser.CallList.TariffAtTime.Add(CurrentUser.CurrentTariff.TariffName);

                            int CallPrice = duration * CurrentUser.CurrentTariff.MinuteCost / 60 + CurrentUser.CurrentTariff.CallCost;
                            if(CurrentUser is Employee) { CallPrice = (int)(0.9 * CallPrice); }
                            CurrentUser.CallList.TotalCost.Add(CallPrice);

                            CurrentUser.Debt += CallPrice;
                        }
                        break;

                    case 3:
                        CurrentUser.GetInfo(CurrentUser);
                        break;

                    case 4:
                        CurrentUser.SwitchPortState();
                        break;

                    case 5:
                        Console.WriteLine($"Your current balance is {CurrentUser.Account}");
                        break;

                    case 0:
                        User newUser = Station.SignUp();
                        Random rand = new Random();
                        int RNDint;

                        RNDint = rand.Next(0, AvailableTariffs.Count);
                        newUser.CurrentTariff = AvailableTariffs[RNDint];

                        RNDint = rand.Next(0, Terminals.Count);
                        newUser.CurrentTerminal = Terminals[RNDint];
                        newUser.CallList = new CallList();

                        newUser.CurrentTerminal.CurrentPortID = Guid.NewGuid();
                        Station.Ports.Add(item: new Port(newUser.CurrentTerminal.CurrentPortID));
                        Station.Contracts.Add($"{newUser.Name} made contract with {Station.CompanyName}");

                        Users.Add(newUser);
                        CurrentUser = newUser;

                        break;

                    default:
                        Console.WriteLine("Invalid input");
                        break;
                  };
                
                
            }
        }
    }
}
