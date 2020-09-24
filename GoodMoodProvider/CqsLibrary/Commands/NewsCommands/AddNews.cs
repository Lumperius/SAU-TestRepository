using CqsLibrary.Interfaces;
using MediatR;
using ModelsLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace CqsLibrary.Commands.NewsCommands
{
    public class AddNews : IRequest
    {
        public News News { get; }

        public AddNews(News news)
        {
            News = news;
        }
    }
}
