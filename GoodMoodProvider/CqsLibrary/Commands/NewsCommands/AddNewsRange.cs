using CqsLibrary.Interfaces;
using MediatR;
using ModelsLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace CqsLibrary.Commands.NewsCommands
{
    public class AddNewsRange : IRequest
    {
        public IEnumerable<News> NewsObjects { get; }

        public AddNewsRange(IEnumerable<News> newsObjects)
        {
            NewsObjects = newsObjects;
        }
    }
}
