using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

public enum userFeatures
{

}

namespace ModelsLibrary
{
    public class Role
    {
        [Required]
        public Guid ID { get; set; }
        [Required]
        public string Name{ get; set;}

        public List<UserRole> UserRoles { get; set; }
    }
}
