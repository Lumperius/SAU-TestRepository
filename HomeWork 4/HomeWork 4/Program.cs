using HomeWork_4.APS.Tariff;
using HomeWork_4.Interfaces;
using HomeWork_4.Terminals;
using HomeWork_4.Users;
using HomeWork_4.APS;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;

namespace HomeWork_4
{
    class Program : IInputable
    {

        delegate void ErrorDelegate(string str);  //Create error messages event
        static event ErrorDelegate ErrorEvent;

        static void ErrorEventHandler(string str)
        {
            Console.ForegroundColor = ConsoleColor.Red;  //Change the color of the message
            Console.WriteLine(str);
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void Main(string[] args)
        {

            ErrorEvent += ErrorEventHandler;



        AutomaticPhoneStation Station = new AutomaticPhoneStation("ReCall");    //Create station object 
        AutomaticPhoneStation.MessageEvent += AutomaticPhoneStation.MessageHandler;

            List<Tariff> AvailableTariffs = new List<Tariff>()
            {
                new Tariff("Buisness Tariff", 1000, 0, 3),
                new Tariff("Social Tariff", 500, 5, 2),
                new Tariff("Home Tariff", 100, 10, 4)
            };   //Create tariffs
            List<Terminal> Terminals = new List<Terminal>()
            {
                new Terminal("Nokia"),
                new Terminal("Samsung"),
                new Terminal("Apple")
            };  //Create terminals
            Terminal.TermianlMessageEvent += Terminal.TerminalEventHandler;  //

          
            List<User> Users = new List<User>();   //Create a list of test users and employees
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
            }  //Provide every user with a terminal, random tariff and creating a list with calls information

            foreach (User user in Users)  //Assign terminals with a port and create an unique ID for every port
            {
                user.CurrentTerminal.CurrentPortID = Guid.NewGuid();
                Station.Ports.Add(item: new Port(user.CurrentTerminal.CurrentPortID));
                Station.Contracts.Add($"{user.Name} made contract with {Station.CompanyName}");
            }


            User CurrentUser = default;  //The user that currently in a system, null until authentication


            Console.WriteLine("Who are you?");
            CurrentUser = Station.Authentication( Users );  //Sign up
            Console.WriteLine($"Hello {CurrentUser.Name}");

            TimerCallback tcb = new TimerCallback(Station.TakePayment);  //A timer that calls TakePayment function every 5000ms(a month)
            Timer timer = new Timer(tcb, Users, 5000, 20000);

            while (true)    //Start of the user's Interface
            {
                Console.WriteLine("\n1: Choose another user");
                Console.WriteLine("2: Make a call");
                Console.WriteLine("3: Watch user's info and call history");
                Console.WriteLine("4: Connect/disconnect terminal");
                Console.WriteLine("5: Show balance sheet");
                Console.WriteLine("6: Change tariff");
                Console.WriteLine("0: Sign up a new user");

                int Input = RequestNumber();

                switch (Input)
                  {
                    case 1: //Enter as a new user
                        CurrentUser = Station.Authentication(Users);   
                        Console.WriteLine($"Hello {CurrentUser.Name}");
                        break;

                    case 2: //Make a call
                        {
                            if (CurrentUser.CurrentTerminal.IsConnected == TerminalState.off)  //Checks terminal state and account
                                { ErrorEvent?.Invoke("Termianl is disconected."); break; }
                            if (CurrentUser.CurrentTerminal.IsConnected == TerminalState.calling)
                                { ErrorEvent?.Invoke("Cant't execute this during call."); break; }
                            if (CurrentUser.Account <= 0 )
                                { ErrorEvent?.Invoke("You don't have enough money on your account."); break; }


                            Console.WriteLine("How many seconds should the call last?");
                            int duration = RequestNumber();

                            Console.WriteLine("And who are you calling to?");
                            for (int i = 0; i < Users.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}: {Users[i].Name}");
                            }

                            Input = RequestNumber();
                            User Reciever = Users[Input - 1];

                            if (Reciever.CurrentTerminal.IsConnected != TerminalState.connected) //Checks reciever's terminal state
                            { ErrorEvent?.Invoke("Can't call this user."); break; }


                            if (Reciever == CurrentUser) { ErrorEvent?.Invoke("You can't call yourself."); break; }  //Prevents calling yourself(I could have deleted cuurent user from the list, but I was too lazy at te moment :p)

                            Task<CallList> task = Task<CallList>.Factory.StartNew(() => CurrentUser.CurrentTerminal.MakeCall(CurrentUser.Name, Reciever.Name, duration));  //Start the call as a new task

                            CurrentUser.CallList.CallReciever.Add(Reciever.Name);  //Adding information about the call to CallList
                            CurrentUser.CallList.CallSender.Add(CurrentUser.Name);
                            CurrentUser.CallList.Duration.Add(duration);
                            CurrentUser.CallList.TariffAtTime.Add(CurrentUser.CurrentTariff.TariffName);

                            int CallPrice = duration * CurrentUser.CurrentTariff.MinuteCost / 60 + CurrentUser.CurrentTariff.CallCost;
                            if(CurrentUser is Employee) { CallPrice = (int)(0.9 * CallPrice); }
                            CurrentUser.CallList.TotalCost.Add(CallPrice);

                            CurrentUser.CurrentDebt += CallPrice;  //And adding call price to user's debt 
                        }
                        break;

                    case 3:  //Get info about user and CallList
                        User.GetInfo(CurrentUser);
                        break;

                    case 4: //Turn terminal off/on port
                        CurrentUser.SwitchPortState();
                        Console.WriteLine($"Terminal is {CurrentUser.CurrentTerminal.IsConnected}");
                        break;

                    case 5:  //Balance check
                        Console.WriteLine($"Your current balance is {CurrentUser.Account}");
                        break;

                    case 6:  //Get info about user and CallList
                        CurrentUser.SwitchTariff(AvailableTariffs);
                        break;

                    case 0:  //Sing up a new user
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
