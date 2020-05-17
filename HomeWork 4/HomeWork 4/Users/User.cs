using HomeWork_4.APS.Tariff;
using HomeWork_4.Interfaces;
using HomeWork_4.Terminals;
using HomeWork_4.APS;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Globalization;

namespace HomeWork_4
{
    class User : IInputable
    {
       

        public string Name { get; set; }
        public int Account { get; set; }
        public int CurrentDebt { get; set; }
        public bool TariffWasChanged { get; set; }


        public Tariff CurrentTariff = default;  //Users will have a terminal, a tariff and callList
        public Terminal CurrentTerminal = default;
        public CallList CallList = default;


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


        public static void GetInfo(User requestedUser)  //Returns user's info and callList
        {
            Console.WriteLine($"User's name - {requestedUser.Name}");
            Console.WriteLine($"Currnet terminal - {requestedUser.CurrentTerminal.Model}, ID - {requestedUser.CurrentTerminal.CurrentPortID}");
            Console.WriteLine($"Phone number - {requestedUser.CurrentTerminal.Number}");
            Console.WriteLine($"Currnet tarrif - {requestedUser.CurrentTariff.TariffName}\n");

            if (requestedUser.CallList.TotalCost != null)  //Checks if any calls were made
            {
                //Couldn't come up with better interface request to sort the list, sorry
                Console.WriteLine("\nSort call list? Type in 'y' to confirm, any other symbol to continue\n");
                if(Console.ReadLine() == "y")
                {
                    Console.WriteLine("Sort by...\n 1)Reciever name\n 2)Calls cost\n 3)Calls duration\n");
                    int usersInput = RequestNumber();
                    switch(usersInput)
                    {
                        case 1:
                            List<string> reciverSortedList = requestedUser.CallList.CallReciever
                                                                          .OrderBy(names => names)
                                                                          .ToList();
                            break;
                       
                        case 2:
                            List<int> costSortedList = requestedUser.CallList.TotalCost
                                                                    .OrderBy(costs => costs)
                                                                    .ToList();
                            break;                                 
                     
                        case 3:
                            List<int> durationSortedList = requestedUser.CallList.Duration
                                                                           .OrderBy(dur => dur)
                                                                           .ToList();
                            break;
                    }
                }

                for (int i = 0; i < requestedUser.CallList.TotalCost.Count; i++)
                {
                    Console.WriteLine($"{i} -- CallReciever: { requestedUser.CallList.CallReciever[i]}");
                    Console.WriteLine($"     CallSender: { requestedUser.CallList.CallSender[i]}");
                    Console.WriteLine($"     Duration: { requestedUser.CallList.Duration[i]}");
                    Console.WriteLine($"     TariffAtTime: { requestedUser.CallList.TariffAtTime[i]}");
                    Console.WriteLine($"     TotalCost: { requestedUser.CallList.TotalCost[i]}");
                }
            }
            else { Console.WriteLine("No calls made"); }

        }

        public void SwitchTariff(List<Tariff> tariffs)
        {
            Console.WriteLine("Which tariff would you like to set up?");
            for(int i = 0; i < tariffs.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {tariffs[i].TariffName}");
            }
            int usersInput = RequestNumber();
            CurrentTariff = tariffs[usersInput - 1];
            TariffWasChanged = true;
        }

        public void SwitchPortState()  //Switches port state
        {
            if (this.CurrentTerminal.IsConnected == TerminalState.off)
                 { this.CurrentTerminal.IsConnected = TerminalState.connected;  } 
            else { this.CurrentTerminal.IsConnected = TerminalState.off; }
        }

        public User(string name, int startAccount)
        {
            Account = startAccount;
            Name = name;
            TariffWasChanged = false;
        }

    }
}
