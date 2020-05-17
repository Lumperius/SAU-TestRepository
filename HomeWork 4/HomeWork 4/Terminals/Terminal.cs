using HomeWork_4.Terminals;
using System;
using System.Threading;

namespace HomeWork_4
{

    delegate void TerminalDelegate(string str);

    class Terminal
    {
        public string Model { get; }
        public string Number { get; set; }
        public Guid CurrentPortID { get; set; }
       
        public TerminalState IsConnected { get; set; }

      
        
        public static event TerminalDelegate TermianlMessageEvent;

        public static void TerminalEventHandler(string str)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(str);
            Console.ForegroundColor = ConsoleColor.White;
        }



        public CallList MakeCall(string senderName, string recieverName, int duration)
        {
            Console.ForegroundColor = ConsoleColor.Red;

            TermianlMessageEvent?.Invoke("\nBeep beep..");
            IsConnected = TerminalState.calling;
            TermianlMessageEvent?.Invoke("\nCall Started");
            CallList CallInfo = new CallList();
            Thread.Sleep(duration * 1000);
            TermianlMessageEvent?.Invoke("\nCall Finished\n");
            IsConnected = TerminalState.connected;
           
            Console.ForegroundColor = ConsoleColor.White;


            return CallInfo;
        }


        public Terminal(string model)
        {
            Model = model;
            Random rand = new Random();
            Number = "+375 " + rand.Next(100000, 999999).ToString();
            IsConnected = TerminalState.off;
        }
    }
}
