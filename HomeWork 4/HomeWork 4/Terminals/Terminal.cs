using HomeWork_4.Terminals;
using System;
using System.Threading;

namespace HomeWork_4
{
    class Terminal
    {
        public string Model { get; }
        public string Number { get; set; }
        public Guid CurrentPortID { get; set; }
       
        public TerminalState IsConnected { get; set; }



        public CallList MakeCall(string senderName, string recieverName, int duration)
        {
            Console.WriteLine("\nBeep beep..");
            IsConnected = TerminalState.calling;
            Console.WriteLine("\nCall Started");
            CallList CallInfo = new CallList();
            Thread.Sleep(duration * 1000);
            Console.WriteLine("\nCall Finished");
            IsConnected = TerminalState.connected;

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
