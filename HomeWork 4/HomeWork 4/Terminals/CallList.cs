using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork_4.Terminals
{
    class CallList //Users provided with CallList, that cotains information about calls
    {
        public List<int> Duration = default;
        public List<int> TotalCost = default;
        public List<string> CallSender = default;
        public List<string> CallReciever = default;
        public List<string> TariffAtTime = default;

        public CallList ()
        {
            Duration = new List<int>();
            TotalCost = new List<int>();
            CallSender = new List<string>();
            CallReciever = new List<string>();
            TariffAtTime = new List<string>();
        }
    }
}
