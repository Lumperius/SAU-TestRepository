using System;
using System.Collections.Generic;
using System.Text;

enum TerminalState
    {
    off,
    calling,
    connected
    }

namespace HomeWork_4.APS
{
    class Port
    {
        public Guid PortID { get; set; }
        public TerminalState state = 0;

        public Port(Guid id)
            {
            PortID = id;
            }
    }
}