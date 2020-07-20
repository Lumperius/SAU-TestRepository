using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoodMoodProvider.ViewsModels
{
    public class UserViewModel
    {
        [Required(ErrorMessage = "Login is required")]

        public string Login { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage="Password confirmation is required")]
        public string ConfirmPassword { get; set; }

        public string Firstname { get; set; }
        public string SecondName { get; set; }
        public DateTime BirthDay { get; set; }
        public string Gender { get; set; }


    }
}
