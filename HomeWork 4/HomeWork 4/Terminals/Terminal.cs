using HomeWork_4.Terminals;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace HomeWork_4
{
    class Terminal
    {
        public string Model { get; }
        public string Number { get; set; }



        public CallList MakeCall(string senderName, string recieverName, int duration)
        {
            Console.WriteLine("Beep beep..");
            Console.WriteLine("Call Started");
            CallList CallInfo = new CallList();


            Thread.Sleep(duration * 1000);
            Console.WriteLine("Call Finished");


            return CallInfo;
        }


        public Terminal(string model)
        {
            Model = model;
            Random rand = new Random();
            Number = "+375 " + rand.Next(100000, 999999).ToString();
        }
    }
}
