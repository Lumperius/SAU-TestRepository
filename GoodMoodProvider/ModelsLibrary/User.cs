using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ModelsLibrary
{
    public class User
    {
        public Guid ID { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public List<UserRole> UserRoles { get; set; }
        public List<Comment> Comments { get; set; }

    }
}
