using System;
using System.Collections.Generic;
using System.Text;

namespace ModelsLibrary.Responces
{
    public class AuthenticationResponse
    {
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
