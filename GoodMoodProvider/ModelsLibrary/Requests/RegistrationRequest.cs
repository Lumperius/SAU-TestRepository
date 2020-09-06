using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace ModelsLibrary.Requests
{
    public class RegistrationRequest
    {
        [Required]
        public string Login { get; set; }
        [JsonIgnore]
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
