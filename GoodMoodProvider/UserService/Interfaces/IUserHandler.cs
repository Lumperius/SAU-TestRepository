using ModelsLibrary;
using ModelsLibrary.Requests;
using ModelsLibrary.Responces;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserService.Interfaces
{
    public interface IUserHandler
    {
        public AuthenticationResponse Authenticate(Guid userId);
        public User Register(RegistrationRequest request);

    }
}
