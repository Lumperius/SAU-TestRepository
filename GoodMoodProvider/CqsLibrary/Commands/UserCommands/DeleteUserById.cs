using CqsLibrary.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CqsLibrary.Commands.UserCommands
{
    public class DeleteUserById : IRequest
    {
        public Guid Id { get; }

        public DeleteUserById(Guid id)
        {
            Id = id;
        }
    }
}
