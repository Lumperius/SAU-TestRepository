using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public enum userFeatures
{

}

namespace ModelsLibrary
{
    public class Role
    {
        public Guid ID { get; set; }
        public string Name{ get; set;}

        public List<UserRole> UserRoles { get; set; }
    }
}
