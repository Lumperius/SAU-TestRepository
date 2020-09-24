using CqsLibrary.Interfaces;
using ModelsLibrary;
using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace CqsLibrary.Queries.NewsQueries
{
    public class GetNewsAll : IRequest<IEnumerable<News>>
    {
        public GetNewsAll()
        {

        }
    }
}
