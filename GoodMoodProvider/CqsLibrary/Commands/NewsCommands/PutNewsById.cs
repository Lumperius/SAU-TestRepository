using CqsLibrary.Interfaces;
using MediatR;
using ModelsLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace CqsLibrary.Commands.NewsCommands
{
    public class PutNewsById : IRequest
    {
        public Guid Id { get; }
        public News News { get; }

        public PutNewsById(Guid id, News news)
        {
            Id = id;
            News = news;
        }
    }
}
