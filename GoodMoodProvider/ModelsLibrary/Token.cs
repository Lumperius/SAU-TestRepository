using System;
using System.Collections.Generic;
using System.Text;

namespace ModelsLibrary
{
    public class Token
    {
        public Guid Id { get; set; }
        public string TokenString { get; set; }
        public User User { get; set; }
    }
}
