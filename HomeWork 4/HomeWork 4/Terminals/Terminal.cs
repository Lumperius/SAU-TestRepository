using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork_4
{
    class Terminal
    {
        public string Model { get; }
        public string Number { get; set; }



        public void MakeCall<T>(T reciever, int duration)
        {

        }


        public Terminal(string model)
        {
            Model = model;
            Random rand = new Random();
            Number = "375 " + rand.Next(100000, 999999).ToString();
        }
    }
}
