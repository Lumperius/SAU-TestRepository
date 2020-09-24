using CqsLibrary.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace CqsLibrary.Commands.NewsCommands
{
    public class DeletNewsById : IRequest
    {
        public Guid Id { get; }
        DeletNewsById(Guid id)
        {
            Id = id;
        }
    }
}
