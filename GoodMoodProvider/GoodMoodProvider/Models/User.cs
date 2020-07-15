using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodMoodProvider.Models
{
    public class User
    {
        public Guid ID { get; set; }
        public string Password { get; set; }
        public string Login { get; set; }
        public string Firstname { get; set; }
        public string SecondName { get; set; }
        public DateTime BirthDay { get; set; }
        public string Gender { get; set; }
        public DateTime RegDate { get; set; }
        public int IsOnline { get; set; }

    }
}
