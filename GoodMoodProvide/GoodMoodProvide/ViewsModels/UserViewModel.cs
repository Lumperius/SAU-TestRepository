using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoodMoodProvider.ViewsModels
{
    public class UserViewModel
    {
        [Required]
        public string Nickname { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage="Confirm the password")]
        public string ConfirmPassword { get; set; }

        public string Firstname { get; set; }
        public string SecondName { get; set; }
        public DateTime BirthDay { get; set; }
        public string Gender { get; set; }


    }
}
