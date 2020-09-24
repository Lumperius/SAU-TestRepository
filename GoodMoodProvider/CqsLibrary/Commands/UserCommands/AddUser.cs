using CqsLibrary.Interfaces;
using MediatR;
using ModelsLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace CqsLibrary.Commands.UserCommands
{
    public class AddUser : IRequest
    {
        public User User { get; }

        public AddUser(User user)
        {
            User = user;
        }
    }
}
