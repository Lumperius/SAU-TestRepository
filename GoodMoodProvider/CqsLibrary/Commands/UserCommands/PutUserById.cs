using CqsLibrary.Interfaces;
using MediatR;
using ModelsLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace CqsLibrary.Commands.UserCommands
{
    public class PutUserById : IRequest
    {
        public Guid Id { get; }
        public User User { get; } 

        public PutUserById(Guid id, User user)
        {
            Id = id;
            User = user;
        }
    }
}
