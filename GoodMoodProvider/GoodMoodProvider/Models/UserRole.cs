using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoodMoodProvider.Models
{
    public class UserRole
    {
        public Guid ID { get; set; }
        public Guid UserID { get; set; }
        public Guid RoleID { get; set; }

        public User User;

        public Role Role;
    }
}
