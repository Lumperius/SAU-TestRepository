using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelsLibrary
{
    public class User
    {
        public Guid ID { get; set; }
        public string Password { get; set; }
        public string Login { get; set; }
        public string Firstname { get; set; }
        public string SecondName { get; set; }
        public DateTime BirthDay { get; set; }
        public int Age { get; set; }

        public string Gender { get; set; }
        public DateTime RegDate { get; set; }
        public bool IsOnline { get; set; }

        public List<UserRole> UserRoles { get; set; }
        public List<Comment> Comments { get; set; }

    }
}
