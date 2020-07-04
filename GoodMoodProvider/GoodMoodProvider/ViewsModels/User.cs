using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodMoodProvider.ViesModels
{
    public class User
    {
        public Guid ID { get; set; }
        public string Nickname { get; set; }
        public string Firstname { get; set; }
        public string SecondName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public DateTime RegDate { get; set; }
        public int IsOnline { get; set; }

    }
}
